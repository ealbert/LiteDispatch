namespace LiteDispatch.Core.DTOs
{
  public class DispatchLineDto
  {
    public long Id { get; set; }
    public string ProductType { get; set; }
    public string Product { get; set; }
    public string Metric { get; set; }
    public int Quantity { get; set; }
    public int ShopId { get; set; }
    public string ShopLetter { get; set; }
    public string Client { get; set; } 
  }
}