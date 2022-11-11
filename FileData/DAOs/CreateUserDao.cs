using Application.DaoInterfaces;
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

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }
}