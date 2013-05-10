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
        .ForMember(d => d.HaulierName, m => m.UseValue("Bluewhale"))
        .ForMember(d => d.Lines, m => m.MapFrom(s => s.DispatchLines()));

      Mapper.CreateMap<Haulier, HaulierModel>();
    }
  }
}
