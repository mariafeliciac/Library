using Model.Interfaces;

namespace Model.ModelForEF;

public partial class Reservation : IReservation
{
    public int ReservationId { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
