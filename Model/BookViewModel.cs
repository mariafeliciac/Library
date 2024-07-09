using Model.Interfaces;

namespace Model
{
    public class BookViewModel
    {
        public IBook Book { get; set; }
        public List<ReservationViewModel> ReservationsViewModel { get; set; }

        public AvailabilityBook AvailabilityBook
        {
            get
            {
                int countAvailabilityBook = 0;

                if (Book != null)
                {
                    foreach (ReservationViewModel reservation in ReservationsViewModel)
                    {
                        if (reservation?.Book != null)
                        {
                            if (this.Book.BookId == reservation.Book.BookId && reservation.ReservationStatus == ReservationStatus.ReservationActive)
                            {
                                countAvailabilityBook++;
                            }
                        }

                    }
                    return (countAvailabilityBook == this.Book.Quantity) ? AvailabilityBook.Unavailability : AvailabilityBook.Availability;
                }
                return 0;
            }
        }

        public DateTime AvailabilityDate
        {
            get
            {
                if (this.AvailabilityBook == AvailabilityBook.Availability)
                {
                    return DateTime.Now.Date;
                }
                else
                {
                    DateTime mostRecentEndDate = DateTime.MaxValue.Date;

                    foreach (ReservationViewModel reservationViewModel in ReservationsViewModel)
                    {
                        if (this.Book.BookId == reservationViewModel.Book.BookId && reservationViewModel.ReservationStatus == ReservationStatus.ReservationActive)
                        {
                            if (reservationViewModel.Reservation.EndDate < mostRecentEndDate)
                            {
                                mostRecentEndDate = reservationViewModel.Reservation.EndDate;
                            }
                        }
                    }
                    return mostRecentEndDate;
                }
            }
        }



    }
}
