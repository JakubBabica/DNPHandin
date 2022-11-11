using System.ComponentModel.DataAnnotations;
using Domain.DTOs;
using Domain.Models;

namespace WebAPI.Services;

public class AuthService:IAuthService
{
    private readonly IList<User> users = new List<User>
    {
        new User
        {
            Age = 36,
            Email = "trmo@via.dk",
            Password = "onetwo3FOUR",
            Username = "trmo",
        },
        new User
        {
            Age = 34,
            Email = "jakob@gmail.com",
            Password = "password",
            Username = "jknr",
            
        }
    };

    public Task<User> GetUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }



    public Task<User> RegisterUser(UserRegisterDto dto)
    {
        if (string.IsNullOrEmpty(dto.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(dto.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        //User user = new User(Age = dto.Age,Domain = dto.Domain,Email = dto.Email,Name = dto.Name,Password = dto.Password,Role = dto.Role,SecurityLevel = dto.SecurityLevel,Username = dto.Username) { };
        //users.Add(user);
        
        return Task.CompletedTask as Task<User>;
    }


}