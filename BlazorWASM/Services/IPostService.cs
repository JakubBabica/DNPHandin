using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<Post> Create(NewPostDTO dto);
    Task<IEnumerable<Post>> GetPosts(string? titleContains = null);
    Task<Post> GetByIdAsync(int id);
}