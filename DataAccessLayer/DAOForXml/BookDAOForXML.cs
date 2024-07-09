using DataAccessLayer.Interfaces;
using Model.Interfaces;
using Model.Model;
using System.Xml;

namespace DataAccessLayer.DAOForXml
{
    public class BookDAOForXML : IBookDAO
    {
        private readonly XmlDocument doc;
        private string xmlPath;
        private string nodeListBook = "//Library/Books/Book";
        private string nodeBooks = "//Library/Books";
        public BookDAOForXML(XmlDocument xmlDocument, string xmlPath)
        {
            doc = xmlDocument;
            this.xmlPath = xmlPath;
        }
        public void CreateBook(IBook book)
        {
            XmlElement bookNode = doc.CreateElement("Book");

            bookNode.SetAttribute("BookId", (ReadBooks(new Book()).Select(b => b.BookId).LastOrDefault() + 1).ToString());
            bookNode.SetAttribute("Title", book.Title);
            bookNode.SetAttribute("AuthorName", book.AuthorName);
            bookNode.SetAttribute("AuthorSurname", book.AuthorSurname);
            bookNode.SetAttribute("PublishingHouse", book.PublishingHouse);
            bookNode.SetAttribute("Quantity", book.Quantity.ToString());

            doc.SelectSingleNode(nodeBooks).AppendChild(bookNode);

            doc.Save(xmlPath);
        }

        public void DeleteBook(IBook book)
        {
            XmlNode xmlNodeRemoveBook = doc.SelectNodes(nodeListBook).Cast<XmlNode>().Where(b => b.Attributes["BookId"].Value.Equals(book.BookId.ToString())).SingleOrDefault();

            doc.SelectSingleNode(nodeBooks).RemoveChild(xmlNodeRemoveBook);

            doc.Save(xmlPath);
        }

        public IEnumerable<IBook> ReadAllBooksAvailable()
        {
            var booksQuantities = doc.SelectNodes("//Library/Reservations/Reservation").Cast<XmlNode>().Where(r => DateTime.Parse(r.Attributes["EndDate"].Value).Date >= DateTime.Now.Date)
                                  .GroupBy(r => r.Attributes["BookId"].Value).Select(r => new
                                  {
                                      Quantity = r.Count(),
                                      BookId = r.Key
                                  });

            return doc.SelectNodes(nodeListBook).Cast<XmlNode>().Select(b => new
            {
                Book = new Book
                {
                    BookId = int.Parse(b.Attributes["BookId"].Value),
                    Title = b.Attributes["Title"].Value,
                    AuthorName = b.Attributes["AuthorName"].Value,
                    AuthorSurname = b.Attributes["AuthorSurname"].Value,
                    PublishingHouse = b.Attributes["PublishingHouse"].Value,
                    Quantity = int.Parse(b.Attributes["Quantity"].Value)
                },
                QuantityReservation = booksQuantities.Where(bq => bq.BookId.Equals(b.Attributes["BookId"].Value)).Select(bq => bq.Quantity).FirstOrDefault(),
            })
                .Where(r => r.Book.Quantity > r.QuantityReservation || r.QuantityReservation == 0)
                .Select(r => r.Book).ToList();
        }

        public IEnumerable<IBook> ReadBooks(IBook book)
        {
            return doc.SelectNodes(nodeListBook).Cast<XmlNode>().Where(b => book == null ||
                                                           ((string.IsNullOrEmpty(book.Title) || b.Attributes["Title"].Value.Equals(book.Title, StringComparison.OrdinalIgnoreCase))
                                                        && (string.IsNullOrEmpty(book.AuthorName) || b.Attributes["AuthorName"].Value.Equals(book.AuthorName, StringComparison.OrdinalIgnoreCase))
                                                        && (string.IsNullOrEmpty(book.AuthorSurname) || b.Attributes["AuthorSurname"].Value.Equals(book.AuthorSurname, StringComparison.OrdinalIgnoreCase))
                                                        && (string.IsNullOrEmpty(book.PublishingHouse) || b.Attributes["PublishingHouse"].Value.Equals(book.PublishingHouse, StringComparison.OrdinalIgnoreCase))
                                                        && (book.BookId == 0 || b.Attributes["BookId"].Value.Equals(book.BookId.ToString()))))
                                               .Select(b => new Book()
                                               {
                                                   BookId = int.Parse(b.Attributes["BookId"].Value),
                                                   Title = b.Attributes["Title"].Value,
                                                   AuthorName = b.Attributes["AuthorName"].Value,
                                                   AuthorSurname = b.Attributes["AuthorSurname"].Value,
                                                   PublishingHouse = b.Attributes["PublishingHouse"].Value,
                                                   Quantity = int.Parse(b.Attributes["Quantity"].Value)
                                               }).ToList();
        }

        public void UpdateBook(int bookid, IBook bookWithNewValues)
        {
            XmlElement bookWithLastValues = doc.SelectNodes(nodeListBook).Cast<XmlElement>().Where(b => b.Attributes["BookId"].Value.Equals(bookid.ToString())).Single();

            bookWithLastValues.SetAttribute("BookId", bookid.ToString());
            bookWithLastValues.SetAttribute("Title", bookWithNewValues.Title);
            bookWithLastValues.SetAttribute("AuthorName", bookWithNewValues.AuthorName);
            bookWithLastValues.SetAttribute("AuthorSurname", bookWithNewValues.AuthorSurname);
            bookWithLastValues.SetAttribute("PublishingHouse", bookWithNewValues.PublishingHouse);
            bookWithLastValues.SetAttribute("Quantity", bookWithNewValues.Quantity.ToString());

            doc.Save(xmlPath);
        }

    }
}
