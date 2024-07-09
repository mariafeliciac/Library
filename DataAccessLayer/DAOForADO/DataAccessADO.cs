using DataAccessLayer.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer.DAOForADO
{
    public class DataAccessADO : IDataAccessADO
    {
        private readonly string connectionString;

        public DataAccessADO(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public DataTable CommandExecuteReader(string sql, SqlParameter[] sqlParameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(sqlParameters);

                    sqlConnection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            return dt;
        }


        public void CommandExecuteNonQuery(string sql, SqlParameter[] sqlParameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(sqlParameters);

                    sqlConnection.Open();

                    var rowsAffected = command.ExecuteNonQuery();
                }
            }
        }


    }
}
