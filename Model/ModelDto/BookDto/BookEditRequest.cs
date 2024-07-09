using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace Model.ModelDto.BookDto
{
    public class BookEditRequest
    {
        
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string AuthorSurname { get; set; }

        public string PublishingHouse { get; set; }
    }
}
