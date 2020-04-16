using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backendapi.Models;
using backendapi.Services;


namespace backendapi.Controllers
{
    
    /**BUILD THE BASE CONTROLLER**/


    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;

        }

        /** METHODS ARE ANNOTATED BASED ON HTTP TYPE **/

        [HttpGet]
        public ActionResult<List<Book>> Get() => _bookService.Get();


        /** WE CAN HAVE METHODS WITH PARAMETERS **/

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

    }
}
