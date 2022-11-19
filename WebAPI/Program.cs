using System.Text;
using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.Auth;
using EfcDataAccess.DAOs;
using FileData;
using FileData.DAOs;
using FileData.FileDaoImpl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDao, CreateUserDao>();
//builder.Services.AddScoped<IUserDao, UserEfcDao>(); -use later
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IPostDAO, PostFileDTO>();
//builder.Services.AddScoped<IPostDAO, PostEfcDao>(); -use later
builder.Services.AddScoped<IPostLogic, PostLogic>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(sp=>new HttpClient
{
    BaseAddress = new Uri("http://localhost:7145")
    
}
    );
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
AuthorizationPolicies.AddPolicies(builder.Services);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();