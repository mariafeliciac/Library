namespace Model.ModelForEF;

public partial class BooksStaging
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public string AuthorSurname { get; set; } = null!;

    public string PublishingHouse { get; set; } = null!;

    public int Quantity { get; set; }
}
