using LiteTracker.UI.Common;
using System;
using System.Collections.Generic;
using LiteTracker.UI.DataModel;
using Windows.UI.Xaml.Controls;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace LiteTracker.UI
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class MainGroups : LayoutAwarePage
    {
        public MainGroups()
        {
            this.InitializeComponent();
        }

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
            //var groups = SharpyMainGroups.GetGroups();
            //this.DefaultViewModel["Groups"] = groups;
        }

        /// <summary>
        /// Invoked when an item is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            //var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            //var group = e.ClickedItem as SharpyGroup;
            //if (group == null) return;
            //switch (group.GroupType)
            //{
            //    case SharpyGroup.SharpyGroupEnum.Sharpies:
            //        this.Frame.Navigate(typeof(DispatchNotesPage), "AllSummaries");
            //        break;

            //    case SharpyGroup.SharpyGroupEnum.MyTasks:
            //        this.Frame.Navigate(typeof(DispatchNotesPage), "AllSummaries");
            //        break;
            //}
            
        }
    }
}
