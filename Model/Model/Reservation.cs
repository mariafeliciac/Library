using Model.Interfaces;
using System.Data;

namespace Model.Model
{
    public class Reservation : IReservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public void MapFromRow(DataRow dataRow)
        {
            ReservationId = dataRow.Field<int>("ReservationId");
            UserId = dataRow.Field<int>("UserId");
            BookId = dataRow.Field<int>("BookId");
            StartDate = dataRow.Field<DateTime>("StartDate");
            EndDate = dataRow.Field<DateTime>("EndDate");
        }
    }
}
