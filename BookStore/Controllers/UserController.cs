using Businesslayer.Interfaces;
using Businesslayer.Services;
using CommonLayer.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _business;
       
        public UserController(IUserBusiness business)
        {
            _business = business;
            
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register_User(UserModel model)
        {
            var registerdata = _business.RegisterUser(model);
            if (registerdata != null)
            {
                return Ok(new {success=true,Message="User Registration Successful",Data=registerdata});
            }
            return BadRequest(new {success=false,Message="Registration failed"});
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers() 
        {
            var users = _business.GetData();
            if (users != null)
            {
                return Ok(new { Success = true, Message = "All users found", Data = users });
            }
            else
            {
                return BadRequest(new { success = false, Message = "users not found" });
            }
        }
        [HttpPut]
        [Route("Login")]
        public IActionResult Login_user(LoginModel model)
        {
            var logindata =_business.LoginUser(model);
            if (logindata != null)
            {
                return Ok(new {sucess=true,message="Login Successfull",Data=logindata});
            }
            return BadRequest(new {success=false,message="Login failed"});
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult reset_password(string password)
        {
            var userid = User.Claims.Where(x => x.Type == "Email").FirstOrDefault().Value;

            var data = _business.ResetPassword(userid, password);
            if (data != null)
            {
                return Ok("Password Changed Sucessfully");
            }
            return BadRequest("Invalid Credentials");

        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var password = _business.ForgotPassword(email);
            if (password != null)
            {
                Send send = new Send();
                send.SendMail(password.EmailId, "Password is Trying to Changed is that you....!\nToken: " + password.token);
                Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
                //var endPoint = await bus.GetSendEndpoint(uri);
                //await endPoint.Send(ticket);


                return Ok("User Found" + "\n Token:" + password.token);

            }
            return BadRequest("Users not found");

        }

        [HttpPost("UpdateById")]
        public IActionResult UpdateUserDetails(int id,UpdateUser model) 
        {
            var data = _business.UpdateUser(id, model);
            if (data != null)
            {
                return Ok(new {success=true,message="User updated",Data=data});
            }
            else
            {
                return BadRequest("User not updated");
            }
        }

    }
}
