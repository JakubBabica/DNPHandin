using System.Security.Claims;
using Domain.DTOs;
using Domain.Models;

namespace BlazorWASM.Services;

public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<User> RegisterAsync(UserCreationDto dto);
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}