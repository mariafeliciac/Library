using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using Model.Interfaces;

namespace DataAccessLayer.DAOForEF
{
    public class UserDAOForEF : IUserDAO
    {
        private LibraryContext dbContext;

        public UserDAOForEF(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUser ReadUser(string username)
        {
            return dbContext.Users.FirstOrDefault(u => u.Username.Equals(username));
        }

        public IUser ReadUserById(int idUser)
        {
            return dbContext.Users.Single(u => u.UserId == idUser);
        }
    }
}
