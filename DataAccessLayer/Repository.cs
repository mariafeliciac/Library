using DataAccessLayer.Interfaces;
using Model.Interfaces;

namespace DataAccessLayer
{
    public class Repository : IRepository
    {
        private IUserDAO userDAO;
        private IBookDAO bookDAO;
        private IReservationDAO reservationDAO;

        public Repository(IUserDAO userDAO, IBookDAO bookDAO, IReservationDAO reservationDAO)
        {
            this.userDAO = userDAO;
            this.bookDAO = bookDAO;
            this.reservationDAO = reservationDAO;
        }
        public void CreateBook(IBook book)
        {
            bookDAO.CreateBook(book);
        }

        public void CreateReservation(IReservation reservation)
        {
            reservationDAO.CreateReservation(reservation);
        }

        public void DeleteBook(IBook book)
        {
            bookDAO.DeleteBook(book);
        }

        public void DeleteReservation(int reservationId)
        {
            reservationDAO.DeleteReservation(reservationId);
        }

        public IEnumerable<IBook> ReadBook(IBook book)
        {
            return bookDAO.ReadBooks(book);
        }

        public IEnumerable<IReservation> ReadReservation(int? bookid, int? userid)
        {
            return reservationDAO.ReadReservations(bookid, userid);
        }

        public IUser ReadUser(string username)
        {
            return userDAO.ReadUser(username);
        }

        public IUser ReadUserById(int idUser)
        {
            return userDAO.ReadUserById(idUser);
        }

        public void UpdateBook(int bookid, IBook bookWithNewValues)
        {
            bookDAO.UpdateBook(bookid, bookWithNewValues);
        }

        public void UpdateReservation(IReservation reservation)
        {
            reservationDAO.UpdateReservation(reservation);
        }

    }
}
