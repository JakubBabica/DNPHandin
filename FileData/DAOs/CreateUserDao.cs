using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class CreateUserDao:IUserDao
{
    private readonly FileContext context;

    public CreateUserDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        // int userId = 1;
        // if (context.Users.Any())
        // {
        //     userId = context.Users.Max(u => u.Age);
        //     userId++;
        // }
        //
        // user.Age = userId;

        Console.WriteLine(user.Password + "i");
        Console.WriteLine(context);

        context.Users.Add(user);

        context.SaveChanges();
        
        Console.WriteLine(user.Username);

        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetUserAsync(SearchUserDto searchUserDto)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchUserDto.IdContains != null)
        {
            users = context.Users.Where(u => u.Id.Equals(searchUserDto.IdContains));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetUserLoginAsync(UserLoginDto userLoginDto)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        
        User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(userLoginDto.Username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        
        if (!existingUser.Password.Equals(userLoginDto.Password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }
}