namespace LiteDispatch.EF.TransManager
{
  using DbContext;
  using Domain.TransManager;

  public class TransManagerFactoryEF
    : ITransFactory
  {
    public IModelCreator ModelCreator { get; private set; }

    public TransManagerFactoryEF(IModelCreator modelCreator)
    {
      ModelCreator = modelCreator;
    }

    #region Implementation of ITransFactory

    public ITransManager CreateManager()
    {
      return new TransManagerEF(new LiteDispatchDbContext(ModelCreator));
    }

    #endregion
  }
}
