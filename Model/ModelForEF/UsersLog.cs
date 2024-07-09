namespace Model.ModelForEF;

public partial class UsersLog
{
    public int LogId { get; set; }

    public int UserId { get; set; }

    public string Role { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string OperationDescription { get; set; } = null!;

    public DateTime OperationDate { get; set; }
}
