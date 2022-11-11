namespace Domain.DTOs;

public class SearchPostDto
{
    public int? IdContains { get; }

    public SearchPostDto(int? idContains)
    {
        IdContains = idContains;
    }
}