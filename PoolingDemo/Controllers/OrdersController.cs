using Microsoft.AspNetCore.Mvc;
using PoolingDemo.Models;
using PoolingDemo.Services;

namespace PoolingDemo.Controllers
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
            var result = orderService.GetUpdate(orderNo);
            if (result.New)
                return new ObjectResult(result);
            return NoContent();
        }

    }
}
