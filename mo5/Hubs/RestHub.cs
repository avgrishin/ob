using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MO5.Hubs
{
  public class RestHub: Hub
  {
    public void getRestRet(SendRequestRet m)
    {
      //quikRepository.AddOrderRet(m);
      //quikRepository.KillOrder(m.Id, m.UserName);
    }

  }
  public class SendRequest
  {
    public int Id { get; set; }
    public int PaymId { get; set; }
  }
  
  public class SendRequestRet
  {
    public int Id { get; set; }
    public int PaymId { get; set; }
    public decimal Amount { get; set; }
  }

}