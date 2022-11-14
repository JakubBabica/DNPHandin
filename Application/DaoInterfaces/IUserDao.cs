using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<IEnumerable<User>> GetUserAsync(SearchUserDto searchUserDto);

    Task<User> GetUserLoginAsync(UserLoginDto userLoginDto);
}