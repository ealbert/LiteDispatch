using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteDispatch.Web.Hubs
{
  using Domain.Models;
  using Microsoft.AspNet.SignalR;

  public class LiteDispatchHub : Hub
  {
    public void Send(string name, string message)
    {
      Clients.Others.addNewMessageToPage(name, message);
    }
  }
}