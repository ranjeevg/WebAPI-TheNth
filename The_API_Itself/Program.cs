using OpenApiUi;
using The_API_Itself.Weather;
using The_API_Itself.Weather.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IOpenWeatherService, OpenWeatherService>();
    
builder.Services
    .AddHttpClient<IOpenWeatherService, OpenWeatherService>("weather", client => 
    {
        client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast");
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
        client.DefaultRequestHeaders.Add("User-Agent", "DotNet10App");
        client.Timeout = TimeSpan.FromSeconds(30);
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApiUi(config => config.OpenApiSpecPath = "/openapi/v1.json");
}

app.UseHttpsRedirection();

app.MapGet("/Weather/GetWeatherForecast", async (IHttpClientFactory _factory) =>
{
    var weatherService = new OpenWeatherService(_factory);

    try
    {
        OpenWeatherAPIResponse forecast = await  weatherService
            .GetWeatherForecast(49.102, -123.1);

        return forecast;
    }
    catch (Exception e) {
        throw new(e.Message);
    }
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 9 * 5);
}
