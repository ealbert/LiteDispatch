namespace LiteDispatch.Domain.Models
{
  public class DispatchLineModel
  {
    public long Id { get; set; }
    public string ProductType { get; set; }
    public string Product { get; set; }
    public string Metric { get; set; }
    public int Quantity { get; set; }
    public int ShopId { get; set; }
    public string ShopLetter { get; set; }
    public string Client { get; set; }

    public string ShopTitle()
    {
      return string.IsNullOrEmpty(ShopLetter)
               ? ShopId.ToString()
               : string.Format("{0}-{1}", ShopId, ShopLetter);
    }
  }
}