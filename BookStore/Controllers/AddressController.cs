using Businesslayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Framing.Impl;
using RepositoryLayer.Entity;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBusiness _business;
        public AddressController(IAddressBusiness business)
        {
            _business = business;
        }

        [HttpPost("AddUserAddress")]
        public IActionResult Add_Address(AddressModel model)
        {
            var address = _business.AddAddress(model);
            if (address != null)
            {
                return Ok(new { success = true, message = "Adrress added Successfully", Data = address });
            }
            else
            {
                return BadRequest(new { success = false, Message = "eeror" });
            }
        }

        [HttpGet("GetUserAddresses")]
        public IActionResult Get_addreses(int userid)
        {
            var address = _business.GetAddresses(userid);
            if (address != null)
            {
                return Ok(new { success = true, message = "Adrress added Successfully", Data = address });
            }
            else
            {
                return BadRequest(new { success = false, Message = "eeror" });
            }
        }

        [HttpPut]
        [Route("UpdateAddress")]

        public IActionResult update_address(UpdateAddressModel model)
        {
            var address = _business.UpdateAddress(model);
            if (address != null)
            {
                return Ok(new { success = true, message = "Adrress Updated Successfully", Data = address });
            }
            else
            {
                return BadRequest(new { success = false, Message = "eeror" });
            }
        }

    }
}
