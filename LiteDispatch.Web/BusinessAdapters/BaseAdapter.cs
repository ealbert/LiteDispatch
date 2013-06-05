namespace LiteDispatch.Web.BusinessAdapters
{
  using System;
  using Domain.Repository;
  using Domain.Services;

  public abstract class BaseAdapter
  {
    protected TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command) where TResult : class
    {
      using (var transManager = Container.GlobalContext.TransFactory.CreateManager())
      {
        return transManager.ExecuteCommand(command);
      }
    }
  }
}