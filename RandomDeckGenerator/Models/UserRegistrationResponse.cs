namespace RandomDeckGenerator.Models;

public class UserRegistrationResponse
{
    public User User { get; set; }
    public string Message { get; set; }
    public bool isSuccess{ get; set; }
}