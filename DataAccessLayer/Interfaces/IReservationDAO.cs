using Model.Interfaces;

namespace DataAccessLayer.Interfaces
{
    public interface IReservationDAO
    {
        void CreateReservation(IReservation reservation);

        IEnumerable<IReservation> ReadReservations(int? bookid, int? userid);

        void UpdateReservation(IReservation reservation);

        void DeleteReservation(int reservationId);

    }
}
