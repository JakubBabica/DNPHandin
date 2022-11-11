using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDAO
{
    Task<Post> createAsync(Post post);
    public Task<IEnumerable<Post>> GetAsync(SearchPostDto searchParameters);
    Task<Post?> GetByTitleAsync(string title);
}