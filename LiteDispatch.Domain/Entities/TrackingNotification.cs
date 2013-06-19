using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteDispatch.Domain.Entities
{
  using System.Data.Entity.ModelConfiguration;
  using AutoMapper;
  using Core.DTOs;
  using Models;
  using Repository;

  public class TrackingNotification
    : EntityBase
  {
    protected TrackingNotification() {}

    public static TrackingNotification Create(IRepositoryLocator locator, TrackingNotificationDto dto, DispatchNote dispatchNote)
    {      
      var instance = Mapper.Map<TrackingNotification>(dto);
      instance.DispatchNote = dispatchNote;
      return locator.Save(instance);
    }

    public virtual DispatchNote DispatchNote { get; private set; }
    public double Distance { get; private set; }
    public double Duration { get; private set; }
    public string DistanceMetric { get; private set; }
    public string DurationMetric { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public string DistanceDescription()
    {
        return DistanceMetric.Equals("Kilometer") ? "Kms" : DistanceMetric;
    }
  }


}
