namespace Model.Interfaces
{
    public interface IUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
