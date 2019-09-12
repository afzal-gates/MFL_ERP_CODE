using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ERP.Model;
using System.Data;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ERPSolution.Hubs {
  [HubName("dashboard")]
    public class DashBoardHub : Hub
    {
    private static ConcurrentDictionary<string, List<int>> _mapping = new ConcurrentDictionary<string, List<int>>();
    public override Task OnConnected() {
      _mapping.TryAdd(Context.ConnectionId, new List<int>());
      Clients.All.newConnection(Context.ConnectionId);
      return base.OnConnected();
    }
    public override Task OnDisconnected(bool stopCalled)
    {
      var list = new List<int>();
      _mapping.TryRemove(Context.ConnectionId, out list);
	  Clients.All.removeConnection(Context.ConnectionId);
      return base.OnDisconnected(stopCalled);
    }

 

  }
}