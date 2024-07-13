using ApiCatalogo.AppServicesExtensions;
using ApiCatalogo.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAuthenticationJwt();

var app = builder.Build();

app.MapCatalogoEndpoints();
app.MapAutenticacaoEndpoints();
app.MapCategoriaEndpoints();
app.MapProdutoEndpoints();


var environment = app.Environment;

app.UserExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();