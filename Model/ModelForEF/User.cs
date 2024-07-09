using Model.Interfaces;

namespace Model.ModelForEF;

public partial class User : IUser
{
    public int UserId { get; set; }

    public Role Role { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
