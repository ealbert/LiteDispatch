namespace LiteDispatch.Web.Hubs
{
  using Microsoft.AspNet.SignalR;

  public class LiteDispatchHub : Hub
  {
    public void Send(string name, string message)
    {
      Clients.Others.addNewMessageToPage(name, message);
    }
  }
}