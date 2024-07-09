using DataAccessLayer.Interfaces;
using Microsoft.Data.SqlClient;
using Model.Interfaces;
using Model.Model;
using System.Data;

namespace DataAccessLayer.DAOForADO
{
    public class ReservationDAOForADO : IReservationDAO
    {
        private IDataAccessADO dataAccessADO;
        private string sql;

        public ReservationDAOForADO(IDataAccessADO dataAccessADO)
        {
            this.dataAccessADO = dataAccessADO;
        }
        public void CreateReservation(IReservation reservation)
        {
            sql = "CreateReservation";

            SqlParameter[] sqlParameters = new SqlParameter[]
           {
                new SqlParameter("@BookId",reservation.BookId),
                new SqlParameter("@UserId", reservation.UserId)
           };

            dataAccessADO.CommandExecuteNonQuery(sql, sqlParameters);
        }

        public void DeleteReservation(int reservationId)
        {
            sql = "DeleteReservation";

            SqlParameter[] sqlParameters = new SqlParameter[]
           {
                new SqlParameter("@ReservationId",reservationId)
           };

            dataAccessADO.CommandExecuteNonQuery(sql, sqlParameters);
        }

        public IEnumerable<IReservation> ReadReservations(int? bookid, int? userid)
        {
            List<Reservation> reservations = new List<Reservation>();
            sql = "ReadReservations";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BookId",(object)bookid ?? DBNull.Value),
                new SqlParameter("@UserId", (object)userid ?? DBNull.Value)
            };

            DataTable dt = dataAccessADO.CommandExecuteReader(sql, sqlParameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var reservation = new Reservation();
                    reservation.MapFromRow(dr);

                    reservations.Add(reservation);
                }
            }

                return reservations;
        }

        public void UpdateReservation(IReservation reservation)
        {
            sql = "UpdateReservation";

            SqlParameter[] sqlParameters = new SqlParameter[]
           {
                new SqlParameter("@ReservationId",reservation.ReservationId),
                new SqlParameter("@BookId",reservation.BookId),
                new SqlParameter("@UserId", reservation.UserId),
                new SqlParameter("@StartDate", reservation.StartDate),
                new SqlParameter("@EndDate", reservation.EndDate)
           };

            dataAccessADO.CommandExecuteNonQuery(sql, sqlParameters);
        }



    }
}
