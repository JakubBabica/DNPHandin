using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.FileDaoImpl;

public class PostFileDTO:IPostDAO
{
    private readonly FileContext context;

    public PostFileDTO(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> createAsync(Post post)
    {
        int postId = 1;
        if (context.Posts.Any())
        {
            postId = context.Posts.Max(p => p.Id);
            postId++;
        }

        post.Id = postId;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }
    
    public Task<IEnumerable<Post>> GetAsync(SearchPostDto searchParameters)
    {
        IEnumerable<Post> posts = context.Posts.AsEnumerable();
        if (searchParameters.IdContains != null)
        {
            posts = context.Posts.Where(p =>p.Id.Equals(searchParameters.IdContains));
        }

        return Task.FromResult(posts);
    }

    public Task<Post?> GetByTitleAsync(string title)
    {
        Post? existing = context.Posts.FirstOrDefault(p =>
            p.title.Equals(title, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }
}