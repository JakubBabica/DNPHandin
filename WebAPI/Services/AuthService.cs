using System.ComponentModel.DataAnnotations;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using FileData;
using FileData.DAOs;

namespace WebAPI.Services;

public class AuthService:IAuthService
{

    private IUserLogic _userLogic;
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

    public AuthService(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    public Task<User> GetUser(UserLoginDto userLoginDto)
    {
        Console.WriteLine("this is strange");
        Task<User> user = _userLogic.GetUser( userLoginDto);
        

        return user;
    }



    public async Task<User> RegisterUser(UserCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(dto.password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        // User user = new User(dto.username,dto.password,dto.email,dto.age);
        //users.Add(user);
        //keep that

        return await _userLogic.CreateAsync(dto);;
    }


}