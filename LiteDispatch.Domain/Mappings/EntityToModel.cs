using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteDispatch.Domain.Mappings
{
  using AutoMapper;
  using Entities;
  using Models;

  public static class EntityToModel
  {
    public static void Install()
    {
      Mapper.CreateMap<DispatchLine, DispatchLineModel>();
      Mapper.CreateMap<DispatchNote, DispatchNoteModel>()
        .ForMember(d => d.HaulierId, m => m.Ignore())
        .ForMember(d => d.HaulierName, m => m.MapFrom(s => s.Haulier.Name))
        .ForMember(d => d.Lines, m => m.MapFrom(s => s.DispatchLines()))
        .ForMember(d => d.Distance, m => m.MapFrom(s => s.LastTrackingNotification == null 
                                                          ? "N/A" 
                                                          : s.LastTrackingNotification.Distance.ToString("N0")))

          .ForMember(d => d.Duration, m => m.MapFrom(s => s.LastTrackingNotification == null 
                                                       ? "N/A" 
                                                       : GetDurationFromSeconds(s.LastTrackingNotification.Duration)))
          ;

      Mapper.CreateMap<Haulier, HaulierModel>();
    }

    private static string GetDurationFromSeconds(double duration)
    {
      var span = new TimeSpan(0, 0, (int) duration);
      
      var minutes = span.ToString("mm");
      var hrs = span.ToString("hh");
      return string.Format("{0} hrs {1} mins", hrs, minutes);
    }
  }
}
