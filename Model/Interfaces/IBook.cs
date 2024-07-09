namespace Model.Interfaces
{
    public interface IBook
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string PublishingHouse { get; set; }
        public int Quantity { get; set; }
    }
}
