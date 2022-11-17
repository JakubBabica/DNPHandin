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
        Console.WriteLine("post file");
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
    public Task<Post> GetByIdAsync(int? id)
    {
        Console.WriteLine("get to the file");
        Post? post = context.Posts.FirstOrDefault(p =>p.Id.Equals(id));
        Console.WriteLine(post.title);
        return Task.FromResult(post);
    }
}