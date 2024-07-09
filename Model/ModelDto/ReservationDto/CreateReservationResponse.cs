using Model.Interfaces;

namespace Model.ModelDto.ReservationDto
{
    public class CreateReservationResponse
    {
        public IBook Book { get; set; }
        public IUser User { get; set; }
        public DateTime EndDateReservation { get; set; }
        public bool Result { get; set; }
    }
}
