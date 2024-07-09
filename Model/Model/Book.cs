using Model.Interfaces;
using System.Data;

namespace Model.Model
{

    public class Book : IBook
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string PublishingHouse { get; set; }
        public int Quantity { get; set; }


        public void MapFromRow(DataRow dataRow)
        {
            BookId = dataRow.Field<int>("BookId");
            Title = dataRow.Field<string>("Title");
            AuthorName = dataRow.Field<string>("AuthorName");
            AuthorSurname = dataRow.Field<string>("AuthorSurname");
            PublishingHouse = dataRow.Field<string>("PublishingHouse");
            Quantity = dataRow.Field<int>("Quantity");
        }
    }
}
