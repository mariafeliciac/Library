using DataAccessLayer.DAOForXml;
using DataAccessLayer.Interfaces;
using Model.Model;
using System.Xml;

namespace DataAccessLayer.Tests
{
    [TestClass()]
    public class BookDAOForXMLTests
    {
        private string xmlPath = "..\\..\\..\\LibraryText.xml";
        private string xmlPathCopy = "..\\..\\..\\LibraryTextCopy.xml";

        private IBookDAO bookDAO;

        [TestInitialize()]
        public void SetUp()
        {
            XmlDocument doc = new XmlDocument();
            File.Copy(xmlPath, xmlPathCopy);
            doc.Load(xmlPathCopy);
            bookDAO = new BookDAOForXML(doc, xmlPathCopy);

        }

        public IBookDAO GetBookDAO()
        {
            return bookDAO;
        }

        [TestMethod()]
        public void ReadBookTest(IBookDAO bookDAO)
        {
            //Arrange

            Book bookReadExsist = new Book() { Title = "a", AuthorName = "a", AuthorSurname = "a", PublishingHouse = "a" };
            var readASingoleBookExsist = bookDAO.ReadBooks(bookReadExsist);

            Book bookNotExsist = new Book() { Title = "notExsist", AuthorName = "notExsist", AuthorSurname = "notExsist", PublishingHouse = "notExsist" };
            var readASingoleBookNotExsist = bookDAO.ReadBooks(bookNotExsist);

            int expectedResultReadASingoleBookExsist = 1;
            bool expectedResultReadASingoleBookNotExsist = false;
            int expectedResultReadAllBooks = 4;

            //Act
            int actualResultReadASingoleBookExsist = readASingoleBookExsist.ToList().Count;

            bool actualResultReadASingoleBookNotExsist = readASingoleBookNotExsist.Any();

            int actualResultReadAllBook = bookDAO.ReadBooks(new Book()).ToList().Count;

            //Assert
            Assert.AreEqual(expectedResultReadASingoleBookExsist, actualResultReadASingoleBookExsist);
            Assert.AreEqual(expectedResultReadASingoleBookNotExsist, actualResultReadASingoleBookNotExsist);
            Assert.AreEqual(expectedResultReadAllBooks, actualResultReadAllBook);

            File.Delete(xmlPathCopy);
        }


        [TestMethod()]
        public void CreateBookTest()
        {
            //Arrange

            Book book = new Book() { Title = "f", AuthorName = "f", AuthorSurname = "f", PublishingHouse = "f", Quantity = 1 };
            bookDAO.CreateBook(book);

            bool expectedResultCreateABook = true;
            int expectedResultIdCreateABook = bookDAO.ReadBooks(new Book()).ToList().Count;

            //Act
            bool actualResultCreateABook = bookDAO.ReadBooks(book).Any();

            int actualResultIdCreateABook = bookDAO.ReadBooks(book).Select(b => b.BookId).SingleOrDefault();

            //Assert
            Assert.AreEqual(expectedResultCreateABook, actualResultCreateABook);

            Assert.AreEqual(expectedResultIdCreateABook, actualResultIdCreateABook);

            File.Delete(xmlPathCopy);

        }


        [TestMethod()]
        public void UpdateBookTest()
        {
            //Arrange

            Book newBook = new Book() { Title = "z", AuthorName = "z", AuthorSurname = "z", PublishingHouse = "z", Quantity = 1 };
            var lastBook = bookDAO.ReadBooks(new Book()).ToList().LastOrDefault() ?? new Book();
            bookDAO.UpdateBook(lastBook.BookId, newBook);

            bool expectedResultNewBook = true;
            bool expectedResultLastBook = false;

            //Act
            bool actualResultNewBook = bookDAO.ReadBooks(newBook).Any();

            bool actualResultLastBook = bookDAO.ReadBooks(lastBook).Any();

            //Assert
            Assert.AreEqual(expectedResultNewBook, actualResultNewBook);
            Assert.AreEqual(expectedResultLastBook, actualResultLastBook);

            File.Delete(xmlPathCopy);

        }


        [TestMethod()]
        public void DeleteBookTest()
        {
            //Arrange

            Book deleteBookExsist = new Book() { BookId = 1 };

            bookDAO.DeleteBook(deleteBookExsist);

            bool expectedResultDeleteBookExsist = false;


            //Act
            bool actualResultDeleteBookExsist = bookDAO.ReadBooks(deleteBookExsist).Any();


            //Assert
            Assert.AreEqual(expectedResultDeleteBookExsist, actualResultDeleteBookExsist);

            File.Delete(xmlPathCopy);

        }
    }
}