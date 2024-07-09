namespace Model.ModelDto.BookDto
{
    public class SearchBookWithAvailabilityResponse : BookSearchResponse
    {

        public AvailabilityBook AvailabilityBook { get; set; }

        public DateTime AvailabilityDate {  get; set; }




    }
}
