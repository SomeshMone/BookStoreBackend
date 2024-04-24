using Businesslayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _business;
        public BookController(IBookBusiness business)
        {
            this._business = business;
        }

        [HttpGet("GetAllBooks")]
        public IActionResult GetBooks()
        {
            var books = _business.GetAllBooks();
            if (books != null)
            {
                return Ok(new { success = true, Message = "All books are fetched", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Something error!" });
            }
        }

        [HttpGet("GetByTitle")]
        public IActionResult GeBookByTitle(string title)
        {
            var books = _business.GetByTitle(title);
            if (books != null)
            {
                return Ok(new { success = true, Message = "All books are fetched", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Something error!" });
            }
        }

        [HttpGet("GetById")]

        public IActionResult Get_BY_ID(int id)
        {
            var books = _business.GetById(id);
            if (books != null)
            {
                return Ok(new { success = true, Message = "All books are fetched", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Something error!" });
            }
        }

        [HttpGet("GetByAuthor")]

        public IActionResult Get_By_Author(string author)
        {
            var books = _business.GetByAuthor(author);
            if (books != null)
            {
                return Ok(new { success = true, Message = "All books are fetched", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Something error!" });
            }
        }


        [HttpGet("AuthorTitle")]

        public IActionResult Get_By_Author_Title(string title,string author)
        {
            var books = _business.GetBookByTitleAndAuthor(title, author);
            if (books != null)
            {
                return Ok(new { success = true, Message = "All books are fetched", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Something error!" });
            }
        }

    }
}
