using Businesslayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBusiness _busines;
        public WishListController(IWishListBusiness busines)
        {
            _busines = busines;
        }

        [HttpPost]
        [Route("AddToWishList")]

        public IActionResult Add_whishlist(WishList model)
        {
            var data = _busines.AddToWishList(model);

            if (data == null)
            {
                return BadRequest(new {success=false,message="Someting error"});
            }
            return Ok(new {Success=true,mesaage="Added to WishList",Data=data});
        }

        [HttpGet]
        [Route("GetWhishList")]

        public IActionResult get_wishlist(int userid)
        {
            var data = _busines.GetWhishListBooks(userid);

            if (data == null)
            {
                return BadRequest(new { success = false, message = "Someting error" });
            }

            return Ok(new { Success = true, mesaage = "WishList items are", Data = data });
        }

        [HttpPost]
        [Route("DeleteWhishList")]

        public IActionResult delete_whishlist(WishList model)
        {
            var data = _busines.DeleteWhishlist(model);

            if (data == null)
            {
                return BadRequest(new { success = false, message = "Someting error" });
            }
            return Ok(new { message = "deleted sucessfully", result = true });
        }
    }
}
