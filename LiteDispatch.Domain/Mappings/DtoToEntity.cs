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
            .ForMember(d => d.DurationMetric, m => m.ResolveUsing(new DurationMetricConvertor()))
            .ForMember(d => d.DistanceMetric, m => m.ResolveUsing(new DistanceMetricConvertor()))
            .ForMember(d => d.DispatchNote, m => m.Ignore())
            .ForMember(d => d.Id, m => m .Ignore());
    }

    public class DurationMetricConvertor : ValueResolver<TrackingNotificationDto, DurationMetricEnum>
    {
      #region Overrides of ValueResolver<TrackingNotificationDto,DurationMetricEnum>

      protected override DurationMetricEnum ResolveCore(TrackingNotificationDto source)
      {
        if (source.EtaMetric.Equals("seconds", StringComparison.InvariantCultureIgnoreCase)) return DurationMetricEnum.Seconds;
        if (source.EtaMetric.Equals("minutes", StringComparison.InvariantCultureIgnoreCase)) return DurationMetricEnum.Minutes;
        return DurationMetricEnum.UnKnown;
      }

      #endregion
    }

    public class DistanceMetricConvertor : ValueResolver<TrackingNotificationDto, DistanceMetricEnum>
    {
      #region Overrides of ValueResolver<TrackingNotificationDto,DurationMetricEnum>

      protected override DistanceMetricEnum ResolveCore(TrackingNotificationDto source)
      {
        if (source.EtaMetric.Equals("kilometres", StringComparison.InvariantCultureIgnoreCase)) return DistanceMetricEnum.Kilometers;
        if (source.EtaMetric.Equals("miles", StringComparison.InvariantCultureIgnoreCase)) return DistanceMetricEnum.Miles;
        return DistanceMetricEnum.UnKnown;
      }

      #endregion
    }
  }
}