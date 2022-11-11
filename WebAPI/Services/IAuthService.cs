using Domain.DTOs;
using Domain.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<User> GetUser(string username, string password);
    
    Task<User> RegisterUser(UserRegisterDto dto);
}