namespace LiteDispatch.Domain.Services
{
  using TransManager;

  /// <summary>
  /// A global context is a place holder for service instances that are 
  /// available for all requests and should not maintain any state for
  /// the request itself
  /// </summary>
  public interface IGlobalContext
  {
    /// <summary>
    /// Provides access to <see cref="TransManager"/> instances
    /// </summary>
    ITransFactory TransFactory { get; set; }
  }
}
