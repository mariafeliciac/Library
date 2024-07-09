using Model.Interfaces;
using System.Data;

namespace Model.Model
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public void MapFromRow(DataRow dataRow)
        {
            UserId = dataRow.Field<int>("UserId");
            Username = dataRow.Field<string>("Username");
            Password = dataRow.Field<string>("Password");
            Role = Enum.Parse<Role>(dataRow.Field<string>("Role"));
        }
    }
}
