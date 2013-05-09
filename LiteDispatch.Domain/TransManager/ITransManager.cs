namespace LiteDispatch.Domain.TransManager
{
  using System;
  using Repository;

  /// <summary>
  /// A transaction manager is responsible for the execution of function within
  /// a well managed persistence transaction.
  /// </summary>
  public interface ITransManager
    : IDisposable
  {
    /// <summary>
    /// Entry point for executing a function that requires a <see cref="IRepositoryLocator"/> instance
    /// </summary>
    /// <typeparam name="TResult">Function result type</typeparam>
    /// <param name="command">Function instance to be executed</param>
    /// <returns>Function result instance</returns>
    TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command) where TResult : class;

    #region Transaction Methods
    
    void BeginTransaction();
    void CommitTransaction();
    void Rollback();

    #endregion
    
  }
}
