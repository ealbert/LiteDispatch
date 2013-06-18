using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiteDispatch.Web.Controllers
{
  using BusinessAdapters;
  using Core.DTOs;
  using Domain.Entities;
  using Hubs;
  using Microsoft.AspNet.SignalR;

  public class TrackingController : ApiController
  {
    public TrackingController()
    {
      TrackingAdapter = new TrackingAdapter();
    }

    public TrackingAdapter TrackingAdapter { get; set; }

    [HttpPost]
    public TrackingResponseDto CreateTrackingNotification(TrackingNotificationDto dto)
    {
      var response = TrackingAdapter.CreateTrackingNotification(dto);
      if (response.DispatchNoteId > 0)
      {
        var hubContext = GlobalHost.ConnectionManager.GetHubContext<LiteDispatchHub>();
        hubContext.Clients.All.updateDispatch(response.DispatchNoteId);
      }
      return response;
    }

    [HttpGet]
    public List<DispatchNoteDto> ActiveDispatchNotes()
    {
      return TrackingAdapter.GetActiveDispatchNotes();
    }
  }
}
