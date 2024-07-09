using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Model;
using Model.ModelDto.BookDto;


namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBusinessLogic businessLogic;

        public BookController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        [HttpGet("BooksWithAvailability")]
        public IActionResult GetBooksWithAvailability([FromQuery] BookSearchRequest booksearch)
        {
            var bookSearch = new Book { Title = booksearch.Title, AuthorName = booksearch.AuthorName, AuthorSurname = booksearch.AuthorSurname, PublishingHouse = booksearch.PublishingHouse };
            var books = businessLogic.SearchBookWithAvailabilityInfos(bookSearch);

            if (books == null || books.Count == 0)
            {
                return NoContent();
            }

            var booksWithAvailable = new List<SearchBookWithAvailabilityResponse>();

            foreach (BookViewModel book in books)
            {
                var bookWithAvailable = new SearchBookWithAvailabilityResponse();

                bookWithAvailable.Title = book.Book.Title;
                bookWithAvailable.AuthorName = book.Book.AuthorName;
                bookWithAvailable.AuthorSurname = book.Book.AuthorSurname;
                bookWithAvailable.PublishingHouse = book.Book.PublishingHouse;
                bookWithAvailable.Quantity = book.Book.Quantity;
                bookWithAvailable.AvailabilityBook = book.AvailabilityBook;
                bookWithAvailable.AvailabilityDate = book.AvailabilityDate;

                booksWithAvailable.Add(bookWithAvailable);
            }

            return Ok(booksWithAvailable);
        }

        [HttpGet]
        public IActionResult GetBooks([FromQuery] BookSearchRequest booksearch)
        {
            var bookSearch = new Book { Title = booksearch.Title, AuthorName = booksearch.AuthorName, AuthorSurname = booksearch.AuthorSurname, PublishingHouse = booksearch.PublishingHouse };
            var books = businessLogic.SearchBookWithAvailabilityInfos(bookSearch);

            if (books == null || books.Count == 0)
            {
                return NoContent();
            }

            var booksSearch = new List<BookSearchResponse>();

            foreach (BookViewModel book in books)
            {
                var bookSearchres = new BookSearchResponse();

                bookSearchres.BookId = book.Book.BookId;
                bookSearchres.Title = book.Book.Title;
                bookSearchres.AuthorName = book.Book.AuthorName;
                bookSearchres.AuthorSurname = book.Book.AuthorSurname;
                bookSearchres.PublishingHouse = book.Book.PublishingHouse;
                bookSearchres.Quantity = book.Book.Quantity;
                booksSearch.Add(bookSearchres);
            }

            return Ok(booksSearch);
        }

        [HttpPost]
        public IActionResult AddBook(BookAddRequest bookAdd)
        {
            var ResultAddBook = businessLogic.AddBook(new Book() { Title = bookAdd.Title, AuthorName = bookAdd.AuthorName, AuthorSurname = bookAdd.AuthorSurname, PublishingHouse = bookAdd.PublishingHouse, Quantity = bookAdd.Quantity });

            if (ResultAddBook == 0)
            {
                return StatusCode(500);
            }

            return Ok(ResultAddBook);

        }

        [HttpPut]
        public IActionResult EditBook(int lastBookId, BookEditRequest bookEdit)
        {
            try
            {
                var bookEditinput = new Book { Title = bookEdit.Title, AuthorName = bookEdit.AuthorName, AuthorSurname = bookEdit.AuthorSurname, PublishingHouse = bookEdit.PublishingHouse };
                businessLogic.UpdateBook(lastBookId, bookEditinput);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                businessLogic.DeleteBook(bookId);
                return NoContent();
            }
            catch (AggregateException ex)
            {
                var listMessageError = new List<string>();
                ex.InnerExceptions.ToList().ForEach(e => listMessageError.Add(e.Message));

                return StatusCode(500, listMessageError);
            }
        }

    }
}
