using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDAO postDao;

    public PostLogic(IPostDAO postDao)
    {
        this.postDao = postDao;
    }

    public async Task<Post> CreateAsync(NewPostDTO dto)
    {
        Post? existing = await postDao.GetByTitleAsync(dto.title);
        if (existing != null)
            throw new Exception("title already exist");
        //ValidateData(newPost);
        Post toCreate = new Post
        {
            title = dto.title,
            body = dto.body
        };
        Post created = await postDao.createAsync(toCreate);
        return created;
    }
    public Task<IEnumerable<Post>> GetAsync(SearchPostDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }
}