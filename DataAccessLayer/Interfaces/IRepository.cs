
using Model.Interfaces;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository
    {
        IUser ReadUser(string username);

        IUser ReadUserById(int idUser);
        void CreateBook(IBook book);

        IEnumerable<IBook> ReadBook(IBook book);

        void UpdateBook(int bookid, IBook bookWithNewValues);

        void DeleteBook(IBook book);

        void CreateReservation(IReservation reservation);

        IEnumerable<IReservation> ReadReservation(int? bookid, int? userid);

        void UpdateReservation(IReservation reservation);
        void DeleteReservation(int reservationId);

    }
}
