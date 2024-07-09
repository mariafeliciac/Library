using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using Model.Interfaces;
using Model.ModelForEF;

namespace DataAccessLayer.DAOForEF
{
    public class ReservationDAOForEF : IReservationDAO
    {
        private LibraryContext dbContext;

        public ReservationDAOForEF(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateReservation(IReservation reservation)
        {
            dbContext.Reservations.Add((Reservation)reservation);

            dbContext.SaveChanges();
        }

        public void DeleteReservation(int reservationId)
        {
            Reservation reservationDelete = dbContext.Reservations.SingleOrDefault(r => r.ReservationId == reservationId);

            dbContext.Reservations.Remove(reservationDelete);

            dbContext.SaveChanges();
        }

        public IEnumerable<IReservation> ReadReservations(int? bookid, int? userid)
        {
            return dbContext.Reservations
              .Where(r => (bookid == null || r.BookId == bookid)
                       && (userid == null || r.UserId == userid))
              .Select(r => new Reservation()
              {
                  ReservationId = r.ReservationId,
                  BookId = r.BookId,
                  UserId = r.UserId,
                  StartDate = r.StartDate,
                  EndDate = r.EndDate,
              }).ToList();

        }

        public void UpdateReservation(IReservation reservation)
        {
            Reservation reservationUpdate = dbContext.Reservations.Single(r => r.ReservationId == reservation.ReservationId);

            reservationUpdate.UserId = reservation.UserId;
            reservationUpdate.BookId = reservation.BookId;
            reservationUpdate.StartDate = reservation.StartDate;
            reservationUpdate.EndDate = reservation.EndDate;

            dbContext.Reservations.Update(reservationUpdate);

            dbContext.SaveChanges();
        }
    }
}
