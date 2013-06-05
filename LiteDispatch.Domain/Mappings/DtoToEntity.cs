namespace LiteDispatch.Domain.Mappings
{
  using System;
  using AutoMapper;
  using Core.DTOs;
  using Entities;

  public static class DtoToEntity
  {
    public static void Install()
    {      
      Mapper.CreateMap<TrackingNotificationDto, TrackingNotification>()
            .ForMember(d => d.DurationMetric, m => m.MapFrom(o => o.DurationMetric))
            .ForMember(d => d.DispatchNote, m => m.Ignore())
            .ForMember(d => d.Id, m => m .Ignore());
    }
  }
}