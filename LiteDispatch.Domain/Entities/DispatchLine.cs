namespace LiteDispatch.Domain.Entities
{
  using System;
  using AutoMapper;
  using Models;
  using Repository;

  public class DispatchLine
    : EntityBase
  {
    protected DispatchLine()
    {

    }

    public static DispatchLine Create(IRepositoryLocator locator, DispatchLineModel dispatchLineModel)
    {
      var instance = Mapper.Map<DispatchLineModel, DispatchLine>(dispatchLineModel);
      return locator.Save(instance);
    }

    public string ProductType { get; private set; }
    public string Product { get; private set; }
    public string Metric { get; private set; }
    public int Quantity { get; private set; }
    public int ShopId { get; private set; }
    public string ShopLetter { get; private set; }
    public string Client { get; private set; }
    
  }
}