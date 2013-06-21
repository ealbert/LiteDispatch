using System.Collections.ObjectModel;
using LiteTracker.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using LiteTracker.UI.DataModel;
using LiteTracker.UI.Services;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LiteTracker.UI
{
  /// <summary>
  /// A page that displays a group title, a list of items within the group, and details for the
  /// currently selected item.
  /// </summary>
  public sealed partial class DispatchNotesPage
  {
    public DispatchNotesPage()
    {
      InitializeComponent();
      _mapServices = new MapServices(mapTrucks, BaseUri);
      App.MobileServices.CurrentChannel.PushNotificationReceived += CurrentChannel_PushNotificationReceived;
    }

    private readonly MapServices _mapServices;

    private async void CurrentChannel_PushNotificationReceived(PushNotificationChannel sender,
                                                               PushNotificationReceivedEventArgs args)
    {
      await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, RefreshDispatchNoteSummaries);
    }

    #region Page state management

    /// <summary>
    /// Populates the page with content passed during navigation.  Any saved state is also
    /// provided when recreating a page from a prior session.
    /// </summary>
    /// <param name="navigationParameter">The parameter value passed to
    /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
    /// </param>
    /// <param name="pageState">A dictionary of state preserved by this page during an earlier
    /// session.  This will be null the first time a page is visited.</param>
    protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
    {
      RefreshDispatchNoteSummaries();

      if (pageState == null)
      {
        this.ItemListView.SelectedItem = null;
        // When this is a new page, select the first item automatically unless logical page
        // navigation is being used (see the logical page navigation #region below.)
        if (!this.UsingLogicalPageNavigation() && this.SummariesViewSource.View != null)
        {
          this.SummariesViewSource.View.MoveCurrentToFirst();
          var selectedSummary = this.SummariesViewSource.View.First() as DispatchNoteSummary;
          if (selectedSummary == null) return;
          this.DefaultViewModel["Lines"] = selectedSummary.DispatchLineSummaries;
        }
      }
      else
      {
        // Restore the previously saved state associated with this page
        if (pageState.ContainsKey("SelectedItem") && this.SummariesViewSource.View != null)
        {
          var selectedItem = App.RepositoryServices.GetItem((int) pageState["SelectedItem"]);
          if (selectedItem == null) return;
          this.DefaultViewModel["Lines"] = selectedItem.DispatchLineSummaries;
          this.SummariesViewSource.View.MoveCurrentTo(selectedItem);
        }
      }
    }

    public async void RefreshDispatchNoteSummaries()
    {
      var dispatchNoteSummaries = await App.RepositoryServices.RefreshDispatchNoteSummaries();
      DefaultViewModel["Summaries"] = new ObservableCollection<DispatchNoteSummary>(dispatchNoteSummaries);
    }

    /// <summary>
    /// Preserves state associated with this page in case the application is suspended or the
    /// page is discarded from the navigation cache.  Values must conform to the serialization
    /// requirements of <see cref="SuspensionManager.SessionState"/>.
    /// </summary>
    /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
    protected override void SaveState(Dictionary<String, Object> pageState)
    {
      if (this.SummariesViewSource.View != null)
      {
        var selectedItem = this.SummariesViewSource.View.CurrentItem as DispatchNoteSummary;
        if (selectedItem != null) pageState["SelectedItem"] = selectedItem.DispatchNoteId;
      }
    }

    #endregion

    #region Logical page navigation

    // Visual state management typically reflects the four application view states directly
    // (full screen landscape and portrait plus snapped and filled views.)  The split page is
    // designed so that the snapped and portrait view states each have two distinct sub-states:
    // either the item list or the details are displayed, but not both at the same time.
    //
    // This is all implemented with a single physical page that can represent two logical
    // pages.  The code below achieves this goal without making the user aware of the
    // distinction.

    /// <summary>
    /// Invoked to determine whether the page should act as one logical page or two.
    /// </summary>
    /// <param name="viewState">The view state for which the question is being posed, or null
    /// for the current view state.  This parameter is optional with null as the default
    /// value.</param>
    /// <returns>True when the view state in question is portrait or snapped, false
    /// otherwise.</returns>
    private bool UsingLogicalPageNavigation(ApplicationViewState? viewState = null)
    {
      if (viewState == null) viewState = ApplicationView.Value;
      return viewState == ApplicationViewState.FullScreenPortrait ||
             viewState == ApplicationViewState.Snapped;
    }

    /// <summary>
    /// Invoked when an item within the list is selected.
    /// </summary>
    /// <param name="sender">The GridView (or ListView when the application is Snapped)
    /// displaying the selected item.</param>
    /// <param name="e">Event data that describes how the selection was changed.</param>
    private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // Invalidate the view state when logical page navigation is in effect, as a change
      // in selection may cause a corresponding change in the current logical page.  When
      // an item is selected this has the effect of changing from displaying the item list
      // to showing the selected item's details.  When the selection is cleared this has the
      // opposite effect.
      if (this.UsingLogicalPageNavigation())
      {
        this.InvalidateVisualState();
      }
      else
      {
        if (e.AddedItems.Count == 0) return;
        var selectedItem = this.ItemListView.SelectedItem as DispatchNoteSummary;
        if (selectedItem == null) return;
        DefaultViewModel["Lines"] = selectedItem.DispatchLineSummaries;
        _mapServices.RefreshMap(selectedItem);
      }
    }

    /// <summary>
    /// Invoked when the page's back button is pressed.
    /// </summary>
    /// <param name="sender">The back button instance.</param>
    /// <param name="e">Event data that describes how the back button was clicked.</param>
    protected override void GoBack(object sender, RoutedEventArgs e)
    {
      if (this.UsingLogicalPageNavigation() && ItemListView.SelectedItem != null)
      {
        // When logical page navigation is in effect and there's a selected item that
        // item's details are currently displayed.  Clearing the selection will return
        // to the item list.  From the user's point of view this is a logical backward
        // navigation.
        this.ItemListView.SelectedItem = null;
      }
      else
      {
        // When logical page navigation is not in effect, or when there is no selected
        // item, use the default back button behavior.
        base.GoBack(sender, e);
      }
    }

    /// <summary>
    /// Invoked to determine the name of the visual state that corresponds to an application
    /// view state.
    /// </summary>
    /// <param name="viewState">The view state for which the question is being posed.</param>
    /// <returns>The name of the desired visual state.  This is the same as the name of the
    /// view state except when there is a selected item in portrait and snapped views where
    /// this additional logical page is represented by adding a suffix of _Detail.</returns>
    protected override string DetermineVisualState(ApplicationViewState viewState)
    {
      // Update the back button's enabled state when the view state changes
      var logicalPageBack = this.UsingLogicalPageNavigation(viewState) && this.ItemListView.SelectedItem != null;
      var physicalPageBack = this.Frame != null && this.Frame.CanGoBack;
      this.DefaultViewModel["CanGoBack"] = logicalPageBack || physicalPageBack;

      // Determine visual states for landscape layouts based not on the view state, but
      // on the width of the window.  This page has one layout that is appropriate for
      // 1366 virtual pixels or wider, and another for narrower displays or when a snapped
      // application reduces the horizontal space available to less than 1366.
      if (viewState == ApplicationViewState.Filled ||
          viewState == ApplicationViewState.FullScreenLandscape)
      {
        var windowWidth = Window.Current.Bounds.Width;
        if (windowWidth >= 1366) return "FullScreenLandscapeOrWide";
        return "FilledOrNarrow";
      }

      // When in portrait or snapped start with the default visual state name, then add a
      // suffix when viewing details instead of the list
      var defaultStateName = base.DetermineVisualState(viewState);
      return logicalPageBack ? defaultStateName + "_Detail" : defaultStateName;
    }

    #endregion
  }
}
