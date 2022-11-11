﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using BlazorWASM.Services.Http;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;
    
    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<Post> Create(NewPostDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Post", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Post post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    public async Task<ICollection<Post>> GetAsync(string? title, int? userId, bool? body)
    {
        HttpResponseMessage response = await client.GetAsync("/Post");
        
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<IEnumerable<Post>> GetPosts(string? titleContains = null)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        string uri = "/Post";
        if (!string.IsNullOrEmpty(titleContains))
        {
            uri += $"?title={titleContains}";
        }

        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        IEnumerable<Post> posts= JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }
    
}