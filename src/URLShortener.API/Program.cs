using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using URLShortener.API.Authentication;
using URLShortener.API.Options;
using URLShortener.Data.Contexts;
using URLShortener.Domain.Entities;
using URLShortener.Services.Implementations;
using URLShortener.Shared.Data;
using URLShortener.Shared.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddSingleton<IAuthorizationHandler, UserTypeAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, UserTypeAuthorizationPolicyProvider>();

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    options.UseSqlite("Data source=../URLShortener.Data/local.db", b => b.MigrationsAssembly("URLShortener.Data"));
});

builder.Services.AddScoped<ILinkService, LinkService>();

builder.Services.AddSingleton<JwtProvider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();