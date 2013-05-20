using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteDispatch.EF.Context
{
  using DbContext;
  using Domain.TransManager;

  /// <summary>
  /// Replica of the LiteDispatchDbContext for
  /// the purpose of creating migrations
  /// </summary>
  public class LiteDispatchMigrationsDbContext : LiteDispatchDbContext
  {
    public LiteDispatchMigrationsDbContext(IModelCreator modelCreator) : base(modelCreator)
    {
    }
  }
}
