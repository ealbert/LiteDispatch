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
      return TrackingAdapter.CreateTrackingNotification(dto);
    }
  }
}
