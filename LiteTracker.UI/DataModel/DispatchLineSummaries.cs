using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Media.Imaging;

namespace LiteTracker.UI.DataModel
{
    public class DispatchLineSummaries
    {
        

        private static readonly DispatchLineSummaries Summaries = new DispatchLineSummaries();
        private readonly ObservableCollection<DispatchLineSummary> _allSummaries = new ObservableCollection<DispatchLineSummary>();
        public ObservableCollection<DispatchLineSummary> AllSummaries
        {
            get { return _allSummaries; }
        }

        public static IEnumerable<DispatchLineSummary> GetSummaries()
        {
            return Summaries.AllSummaries;
        }

        public static List<DispatchLineSummary> GetLines(long dispatchNoteId)
        {
            return Summaries._allSummaries.Where(s => s.DispatchNoteId == dispatchNoteId).ToList();
        }

        public DispatchLineSummaries()
        {
            var summary = new DispatchLineSummary { DispatchNoteId = 1, Line = 1, ProductType = "Fresh", Product = "Hake", QuantityDescription = "25 Kg", ClientDescription = "RedSquid (Shop: 18)" };
            summary.SetImage("/images/shark-icon.png");
            _allSummaries.Add(summary);

            summary = new DispatchLineSummary { DispatchNoteId = 1, Line = 2, ProductType = "Frozen", Product = "Frozen Squid", QuantityDescription = "4 Pallet", ClientDescription = "Alaska Brothers (Shop: 4-A)" };
            summary.SetImage("/images/shark-icon.png");
            _allSummaries.Add(summary);

            summary = new DispatchLineSummary { DispatchNoteId = 1, Line = 3, ProductType = "Shellfish", Product = "Mussel", QuantityDescription = "20 Sac", ClientDescription = "Irish Seafood (Shop: 112)" };
            summary.SetImage("/images/shark-icon.png");
            _allSummaries.Add(summary);
        }
    }
}