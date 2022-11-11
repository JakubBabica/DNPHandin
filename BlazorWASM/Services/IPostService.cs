using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<Post> Create(NewPostDTO dto);
    
    Task<ICollection<Post>> GetAsync(
        string? title, 
        int? userId, 
        bool? body
    );
    Task<IEnumerable<Post>> GetPosts(string? titleContains = null);
    
}