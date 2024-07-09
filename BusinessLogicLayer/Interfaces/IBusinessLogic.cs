using Model.Interfaces;

namespace Model
{
    public interface IBusinessLogic
    {
        IUser Login(string username, string password);

        ResultAddBook AddBook(IBook book);

        IEnumerable<IBook> SearchBook(IBook book);

        List<BookViewModel> SearchBookWithAvailabilityInfos(IBook book);

        void UpdateBook(int bookid, IBook bookWithNewValues);

        void DeleteBook(int bookid);

        IUser GetUserByUserName(string userName);

        ReservationResult ReserveBook(int bookid, int userid);

        ReservationResult BookReturn(int bookid, int userid);

        List<ReservationViewModel> GetReservationHistory(int? bookid, int? userid, ReservationStatus? reservationStatus);

    }
}
