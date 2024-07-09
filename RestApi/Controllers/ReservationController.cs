using Microsoft.AspNetCore.Mvc;
using Model;
using Model.ModelDto.ReservationDto;


namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly IBusinessLogic businessLogic;

        public ReservationController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        [HttpGet]

        public IActionResult GetReservations([FromQuery] GetReservationRequest reservationGet)
        {
            var reservations = this.businessLogic.GetReservationHistory(reservationGet.BookId,reservationGet.UserId, reservationGet.ReservationStatus);

            if(reservations == null || reservations.Count == 0)
            {
                return NoContent();
            }
            List<GetReservationsResponse> reservationsResult = new List<GetReservationsResponse>();

            foreach (var reservation in reservations)
            {
                GetReservationsResponse reserv = new GetReservationsResponse()
                {
                    Title = reservation.Book.Title,
                    Username = reservation.User.Username,
                    StartDate = reservation.Reservation.StartDate,
                    EndDate = reservation.Reservation.EndDate,
                    ReservationStatus = reservation.ReservationStatus,
                };
                reservationsResult.Add(reserv);
            }
            return Ok(reservationsResult);

        }

        [HttpPost]
        public IActionResult CreateReservation(int bookId, int userId)
        {
            var reservationResultCreate = businessLogic.ReserveBook(bookId, userId);

            if (reservationResultCreate == null)
            {
                return StatusCode(500);
            }
            CreateReservationResponse reserv = new CreateReservationResponse()
            {
                Book = reservationResultCreate.Book,
                User = reservationResultCreate.User,
                EndDateReservation = reservationResultCreate.EndDateReservation,
                Result = reservationResultCreate.Result

            };
            return Ok(reservationResultCreate);
        }

        [HttpPut]
        public IActionResult UpdateReservation(int bookId, int userId)
        {
            var reservationUpdate = businessLogic.BookReturn(bookId, userId);

            if (reservationUpdate == null)
            {
                return StatusCode(500);
            }

            var reservation = new UpdateReservationResponse();
            reservation.Title=reservationUpdate.Book.Title;
            reservation.Result = reservationUpdate.Result;

            return Ok(reservation); 
        }

    }
}
