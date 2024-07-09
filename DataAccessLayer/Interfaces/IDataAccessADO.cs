using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer.Interfaces
{
    public interface IDataAccessADO
    {
        public DataTable CommandExecuteReader(string sql, SqlParameter[] sqlParameters);

        public void CommandExecuteNonQuery(string sql, SqlParameter[] sqlParameters);
    }
}
