using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Endpoints
{
    public static class CatalogoEndpoints
    {
        public static void MapCatalogoEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => "Catálogo de Produtos - 2024").ExcludeFromDescription();
        }
    }
}
