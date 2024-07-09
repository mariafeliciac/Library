namespace Model.ModelForEF;

public partial class VReservationsHistory
{
    public string Title { get; set; } = null!;

    public string Username { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
