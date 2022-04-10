using Microsoft.AspNetCore.Mvc;
using SSEDemo.Models;
using SSEDemo.Services;

namespace SSEDemo.Controllers
{
    [Route("api/[controller]")]
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
            Response.Headers.Add("Content-Type", "text/event-stream");
            CheckResult result;

            do
            {
                result = orderService.GetUpdate(orderNo);

                 if (!result.New) continue;

                 // data is required
                await Response.WriteAsync($"data: {result.Update}\r\r");

                await Response.Body.FlushAsync();
                await Task.Delay(2 * 1000);

            } while (!result.Finished);

            Response.Body.Close();
        }

    }
}
