using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using Model.Interfaces;
using Model.ModelForEF;

namespace DataAccessLayer.DAOForEF
{
    public class BookDAOForEF : IBookDAO
    {
        private LibraryContext dbContext;

        public BookDAOForEF(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CreateBook(IBook book)
        {
            dbContext.Books.Add((Book)book);

            dbContext.SaveChanges();
        }

        public void DeleteBook(IBook book)
        {
            Book bookDelete = dbContext.Books.SingleOrDefault(b => b.BookId == book.BookId);

            dbContext.Books.Remove(bookDelete);

            dbContext.SaveChanges();
        }

        public IEnumerable<IBook> ReadAllBooksAvailable()
        {
            return dbContext.Books.Where(b => !b.Reservations.Any() || b.Reservations.Where(r => r.EndDate.Date >= DateTime.Now.Date).Count() != b.Quantity).ToList();
        }

        public IEnumerable<IBook> ReadBooks(IBook book)
        {
            return dbContext.Books.Where(b => book==null || ((string.IsNullOrEmpty(book.Title) || b.Title.ToUpper().Equals(book.Title.ToUpper()))
                                                        && (string.IsNullOrEmpty(book.AuthorName) || b.AuthorName.ToUpper().Equals(book.AuthorName.ToUpper()))
                                                        && (string.IsNullOrEmpty(book.AuthorSurname) || b.AuthorSurname.ToUpper().Equals(book.AuthorSurname.ToUpper()))
                                                        && (string.IsNullOrEmpty(book.PublishingHouse) || b.PublishingHouse.ToUpper().Equals(book.PublishingHouse.ToUpper()))
                                                        && (book.BookId == 0 || b.BookId == book.BookId)))
                                               .Select(b => new Book()
                                               {
                                                   BookId = b.BookId,
                                                   Title = b.Title,
                                                   AuthorName = b.AuthorName,
                                                   AuthorSurname = b.AuthorSurname,
                                                   PublishingHouse = b.PublishingHouse,
                                                   Quantity = b.Quantity,
                                               }).ToList();

        }

        public void UpdateBook(int bookid, IBook bookWithNewValues)
        {
            Book bookUpdate = dbContext.Books.Single(b => b.BookId == bookid);

            bookUpdate.Title = bookWithNewValues.Title;
            bookUpdate.AuthorName = bookWithNewValues.AuthorName;
            bookUpdate.AuthorSurname = bookWithNewValues.AuthorSurname;
            bookUpdate.PublishingHouse = bookWithNewValues.PublishingHouse;
            bookUpdate.Quantity = bookWithNewValues.Quantity;

            dbContext.Books.Update(bookUpdate);

            dbContext.SaveChanges();
        }
    }
}
