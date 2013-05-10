using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteDispatch.Domain.Mappings
{
  using AutoMapper;
  using Entities;
  using Models;

  public static class ModelToEntity
  {
    public static void Install()
    {
      Mapper.CreateMap<DispatchLineModel, DispatchLine>();
      Mapper.CreateMap<DispatchNoteModel, DispatchNote>()
        .ForMember(d => d.Haulier, m => m.Ignore());

      Mapper.CreateMap<HaulierModel, Haulier>();
    }
  }
}
