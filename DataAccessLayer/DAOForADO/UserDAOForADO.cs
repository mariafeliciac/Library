using DataAccessLayer.Interfaces;
using Microsoft.Data.SqlClient;
using Model;
using Model.Interfaces;
using Model.Model;
using System.Data;

namespace DataAccessLayer.DAOForADO
{
    public class UserDAOForADO : IUserDAO
    {
        private IDataAccessADO dataAccessADO;
        private string sql;

        public UserDAOForADO(IDataAccessADO dataAccessADO)
        {
            this.dataAccessADO = dataAccessADO;
        }

        public IUser ReadUser(string username)
        {
            List<User> users = new List<User>();
            sql = "ReadUser";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };

            DataTable dt = dataAccessADO.CommandExecuteReader(sql, sqlParameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var userRead = new User();
                    userRead.MapFromRow(dr);

                    users.Add(userRead);
                }
            }

            return users.FirstOrDefault();
        }

        public IUser ReadUserById(int idUser)
        {
            List<User> users = new List<User>();
            sql = "ReadUserById";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", idUser)
            };

            DataTable dt = dataAccessADO.CommandExecuteReader(sql, sqlParameters);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var userRead = new User();
                    userRead.MapFromRow(dr);

                    users.Add(userRead);
                }
            }

            return users.FirstOrDefault();
        }



   
    }
}
