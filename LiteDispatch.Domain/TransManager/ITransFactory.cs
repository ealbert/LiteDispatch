namespace LiteDispatch.Domain.TransManager
{
  /// <summary>
  /// A transaction factory is responsible for the creation of
  /// <see cref="ITransManager"/> instances, it is assumed that
  /// a single instance of the <see cref="ITransFactory"/> would be
  /// created for the application
  /// </summary>
  public interface ITransFactory
  {
    ITransManager CreateManager();
  }
}
