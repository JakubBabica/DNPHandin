using Domain.Models;

namespace FileData.FileDaoImpl;

public class CreateUserDao
{
    private readonly UsersContext context;

    public CreateUserDao(UsersContext context)
    {
        this.context = context;
    }
    public Task<User> registerUserAsync(User user)
    {
        int age = 1;
        if (context.Users.Any())
        {
            age = context.Users.Max(u => u.Age);
            age++;
        }
        
        
        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }
}