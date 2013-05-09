namespace LiteDispatch.Domain.Repository
{
    /// <summary>
    /// Indicates that the store implementation requires schema or other
    /// type of initial configuration before it can be used, normally used
    /// for testing purposes to initialise the store instance
    /// </summary>
    public interface IStoreInitialiser
    {
        void ConfigureStore();
    }
}