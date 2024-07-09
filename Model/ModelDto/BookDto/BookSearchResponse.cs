namespace Model.ModelDto.BookDto
{
    public class BookSearchResponse : BookEditRequest
    {
        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}
