using DataAccessLayer.Interfaces;
using Model;
using Model.Interfaces;
using Model.ModelForEF;
using System.Xml;

namespace DataAccessLayer.DAOForXml
{
    public class UserDAOForXML : IUserDAO
    {
        private readonly XmlDocument doc;
        private string nodeList = "//Library/Users/User";

        public UserDAOForXML(XmlDocument xmlDocument)
        {
            doc = xmlDocument;
        }


        public IUser ReadUser(string username)
        {
            return doc.SelectNodes(nodeList).Cast<XmlNode>().Where(n => n.Attributes["Username"].Value.Equals(username))
                                                             .Select(n => new User
                                                             {
                                                                 UserId = int.Parse(n.Attributes["UserId"].Value),
                                                                 Role = Enum.Parse<Role>(n.Attributes["Role"].Value),
                                                                 Username = n.Attributes["Username"].Value,
                                                                 Password = n.Attributes["Password"].Value
                                                             }).SingleOrDefault();
        }

        public IUser ReadUserById(int idUser)
        {
            return doc.SelectNodes(nodeList).Cast<XmlNode>().Where(n => n.Attributes["UserId"].Value.Equals(idUser.ToString()))
                                                                .Select(n => new User
                                                                {
                                                                    UserId = int.Parse(n.Attributes["UserId"].Value),
                                                                    Role = Enum.Parse<Role>(n.Attributes["Role"].Value),
                                                                    Username = n.Attributes["Username"].Value,
                                                                    Password = n.Attributes["Password"].Value
                                                                }).SingleOrDefault();
        }
    }
}
