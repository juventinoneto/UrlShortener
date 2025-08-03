var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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


app.MapGet("/healthcheck", () =>
{
    return "Healthy";
})
.WithTags("healthcheck");

app.MapGet("/url", () =>
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

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
