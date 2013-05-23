namespace LiteDispatch.Web.BusinessAdapters
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Data.SqlClient;
  using System.Linq;
  using Domain.Entities;
  using Domain.Mappings;
  using EF.DbContext;
  using Models;

  public class PascalAdapter
  {
    public PascalRequestModel CalculateLevel(int level)
    {
      List<PascalRecord> results = null;
      var startTime = DateTime.Now;
      using (var context = new LiteDispatchDbContext(new ModelCreator()))
      {        
        for (int i = 0; i < 10; i++)
        {
          var parameter = new SqlParameter("@level", level);
          results = context.Database.SqlQuery<PascalRecord>("GetPascalPyramidLevel @level", parameter).ToList();
        }
      }
      var endTime = DateTime.Now;
      var diff = endTime.Subtract(startTime);
      var res = String.Format("{0} hrs {1} mins {2} secs {3} millisecs", diff.Hours, diff.Minutes, diff.Seconds, diff.Milliseconds);
      
      return new PascalRequestModel
        {
          Level = level,
          ElapsedTime = res,
          PascalRecords = results
        };
    }
    

  }
}