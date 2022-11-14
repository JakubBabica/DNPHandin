﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Services;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController:ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;
    private readonly IUserLogic _userLogic;

    public AuthController(IConfiguration config, IAuthService authService, IUserLogic userLogic)
    {
        this.config = config;
        this.authService = authService;
        this._userLogic = userLogic;
    }
    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Email", user.Email),
            new Claim("Age", user.Age.ToString()),

        };
        return claims.ToList();
    }
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
    
        try
        {
            User user = await authService.GetUser(userLoginDto);
            string token = GenerateJwt(user);
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine("controller is not working");
            return BadRequest(e.Message);
        }
    }
    [HttpPost, Route("register")]
    public async Task<ActionResult> Register([FromBody] UserCreationDto userRegisterDto)
    {
        try
        {
            Console.WriteLine("register is working");
            User user = await authService.RegisterUser(userRegisterDto);

             // _userLogic.CreateAsync(userRegisterDto);
            // string token = GenerateJwt(user);
    
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}