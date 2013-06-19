﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LiteDispatch.Core.DTOs;
using LiteTracker.UI.DataModel;
using Newtonsoft.Json;

namespace LiteTracker.UI.Services
{
    class TrackingServices
    {

        public async Task<List<DispatchNoteDto>> GetDispatchNotes(ObservableCollection<DispatchNoteSummary> dispatchNoteSummaries, ObservableCollection<DispatchLineSummary> dispatchLineSummaries)
        {
            const string uriString = @"http://litedispatch.azurewebsites.net/api/tracking/ActiveDispatchNotes";
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));    
                using (var response = await client.GetAsync(uriString))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var dispatches = JsonConvert.DeserializeObject<List<DispatchNoteDto>>(result);
                        dispatches = dispatches.OrderByDescending(d => d.LastUpdate).ToList();
                        dispatchNoteSummaries.Clear();
                        foreach (var dispatchNoteDto in dispatches)
                        {
                            dispatchNoteSummaries.Add(DispatchNoteSummary.Create(dispatchNoteDto));
                        }
                        dispatchLineSummaries.Clear();
                        foreach (var line in dispatchNoteSummaries.SelectMany(d => d.DispatchLineSummaries))
                        {
                            dispatchLineSummaries.Add(line);
                        }
                    }
                }
            }
            return null;                        
        }
    }
}
