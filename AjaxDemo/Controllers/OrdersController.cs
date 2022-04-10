using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public ActionResult PostOrder([FromBody] Order order)
        {
            return Accepted(1);
        }

    }
}
