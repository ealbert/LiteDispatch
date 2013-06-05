using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteDispatch.Domain.Entities
{
  using AutoMapper;
  using Core.DTOs;
  using Models;
  using Repository;

  public class TrackingNotification
    : EntityBase
  {
    protected TrackingNotification() {}

    public static TrackingNotification Create(IRepositoryLocator locator, TrackingNotificationDto dto)
    {
      var dispatchNote = locator.GetById<DispatchNote>(dto.DispatchNoteId);
      var instance = Mapper.Map<TrackingNotification>(dto);
      instance.DispatchNote = dispatchNote;
      return locator.Save(instance);
    }

    public DispatchNote DispatchNote { get; private set; }
    public double Distance { get; private set; }
    public double Eta { get; private set; }
    public DistanceMetricEnum DistanceMetric { get; private set; }
    public DurationMetricEnum DurationMetric { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
  }


}
