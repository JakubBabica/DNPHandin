namespace Domain.Models;

public class User
{
    public User(string username, string password, string email, int age)
    {
        Username = username;
        Password = password;
        Email = email;
        Age = age;
    }

    public User()
    {
        
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
  

   
}