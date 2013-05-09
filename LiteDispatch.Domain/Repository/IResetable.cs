namespace LiteDispatch.Domain.Repository
{
  /// <remarks>
    /// For testing purposes, use it to flush off all the contents of the
    /// store
    /// </remarks>
    public interface IResetable
    {
        void Reset();
    }
}
