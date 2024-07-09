using Model.Interfaces;

namespace Model.ModelForEF;

public partial class Book : IBook
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public string AuthorSurname { get; set; } = null!;

    public string PublishingHouse { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
