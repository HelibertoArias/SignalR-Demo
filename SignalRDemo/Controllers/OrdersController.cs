using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Hubs;
using SignalRDemo.Models;
using System.Net.WebSockets;

namespace WebsocketDemo.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IHubContext<OrdersHub> ordersHub;

        public OrdersController(IHubContext<OrdersHub> ordersHub)
        {
            this.ordersHub = ordersHub;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            await ordersHub.Clients.All.SendAsync("NewOrder", order);

            return Accepted(1);
        }

    }
}
