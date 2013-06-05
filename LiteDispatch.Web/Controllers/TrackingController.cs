using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiteDispatch.Web.Controllers
{
  using Core.DTOs;

  public class TrackingController : ApiController
    {
      [HttpPost]
      public TrackingResponseDto ReceiveTrackingNotification(TrackingNotificationDto dto)
      {        
        return new TrackingResponseDto{Accepted = true, NotificationId = dto.Id};
      }
    }
}
