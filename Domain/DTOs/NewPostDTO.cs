namespace Domain.DTOs;

public class NewPostDTO
{
    public string title { get; }
    public string body { get; }

    public NewPostDTO(string title, string body)
    {
        this.title = title;
        this.body = body;
    }
}