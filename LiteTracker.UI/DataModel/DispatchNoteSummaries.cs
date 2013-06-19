﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiteTracker.UI.Services;

namespace LiteTracker.UI.DataModel
{
    public class DispatchNoteSummaries
    {
        private static readonly DispatchNoteSummaries Summaries = new DispatchNoteSummaries();
        private readonly ObservableCollection<DispatchNoteSummary> _allSummaries = new ObservableCollection<DispatchNoteSummary>();
        public ObservableCollection<DispatchNoteSummary> AllSummaries
        {
            get { return _allSummaries; }
        }

        public static IEnumerable<DispatchNoteSummary> GetSummaries()
        {                   
            return Summaries.AllSummaries;
        }


        public static DispatchNoteSummary GetItem(int dispatchNoteId)
        {
            return Summaries._allSummaries.SingleOrDefault(s => s.DispatchNoteId == dispatchNoteId);
        }
       

        public DispatchNoteSummaries()
        {
            var summary = new DispatchNoteSummary { DispatchNoteId = 1, Haulier = "BlueWhale", LastActivity = "23-May-2013 @ 14:31 - Tracking Event", Status = StatusEnum.InTransit, TrackingInfo = "Distance: 233 kms ETA in: 2 hrs 33 mins", Truck = "131D0982"};
            summary.SetImage(summary.StatusImageName(), DispatchNoteSummary.ImageEnum.Main);           
            _allSummaries.Add(summary);

            summary = new DispatchNoteSummary { DispatchNoteId = 2, Haulier = "BlueWhale", LastActivity = "25-May-2013 @ 14:31 - Dispatch was created", Status = StatusEnum.New, TrackingInfo = "Has not started trip", Truck = "11D9827" };
            summary.SetImage(summary.StatusImageName(), DispatchNoteSummary.ImageEnum.Main);
            _allSummaries.Add(summary);
        }
    }

    public interface IDispatchNoteRepository
    {
        
    }
}