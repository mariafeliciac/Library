using DataAccessLayer.Interfaces;
using Model;
using Model.Interfaces;
using Model.Model;

namespace BusinessLogicLayer.Interfaces
{
    public class BusinessLogic : IBusinessLogic
    {
        private IRepository repository;


        public BusinessLogic(IRepository repository)
        {
            this.repository = repository;
            
        }




        public ResultAddBook AddBook(IBook book)
        {
            var books = repository.ReadBook(book).ToList();
            if (books == null || books.Count == 0)
            {
                repository.CreateBook(book);
                return ResultAddBook.Added;
            }
            else
            {
                IBook bookRead = books.Single();
                book.Quantity = book.Quantity + bookRead.Quantity;

                repository.UpdateBook(bookRead.BookId, book);
                return ResultAddBook.Updated;
            }
        
    }




    public ReservationResult BookReturn(int bookid, int userid)
    {
        List<ReservationViewModel> reservationsViewModelReturn = this.GetReservationHistory(bookid, userid, null);

        BookViewModel returnBook = this.SearchBookWithAvailabilityInfos(new Book() { BookId = bookid }).Single();

        if (reservationsViewModelReturn != null && reservationsViewModelReturn.Any(r => r.ReservationStatus == ReservationStatus.ReservationActive))
        {
            ReservationViewModel reservationViewModelReturn = reservationsViewModelReturn.Where(r => r.ReservationStatus == ReservationStatus.ReservationActive).SingleOrDefault();
            reservationViewModelReturn.Reservation.EndDate = DateTime.Now.Date;

            repository.UpdateReservation(reservationViewModelReturn.Reservation);
            return new ReservationResult() { Book = returnBook.Book, EndDateReservation = reservationViewModelReturn.Reservation.EndDate, Result = true };
        }
        return new ReservationResult() { Book = returnBook.Book, EndDateReservation = returnBook.AvailabilityDate, Result = false };
    }




    public void DeleteBook(int bookid)
    {
        List<ReservationViewModel> reservationsViewModelDelete = this.GetReservationHistory(bookid, null, null);

        List<Exception> exceptionsReservationActive = new List<Exception>();

        if (reservationsViewModelDelete != null)
        {
            if (reservationsViewModelDelete.Any(r => r.ReservationStatus == ReservationStatus.ReservationActive))
            {
                foreach (ReservationViewModel reservationViewModel in reservationsViewModelDelete)
                {
                    if (reservationViewModel.ReservationStatus == ReservationStatus.ReservationActive)
                    {
                        exceptionsReservationActive.Add(new Exception($"The cancellation was not carried out as the book {reservationViewModel.Book.Title} was still booked by user {reservationViewModel.User.Username} starting from {reservationViewModel.Reservation.StartDate.ToShortDateString()} until {reservationViewModel.Reservation.EndDate.ToShortDateString()}"));
                    }
                }
                throw new AggregateException(exceptionsReservationActive);
            }
            reservationsViewModelDelete.ForEach(reservationViewModel => repository.DeleteReservation(reservationViewModel.Reservation.ReservationId));
        }

        repository.DeleteBook(new Book() { BookId = bookid });
    }




    public List<ReservationViewModel> GetReservationHistory(int? bookid, int? userid, ReservationStatus? reservationStatus)
    {
        return (from r in repository.ReadReservation(bookid, userid)
                select new ReservationViewModel()
                {
                    User = repository.ReadUserById(r.UserId),
                    Book = repository.ReadBook(new Book() { BookId = r.BookId }).Single(),
                    Reservation = r
                }).Where(a => a.ReservationStatus == reservationStatus || reservationStatus == null).ToList();
    }




    public IUser GetUserByUserName(string userName)
    {
        return repository.ReadUser(userName);
    }




    public IUser Login(string username, string password)
    {
        IUser user = repository.ReadUser(username);

        if (user != null)
        {
            return (user.Password == password) ? user : null;
        }
        return user;

    }




    public ReservationResult ReserveBook(int bookid, int userid)
    {
        List<ReservationViewModel> reservationsViewModel = this.GetReservationHistory(bookid, userid, null);

        BookViewModel reservationBookViewModel = this.SearchBookWithAvailabilityInfos(new Book() { BookId = bookid }).Single();

        if (reservationsViewModel != null)
        {
            if (reservationsViewModel.Any(r => r.ReservationStatus == ReservationStatus.ReservationActive))
            {
                DateTime endDateReservation = reservationsViewModel.Where(r => r.ReservationStatus == ReservationStatus.ReservationActive).Single().Reservation.EndDate;
                return new ReservationResult() { Book = reservationBookViewModel.Book, User = repository.ReadUserById(userid), EndDateReservation = endDateReservation, Result = false };
            }

            if (reservationBookViewModel.AvailabilityBook == AvailabilityBook.Unavailability)
                return new ReservationResult() { Book = reservationBookViewModel.Book, EndDateReservation = reservationBookViewModel.AvailabilityDate, Result = false };
        }

        repository.CreateReservation(new Reservation() { BookId = bookid, UserId = userid, StartDate = DateTime.Now.Date, EndDate = DateTime.Now.AddDays(30).Date });
        return new ReservationResult() { Book = reservationBookViewModel.Book, EndDateReservation = DateTime.Now.AddDays(30).Date, Result = true };
    }




    public IEnumerable<IBook> SearchBook(IBook book)
    {
        return repository.ReadBook(book);
    }




    public List<BookViewModel> SearchBookWithAvailabilityInfos(IBook book)
    {
        return repository.ReadBook(book).Select(b => new BookViewModel()
        {
            Book = b,
            ReservationsViewModel = this.GetReservationHistory(b.BookId, null, null)
        }).ToList();
    }




    public void UpdateBook(int bookid, IBook bookWithNewValues)
    {
        IEnumerable<IBook> exsistBooksListWithNewValues = repository.ReadBook(bookWithNewValues);

        if (exsistBooksListWithNewValues != null && exsistBooksListWithNewValues.Any())
        {
            throw new Exception("\r\nDuplicate book! Editing is not possible!\r\n");
        }
        else
        {
            IBook bookWithLastValues = repository.ReadBook(new Book() { BookId = bookid }).Single();
            bookWithNewValues.Quantity = bookWithLastValues.Quantity;
            repository.UpdateBook(bookid, bookWithNewValues);
        }
    }
}
}

