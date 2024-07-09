using Model.Interfaces;

namespace Model
{
    public class ReservationResult
    {
        public IBook Book { get; set; }
        public IUser User { get; set; }
        public DateTime EndDateReservation { get; set; }
        public bool Result { get; set; }
    }
}
