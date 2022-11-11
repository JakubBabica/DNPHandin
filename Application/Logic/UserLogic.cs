using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic:IUserLogic
{
    //private readonly CreateUserDao userDaoo;
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        // User? existing = await userDao.GetByUsernameAsync(dto.username);
        // if (existing != null)
        //     throw new Exception("Username already taken!");
        
        ValidateData(dto);
        User toCreate = new User
        {
            Username = dto.username,
            Password = dto.password,
            Age = dto.age,
            Email = dto.email
        };
    
        Console.WriteLine(dto.username);

        User created = await userDao.CreateAsync(toCreate);


        return created;
    }
    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.username;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
}