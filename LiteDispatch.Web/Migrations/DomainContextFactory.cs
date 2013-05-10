using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using LiteDispatch.Domain.Mappings;
using LiteDispatch.EF.TransManager;

namespace LiteDispatch.Web.Migrations
{
  public class DomainContextFactory
    : IDbContextFactory<LiteDispatchDbContext>
  {
    public LiteDispatchDbContext Create()
    {
      return new LiteDispatchDbContext(new ModelCreator());
    }
  }
}