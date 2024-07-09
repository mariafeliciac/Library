using Model.Interfaces;

namespace Model
{
    public class ReservationViewModel
    {
        public IBook Book { get; set; }

        public IUser User { get; set; }

        public IReservation Reservation { get; set; }
        public ReservationStatus ReservationStatus
        {
            get
            {
                if (Reservation != null)
                {
                    return (Reservation.EndDate.Date > DateTime.Now.Date) ? ReservationStatus.ReservationActive : ReservationStatus.ReservationNotActive;
                }
                return 0;
            }
        }
    }
}
