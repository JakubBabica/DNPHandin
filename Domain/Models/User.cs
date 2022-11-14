namespace Domain.Models;

public class User
{
    public User(int id,string username, string password, string email, int age)
    {
        Id = id;
        Username = username;
        Password = password;
        Email = email;
        Age = age;
    }

    public User()
    {
        
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
  

   
}