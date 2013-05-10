namespace LiteDispatch.Domain.Services
{
  using Mappings;

  /// <summary>
  /// Provides access to services that should be accessible
  /// across all layers in the Domain project
  /// </summary>
  public class Container
  {
    /// <summary>
    /// The global context exposes services that provide functionality
    /// to all requests but don't maintain any state for each of the requests
    /// </summary>
    public static IGlobalContext GlobalContext
    {
      get { return Services.GlobalContext.Instance(); }
    }
  }
}
