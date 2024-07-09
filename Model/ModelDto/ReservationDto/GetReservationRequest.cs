using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelDto.ReservationDto
{
    public class GetReservationRequest
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public ReservationStatus? ReservationStatus { get; set; }
    }
}
