using Domain.DTOs;
using Domain.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<User> GetUser(UserLoginDto userLoginDto);
    
    Task<User> RegisterUser(UserCreationDto dto);
}