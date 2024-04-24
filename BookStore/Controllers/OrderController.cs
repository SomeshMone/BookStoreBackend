using Businesslayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _business;
        public OrderController(IOrderBusiness business)
        {
            _business = business;
        }

        [HttpPost]
        [Route("AddOrder")]

        public IActionResult Add_order(OrderModel model, int userid)
        {
            var data = _business.AddToOrder(model, userid);
            if (data != null)
            {
                return Ok(new {success=true,message="Order added successfully",Data=data});
            }
            return BadRequest(new {success=false,message="Something wrrong"});
        }


        [HttpGet]
        [Route("GetAllOrder")]


        public IActionResult Get_all_orders(int userid)
        {
            var data = _business.GetOrders(userid);
            if (data != null)
            {
                return Ok(new {success=true,message="All Orders are fetched",Data=data});
            }
            return BadRequest(new { successs = false ,message="eeror"}); 
        }


        [HttpGet]
        [Route("GetOrderPrice")]

        public IActionResult Get_order_price(int userid)
        {
            var data = _business.GetPriceInOrder(userid);
            if (data != null)
            {
                return Ok(new {success=true,message="Total ordered price equals to",Data=data});
            }
            return BadRequest(new {success=false,message="Error"});
        }


    }
}
