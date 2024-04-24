using Businesslayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartListController : ControllerBase
    {
        private readonly ICartListBusiness _business;
        public CartListController(ICartListBusiness business)
        {
            _business = business;
        }
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddCart(int userid, CartModel model)
        {
            var data = _business.AddToCart(model, userid);
            if (data == null)
            {
                return NotFound(new {success=false,message="something error"});
            }
            return Ok(new {success=true,message="Added to Cart Successfully",Data=data});
        }

        [HttpGet]
        [Route("GetCard")]
        public IActionResult GetCard(int userid)
        {
            var data = _business.GetCartBooks(userid);
            if (data == null)
            {
                return NotFound(new { success = false, message = "something error" });
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("GetCardPrice")]
        public IActionResult GetCardPrice(int userid)
        {
            var data = _business.GetPrice(userid);
            if (data == null)
            {
                return NotFound(new { success = false, message = "something error" });
            }
            return Ok(new {success=true,message="Total cart price",Data=data});
        }

        [HttpPut]
        [Route("UpdateQuantity")]
        public IActionResult UpdateQty(int userid, CartModel model)
        {
            var data = _business.UpdateQuantity(userid, model);
            if (data == null)
            {
                return NotFound(new { success = false, message = "something error" });
            }
            return Ok(new {success=true,message="Quantity updated",Data=data});
        }

        [HttpPost]
        [Route("DeleteCart")]

        public IActionResult Delete_cart(DeleteCart model)
        {
            var data = _business.DeleteCart(model);
            if (!data)
            {
                return NotFound("Cart Not found");
            }
            return Ok(new { message = "deleted sucessfully", result = true });
        }
    }
}
