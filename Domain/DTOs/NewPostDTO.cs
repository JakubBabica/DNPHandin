namespace Domain.DTOs;

public class NewPostDTO
{
    public string title { get; set; }
    public string body { get; set; }

    public NewPostDTO(string title, string body)
    {
        this.title = title;
        this.body = body;
    }
}