using Microsoft.AspNetCore.Mvc;
using UrlShortener_AppServices.Interfaces;
using UrlShortenerDI;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ResolveServices();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Open API");
    });
}

app.UseHttpsRedirection();

// healthcheck
app.MapGet("/healthcheck", () =>
{
    return "Healthy";
})
.WithTags("healthcheck");

// url
app.MapGet("/", (
    [FromServices] IUrlAppService service) =>
{
    return "Hold on tight... You will be redirected soon.";
})
.WithTags("redirect");

// url
app.MapGet("/url", (
    [FromServices] IUrlAppService service) =>
{
    string[] urls = ["urlShorted1", "urlShorted2"];
    return urls;
})
.WithTags("url");

app.MapGet("/url/raw/{key}", (
    [FromServices] IUrlAppService service,
    [FromRoute] string key) =>
{
    string[] urls = ["urlShorted1", "urlShorted2"];
    return urls;
})
.WithTags("url");

app.MapGet("/url/shortened", (
    [FromServices] IUrlAppService service,
    [FromQuery] string originalUrl) =>
{
    string[] urls = ["urlShorted1", "urlShorted2"];
    return urls;
})
.WithTags("url");

app.MapPost("/url", () =>
{
    return Results.Accepted();
})
.WithTags("url");

app.Run();
