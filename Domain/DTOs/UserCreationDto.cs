namespace Domain.DTOs;

public class UserCreationDto
{
    public string username { get; }
    public string password { get; }
    public string email { get; }
    public int age { get; }

    public UserCreationDto(string username, string password, string email, int age)
    {
        this.username = username;
        this.password = password;
        this.email = email;
        this.age = age;
    }
}