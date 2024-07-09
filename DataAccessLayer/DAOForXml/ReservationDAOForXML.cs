using DataAccessLayer.Interfaces;
using Model.Interfaces;
using Model.ModelForEF;
using System.Xml;

namespace DataAccessLayer.DAOForXml
{
    public class ReservationDAOForXML : IReservationDAO
    {
        private readonly XmlDocument doc;
        private string xmlPath;
        private string nodeListReservation = "//Library/Reservations/Reservation";
        private string nodeReservations = "//Library/Reservations";

        public ReservationDAOForXML(XmlDocument xmlDocument, string xmlPath)
        {
            doc = xmlDocument;
            this.xmlPath = xmlPath;
        }
        public void CreateReservation(IReservation reservation)
        {
            XmlElement reservationNode = doc.CreateElement("Reservation");

            reservationNode.SetAttribute("Id", (ReadReservations(null, null).LastOrDefault().ReservationId + 1).ToString());
            reservationNode.SetAttribute("UserId", reservation.UserId.ToString());
            reservationNode.SetAttribute("BookId", reservation.BookId.ToString());
            reservationNode.SetAttribute("StartDate", reservation.StartDate.ToShortDateString());
            reservationNode.SetAttribute("EndDate", reservation.EndDate.ToShortDateString());

            doc.SelectSingleNode(nodeReservations).AppendChild(reservationNode);

            doc.Save(xmlPath);
        }

        public void DeleteReservation(int reservationId)
        {
            XmlNode reservationDelete = doc.SelectNodes(nodeListReservation).Cast<XmlNode>().Where(b => b.Attributes["Id"].Value.Equals(reservationId.ToString())).SingleOrDefault();

            doc.SelectSingleNode(nodeReservations).RemoveChild(reservationDelete);

            doc.Save(xmlPath);
        }

        public IEnumerable<IReservation> ReadReservations(int? bookid, int? userid)
        {
            return doc.SelectNodes(nodeListReservation).Cast<XmlNode>()
               .Where(r => (bookid == null || r.Attributes["BookId"].Value.Equals(bookid?.ToString(), StringComparison.OrdinalIgnoreCase))
                        && (userid == null || r.Attributes["UserId"].Value.Equals(userid?.ToString(), StringComparison.OrdinalIgnoreCase)))
               .Select(r => new Reservation()
               {
                   ReservationId = int.Parse(r.Attributes["Id"].Value),
                   BookId = int.Parse(r.Attributes["BookId"].Value),
                   UserId = int.Parse(r.Attributes["UserId"].Value),
                   StartDate = DateTime.Parse(r.Attributes["StartDate"].Value),
                   EndDate = DateTime.Parse(r.Attributes["EndDate"].Value),
               }).ToList();
        }

        public void UpdateReservation(IReservation reservation)
        {
            XmlElement reservationWithLastValues = doc.SelectNodes(nodeListReservation).Cast<XmlElement>().Where(b => b.Attributes["Id"].Value.Equals(reservation.ReservationId.ToString())).Single();

            reservationWithLastValues.SetAttribute("Id", reservation.ReservationId.ToString());
            reservationWithLastValues.SetAttribute("UserId", reservation.UserId.ToString());
            reservationWithLastValues.SetAttribute("BookId", reservation.BookId.ToString());
            reservationWithLastValues.SetAttribute("StartDate", reservation.StartDate.ToShortDateString());
            reservationWithLastValues.SetAttribute("EndDate", reservation.EndDate.ToShortDateString());

            doc.Save(xmlPath);
        }
    }
}
