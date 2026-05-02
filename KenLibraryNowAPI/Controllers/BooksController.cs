using KenLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KenLibraryNowAPI.Controllers
{
    [Route("api/v1/book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Flowers of Algernon",
                Author = "Daniel Keyes",
                Genre = "Science Fiction",
                Available = true,
                PublishedYear = 1959,

            },
            new Book
            {
                Id = 2,
                Title = "Weight of Ink",
                Author = "Rachel Kadish",
                Genre = "Historical Fiction",
                Available = true,
                PublishedYear = 2017,

            }
        };

        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "books retrieved"

            });
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = book,
                    message = "Book not found."
                });
            }
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books retrieved."

            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetId),
                new { id = newBook.Id },
                new
                {
                    status = "Success",
                    data = newBook,
                    message = "Book Created"
                });

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = books,
                message = "Book updated."

            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Book deleted."

            });
        }
    }
}
