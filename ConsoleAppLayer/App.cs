using Model;
using Model.Interfaces;

namespace ConsoleAppLayer
{
    internal class App
    {
        private readonly IBusinessLogic businessLogic;
        public App(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        public void Run(string[] args)
        {
            var user = this.Login();
            string operationUser = "";
            bool result = true;

            OperationsMenu operationsMenu = OperationsMenu.Exit;

            if (user != null)
            {
                do
                {
                    operationsMenu = this.Menu(user);

                    switch (operationsMenu)
                    {
                        case OperationsMenu.SearchBookWithAvailabilityInfos:
                            this.SearchBookWithAvailabilityInfos();
                            break;

                        case OperationsMenu.BookReservation:
                            this.BookReservation(user);
                            break;

                        case OperationsMenu.ReservationHistory:
                            this.ReservationHistory(user);
                            break;

                        case OperationsMenu.BookReturn:
                            this.BookReturn(user);
                            break;

                        case OperationsMenu.AddBook:
                            this.AddBook(user);
                            break;

                        case OperationsMenu.EditBook:
                            this.EditBook(user);
                            break;

                        case OperationsMenu.DeleteBook:
                            this.DeleteBook(user);
                            break;
                    }
                } while (result == false || operationsMenu != OperationsMenu.Exit);
            }
        }

        private OperationsMenu Menu(IUser user)
        {
            Console.WriteLine("\n\rExit (Type 0)\n\r" +
                                   "Search Book With Availability Infos (Type 1)\n\r" +
                                   "Book Reservation (Type 2)\n\rReservation History (Type 3)\n\r" +
                                   "Book Return (Type 4)");

            Console.WriteLine(user.Role == Role.Admin ? "Add Book (Type 5)\n\r" +
                                                        "Edit Book (Type 6)\n\r" +
                                                        "Delete Book (Type 7)" : "");

            Console.Write("\n\rWhich menu action do you want to perform: ");

            string operationUser = Console.ReadLine() ?? "";

            bool result = Enum.TryParse(operationUser, out OperationsMenu operationsMenu);

            return operationsMenu;
        }

        private IUser Login()
        {
            IUser user = null;
            string operationUser = "N";

            do
            {
                Console.WriteLine("\r\nHello!");
                user = businessLogic.Login(MethodProgram.InsertUserParameters("Username"), MethodProgram.InsertUserParameters("Password"));

                if (user == null)
                {
                    bool res = true;
                    do
                    {
                        Console.Write("\r\nUser not found!Do you want to re-enter credentials (Y or N): ");
                        operationUser = Console.ReadLine();

                    } while (!operationUser.Equals("n",StringComparison.OrdinalIgnoreCase) && !operationUser.Equals("y",StringComparison.OrdinalIgnoreCase));
                }

            } while (operationUser.Equals("Y", StringComparison.OrdinalIgnoreCase) && user==null);

            return user;
        }

        private void SearchBookWithAvailabilityInfos()
        {
            List<BookViewModel> searchBooksViewModel = businessLogic.SearchBookWithAvailabilityInfos(MethodProgram.InsertBookParameters(optionalParameters: true));
            if (MethodProgram.CheckListIsNullOrEmpity(searchBooksViewModel))
                searchBooksViewModel.ForEach(b => Console.WriteLine($"\r\nTitle: {b.Book.Title}, AuthorName: {b.Book.AuthorName}, AuthorSurname: {b.Book.AuthorSurname}, PublishingHouse: {b.Book.PublishingHouse}, Quantity: {b.Book.Quantity}, Availability Book: {b.AvailabilityBook}, Availability Date: {b.AvailabilityDate.ToShortDateString()}"));
        }

        private void BookReservation(IUser user)
        {
            IEnumerable<IBook> searchBooksReservation = businessLogic.SearchBook(MethodProgram.InsertBookParameters());
            if (MethodProgram.CheckListIsNullOrEmpity(searchBooksReservation))
            {
                ReservationResult reservationResult = businessLogic.ReserveBook(searchBooksReservation.Single().BookId, user.UserId);
                if (reservationResult == null)
                {
                    Console.WriteLine("\r\nReservation is not possible!\r\n");
                }
                else
                {
                    if (reservationResult.Result == true)
                    {
                        Console.WriteLine($"\r\nThe booking was successful! Book {reservationResult.Book.Title} appears to be booked until {reservationResult.EndDateReservation.ToShortDateString()}\r\n");
                    }
                    else
                    {
                        if (reservationResult.User != null && reservationResult.User.UserId == user.UserId)
                        {
                            Console.WriteLine($"\r\nThe reservation was not successful because the book {reservationResult.Book.Title} is already reserved by you until the day {reservationResult.EndDateReservation.ToShortDateString()}\r\n");
                        }
                        else
                        {
                            Console.WriteLine($"\r\nThe reservation was unsuccessful because the book {reservationResult.Book.Title} is already reserved by other users until the day {reservationResult.EndDateReservation.ToShortDateString()}\r\n");
                        }
                    }
                }
            }
        }

        private void ReservationHistory(IUser user)
        {
            IBook? bookHistory = null;
            IUser? userHistory = null;
            ReservationStatus? reservationStatus = null;
            bool result = true;
            do
            {
                result = MethodProgram.RequestTheUserForReservationHistory("Book");
                if (!result)
                {
                    IEnumerable<IBook> books = businessLogic.SearchBook(MethodProgram.InsertBookParameters());

                    result = MethodProgram.CheckListIsNullOrEmpity(books);
                    if (result && books.Count() == 1)
                        bookHistory = books.Single();
                }
            } while (!result);

            if (user.Role != Role.User)
            {
                do
                {
                    result = MethodProgram.RequestTheUserForReservationHistory("Username");
                    if (!result)
                    {
                        string usernameUser = "";
                        do
                        {
                            Console.Write("Type the user's Username: ");
                            usernameUser = Console.ReadLine() ?? "";
                        } while (string.IsNullOrWhiteSpace(usernameUser));

                        userHistory = businessLogic.GetUserByUserName(usernameUser);

                        result = userHistory == null ? false : true;

                        if (!result)
                            Console.WriteLine("\r\nNo user was found!\r\n");
                    }
                } while (!result);
            }
            else
            {
                userHistory = user;
            }

            if (!MethodProgram.RequestTheUserForReservationHistory("Reservation Status"))
            {
                Console.WriteLine("Type Y (ReservationActive)\r\nType N (ReservationNotActive)\r\nType any other key(No longer filter by Reservation Status)");
                string reservationS = Console.ReadLine() ?? "";
                if (reservationS.Equals("Y", StringComparison.OrdinalIgnoreCase) || reservationS.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    reservationStatus = reservationS.Equals("Y", StringComparison.OrdinalIgnoreCase) ? ReservationStatus.ReservationActive : ReservationStatus.ReservationNotActive;
                }
            }

            List<ReservationViewModel> reservationViewModels = businessLogic.GetReservationHistory(bookHistory?.BookId, userHistory?.UserId, reservationStatus);
            if (MethodProgram.CheckListIsNullOrEmpity(reservationViewModels))
            {
                foreach (ReservationViewModel model in reservationViewModels)
                {
                    if (model.User == null || model.Book == null)
                    {
                        Console.WriteLine("\r\nOperation is not possible!\r\n");
                        break;
                    }
                    Console.WriteLine($"Title: {model.Book.Title}, Username: {model.User.Username}, StartDate: {model.Reservation.StartDate.ToShortDateString()}, EndDate: {model.Reservation.EndDate.ToShortDateString()}, ReservationStatus: {model.ReservationStatus}");
                }
            }
        }

        private void BookReturn(IUser user)
        {
            IEnumerable<IBook> searchBooksReturn = businessLogic.SearchBook(MethodProgram.InsertBookParameters());
            if (MethodProgram.CheckListIsNullOrEmpity(searchBooksReturn) && searchBooksReturn.ToList().Count == 1)
            {
                ReservationResult reservationResult = businessLogic.BookReturn(searchBooksReturn.Single().BookId, user.UserId);
                if (reservationResult.Result == true)
                {
                    Console.WriteLine($"\r\nBook {reservationResult.Book.Title} was successfully returned!\r\n");
                }
                else
                {
                    Console.WriteLine($"\r\nBook {reservationResult.Book.Title} does not appear to be currently on loan.\r\n");
                }
            }
        }

        private void AddBook(IUser user)
        {
            if (user.Role == Role.Admin)
            {
             ResultAddBook resultAddBook = businessLogic.AddBook(MethodProgram.InsertBookParameters(quantityOn: true));
                string resultoperation;
                if (resultAddBook == ResultAddBook.Added)
                {
                   resultoperation= "Operation is successfully";
                }
                else
                {
                    resultoperation = "The only quantity is added";
                }
                Console.WriteLine($"\r\n{resultoperation}\r\n");
            }
        }

        private void EditBook(IUser user)
        {
            if (user.Role == Role.Admin)
            {
                Console.WriteLine("Look for the Book!");
                IEnumerable<IBook> searchBooksEdit = businessLogic.SearchBook(MethodProgram.InsertBookParameters());
                if (MethodProgram.CheckListIsNullOrEmpity(searchBooksEdit) && searchBooksEdit.ToList().Count == 1)
                {
                    Console.WriteLine("\r\nType the fields to edit!\r\nContinue with ENTER if you do not want to change the field.\r\n");
                    try
                    {
                        IBook bookWithLastValues = searchBooksEdit.Single();
                        IBook bookWithNewValues = MethodProgram.InsertBookParameters(optionalParameters: true);

                        businessLogic.UpdateBook(bookWithLastValues.BookId, MethodProgram.SetNewValuesBookToModify(bookWithLastValues, bookWithNewValues));
                        Console.WriteLine("\r\nBook edited successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void DeleteBook(IUser user)
        {
            if (user.Role == Role.Admin)
            {
                IEnumerable<IBook> searchBooksDelete = businessLogic.SearchBook(MethodProgram.InsertBookParameters());
                if (MethodProgram.CheckListIsNullOrEmpity(searchBooksDelete) && searchBooksDelete.Count() == 1)
                {
                    try
                    {
                        businessLogic.DeleteBook(searchBooksDelete.Single().BookId);
                        Console.WriteLine($"\r\nThe book {searchBooksDelete.Single().Title} has been deleted!\r\n");
                    }
                    catch (AggregateException ex)
                    {
                        ex.InnerExceptions.ToList().ForEach(e => Console.WriteLine(e.Message));
                    }
                }
            }
        }

    }
}
