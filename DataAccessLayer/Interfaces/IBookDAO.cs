using Model.Interfaces;

namespace DataAccessLayer.Interfaces
{
    public interface IBookDAO
    {
        void CreateBook(IBook book);

        IEnumerable<IBook> ReadBooks(IBook book);

        IEnumerable<IBook> ReadAllBooksAvailable();

        void UpdateBook(int bookid, IBook bookWithNewValues);

        void DeleteBook(IBook book);
    }
}
