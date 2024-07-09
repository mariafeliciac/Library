using DataAccessLayer.Interfaces;
using Microsoft.Data.SqlClient;
using Model.Interfaces;
using Model.Model;
using System.Data;

namespace DataAccessLayer.DAOForADO
{
    public class BookDAOForADO : IBookDAO
    {
        private IDataAccessADO dataAccessADO;
        private string sql;

        public BookDAOForADO(IDataAccessADO dataAccessADO)
        {
            this.dataAccessADO = dataAccessADO;
        }

        public void CreateBook(IBook book)
        {
            sql = "CreateBook";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Title", book.Title),
                new SqlParameter("@AuthorName", book.AuthorName),
                new SqlParameter("@AuthorSurname",book.AuthorSurname),
                new SqlParameter("@PublishingHouse", book.PublishingHouse),
                new SqlParameter("@Quantity", book.Quantity)
            };

            dataAccessADO.CommandExecuteNonQuery(sql, sqlParameters);
        }

        public void DeleteBook(IBook book)
        {
            sql = "DeleteBook";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BookId", book.BookId)
            };
            dataAccessADO.CommandExecuteNonQuery(sql, sqlParameters);
        }

        public IEnumerable<IBook> ReadAllBooksAvailable()
        {
            List<Book> books = new List<Book>();
            sql = "ReadAllBooksAvailable";

            SqlParameter[] sqlParameters = new SqlParameter[] { };

            DataTable dt = dataAccessADO.CommandExecuteReader(sql, sqlParameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var bookRead = new Book();
                    bookRead.MapFromRow(dr);

                    books.Add(bookRead);
                }
            }

            return books;
        }

        public IEnumerable<IBook> ReadBooks(IBook book)
        {
            List<Book> books = new List<Book>();
            sql = "ReadBooks";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BookId",book?.BookId),
                new SqlParameter("@Title",(object)book?.Title?.ToUpper() ?? DBNull.Value),
                new SqlParameter("@AuthorName", (object)book?.AuthorName?.ToUpper() ?? DBNull.Value),
                new SqlParameter("@AuthorSurname",(object)book?.AuthorSurname?.ToUpper() ?? DBNull.Value),
                new SqlParameter("@PublishingHouse", (object)book?.PublishingHouse?.ToUpper() ?? DBNull.Value)
            };

            DataTable dt = dataAccessADO.CommandExecuteReader(sql, sqlParameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var bookRead = new Book();
                    bookRead.MapFromRow(dr);

                    books.Add(bookRead);
                }
            }

            return books;
        }

        public void UpdateBook(int bookid, IBook bookWithNewValues)
        {
            sql = "UpdateBook";


            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BookId", bookid),
                new SqlParameter("@Title", bookWithNewValues.Title),
                new SqlParameter("@AuthorName", bookWithNewValues.AuthorName),
                new SqlParameter("@AuthorSurname",bookWithNewValues.AuthorSurname),
                new SqlParameter("@PublishingHouse", bookWithNewValues.PublishingHouse),
                new SqlParameter("@Quantity", bookWithNewValues.Quantity)
            };

            dataAccessADO.CommandExecuteNonQuery(sql, sqlParameters);
        }

     

    }
}
