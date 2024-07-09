namespace Model.ModelDto.UserDto
{
    public class UserLoginResponse : UserByUsernameResponse
    {
        public Role Role { get; set; }
    }
}
