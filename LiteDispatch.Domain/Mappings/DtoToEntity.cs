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

      Mapper.CreateMap<DispatchNote, DispatchNoteDto>()
            .ForMember(d => d.LastTrackingNotification, m => m.MapFrom(o => o.LastTrackingNotificationDescription()))
            .ForMember(d => d.Haulier, m => m.MapFrom(o => o.Haulier.Name))
            .ForMember(d => d.DispatchStatus, m => m.MapFrom(o => o.DispatchNoteStatus))
            .ForMember(d => d.DispatchLineSet, m => m.MapFrom(o => o.DispatchLines()))
            .ForMember(d => d.Latitude, m => m.MapFrom(o => o.LastTrackingNotification == null ? 0 : o.LastTrackingNotification.Latitude))
            .ForMember(d => d.Longitude, m => m.MapFrom(o => o.LastTrackingNotification == null ? 0 : o.LastTrackingNotification.Longitude));

      Mapper.CreateMap<DispatchLine, DispatchLineDto>();
    }
  }
}