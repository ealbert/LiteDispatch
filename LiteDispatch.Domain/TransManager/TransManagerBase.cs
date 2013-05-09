namespace LiteDispatch.Domain.TransManager
{
  using System;
  using Repository;

  /// <summary>
  /// Default transaction manager which starts a transaction when
  /// the <see cref="ExecuteCommand{TResult}"/> method is called and
  /// then a commit is invoked before the result is returned.
  /// If an exception takes place the manager rolls back the transaction
  /// </summary>
  public abstract class TransManagerBase
    : ITransManager
  {
    protected bool IsInTranx;

    protected IRepositoryLocator Locator { get; set; }

    #region ITransManager Members

    public TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
      where TResult : class
    {
      try
      {
        BeginTransaction();
        var result = command.Invoke(Locator);
        CommitTransaction();
        return result;
      }
      catch (Exception e)
      {
        // ToDo: Add login at this point  
        throw;
      }
    }

    public virtual void BeginTransaction()
    {
      IsInTranx = true;
    }

    public virtual void CommitTransaction()
    {
      IsInTranx = false;
    }

    public virtual void Rollback()
    {
      IsInTranx = false;
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected bool IsDisposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing) return;
      // free managed resources
      if (!IsDisposed && IsInTranx)
      {
        Rollback();
      }
      Locator = null;
      IsDisposed = true;
    }

    #endregion
  }
}
