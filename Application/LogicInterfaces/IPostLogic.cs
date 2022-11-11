using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(NewPostDTO newPost);
    public Task<IEnumerable<Post>> GetAsync(SearchPostDto searchParameters);
}