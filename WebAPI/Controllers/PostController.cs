using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class PostController:ControllerBase
{
    private readonly IPostLogic PostLogic;

    public PostController(IPostLogic postLogic)
    {
        PostLogic = postLogic;
    }
    [HttpGet("authorized")]
    public ActionResult GetAsAuthorized()
    {
        return Ok("This was accepted as authorized");
    }
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync(NewPostDTO newPostDto)
    {
        try
        {
            Post post = await PostLogic.CreateAsync(newPostDto);
            return Created($"/Post/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] int? id)
    {
        try
        {
            Console.WriteLine("ttik");
            SearchPostDto parameters = new(id);
            IEnumerable<Post> posts = await PostLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            Console.WriteLine("jozo kubany");
            Post post = await PostLogic.GetByIdAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine("jozo kubany 2");
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}