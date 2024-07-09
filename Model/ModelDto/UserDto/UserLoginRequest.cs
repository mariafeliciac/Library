namespace Model.ModelDto.UserDto
{
    public class UserLoginRequest : UserByUsernameResponse
    {
        public string Password { get; set; }

    }
}
