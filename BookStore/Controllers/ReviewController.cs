﻿using Businesslayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewBusiness _business;
        public ReviewController(IReviewBusiness business)
        {
            _business = business;
        }
        [HttpPost("AddReview")]
        public IActionResult Add_Review(ReviewModel model)
        {
            var review=_business.AddReview(model);
            if (review != null)
            {
                return Ok(new {success=true,message="Review Added Successfully",Data=review});
            }
            else
            {
                return BadRequest(new { success = false, message = "Something error" });
            }
        }

        [HttpGet("GetAllReviews")]

        public IActionResult Get_Reviews(int bookid)
        {
            var review = _business.GetReviews(bookid);
            if (review != null)
            {
                return Ok(new { success = true, message = "Review Added Successfully", Data = review });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something error" });
            }
        }

    }
}
