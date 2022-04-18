using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using WebsocketDemo.Models;
using WebsocketDemo.Services;

namespace WebsocketDemo.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService orderService;



        public OrdersController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] Order order)
        {
            return Accepted(1);
        }



        [HttpGet("{orderNo}")]
        public async Task GetUpdateForOrder(int orderNo)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await SendEvents(webSocket, orderNo);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                    "Done", CancellationToken.None);
            }
            else
            {
                HttpContext.Response.StatusCode = 400; // Bad request
            }
        }

        private async Task SendEvents(WebSocket webSocket, int orderNo)
        {
            CheckResult result;

            do
            {
                result = orderService.GetUpdate(orderNo);
                Thread.Sleep(2000);

                if (!result.New) continue;

                var jsonMessage = $"\"{result.Update}\"";
                await webSocket.SendAsync(buffer: new ArraySegment<byte>(
                        array: Encoding.ASCII.GetBytes(jsonMessage),
                        offset: 0,
                        count: jsonMessage.Length),
                    messageType: WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken: CancellationToken.None);
            } while (!result.Finished);
        }

    }
}
