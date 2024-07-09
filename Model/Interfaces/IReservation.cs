namespace Model.Interfaces
{
    public interface IReservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
