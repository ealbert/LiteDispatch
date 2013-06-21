using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteTracker.UI.DataModel;

namespace LiteTracker.UI.Services
{
  internal class RepositoryServices
  {    
    private IEnumerable<DispatchNoteSummary> _summaries = new List<DispatchNoteSummary>();
    private readonly TrackingServices _trackingServices = new TrackingServices();

    public async Task<IEnumerable<DispatchNoteSummary>> RefreshDispatchNoteSummaries()
    {            
      _summaries = await _trackingServices.GetDispatchNotes();
      return _summaries;
    }

    public DispatchNoteSummary GetItem(int dispatchNoteId)
    {
      return _summaries.SingleOrDefault(s => s.DispatchNoteId == dispatchNoteId);
    }
  }
}
