using LongPollingDemo.Services;
using Microsoft.AspNetCore.Mvc;
using PoolingDemo.Models;

namespace LongPollingDemo.Controllers
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
        public IActionResult GetUpdateForOrder(int orderNo)
        {
            CheckResult result;
            do
            {
                result = orderService.GetUpdate(orderNo);
                Thread.Sleep(3000);
            } while (!result.New);
             
            return new ObjectResult(result);
        }

    }
}
