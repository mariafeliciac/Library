using Model.Interfaces;

namespace DataAccessLayer.Interfaces
{
    public interface IUserDAO
    {
        IUser ReadUser(string username);

        IUser ReadUserById(int idUser);

    }
}
