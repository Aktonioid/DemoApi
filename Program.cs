using DemoApi.Repositories;
using MongoDB.Driver;
using DemoApi.Settings;
using DemoApi.Repositories.Logi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using dotenv.net;
using static DemoApi.Extentions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DemoApi.Repositories.Refresh;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IMongoClient>(ServiceProvider => 
{
    MongoDBClient settings = new();
    return new MongoClient(settings.Connection_string);
});

 builder.WebHost.ConfigureKestrel(options => {
   options.ListenAnyIP(7213); 
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options => {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidIssuer = EnvDict()["Issuer"],
            ValidateAudience = true,
            ValidAudience = EnvDict()["Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvDict()["Key"])),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddAuthorization();

//builder.Services.AddSession();
builder.Services.AddSingleton<INewsRepo, NewsRepo>();
builder.Services.AddSingleton<IUserRepo, UserRepo>();
builder.Services.AddSingleton<IScheduleRepo, ScheduleRepo>();
builder.Services.AddSingleton<IInformationRepo, InformationRepo>();
builder.Services.AddSingleton<IMaterialsRepo, MaterialsRepo>();
builder.Services.AddSingleton<ITaskRepo, TaskRepo>();
builder.Services.AddSingleton<IAuthRepo, AuthRepo>();
builder.Services.AddSingleton<IRefresh, Refresh>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthentication();
app.UseRouting();   
app.UseAuthorization();
//app.UseSession();

app.MapControllers();

app.Run();
