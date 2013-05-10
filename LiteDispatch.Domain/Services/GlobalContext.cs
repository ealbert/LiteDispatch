namespace LiteDispatch.Domain.Services
{
  using System;
  using TransManager;

  /// <summary>
  /// A global context is a place holder for service instances that are 
  /// available for all requests and should not maintain any state for
  /// the request itself
  /// </summary> 
  public class GlobalContext
    : IGlobalContext
  {    
    private static readonly Lazy<GlobalContext> InternalInstance = new Lazy<GlobalContext>(CreateInstance, true);

    private static GlobalContext CreateInstance()
    {
      return new GlobalContext();
    }

    private GlobalContext(){}

    public static GlobalContext Instance()
    {
      return InternalInstance.Value;
    }

    #region IGlobalContext Members

    public ITransFactory TransFactory { get; set; }

    #endregion
  }
}
