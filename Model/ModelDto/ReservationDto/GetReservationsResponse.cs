namespace Model.ModelDto.ReservationDto
{
    public class GetReservationsResponse
    {
        public int ReservationId { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}
