using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Endpoints
{
    public static class AutenticacaoEndpoints
    {
        public static void MapAutenticacaoEndpoints(this WebApplication app)
        {
            app.MapPost("/register", async (User user, AppDbContext db) =>
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                db.Users.Add(user);
                await db.SaveChangesAsync();

                return Results.Created();
            })
            .Produces(StatusCodes.Status201Created)
            .WithName("Register")
            .WithTags("Autenticacao");

            app.MapPost("/login", [AllowAnonymous] async (User userModel, ITokenService tokenService, AppDbContext db) =>
            {
                if (userModel == null)
                {
                    return Results.BadRequest("Login Inválido");
                }

                var usuario = await db.Users.FirstOrDefaultAsync(u => u.UserName == userModel.UserName);

                if (usuario == null)
                {
                    return Results.BadRequest("Login Inválido");
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userModel.Password, usuario.Password);

                if (!isPasswordValid)
                {
                    return Results.BadRequest("Login Inválido");
                }

                var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
                    app.Configuration["Jwt:Issuer"],
                    app.Configuration["Jwt:Audience"],
                    userModel);

                return Results.Ok(new { token = tokenString });
            })
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            .WithName("Login")
            .WithTags("Autenticacao");
        }
    }
}
