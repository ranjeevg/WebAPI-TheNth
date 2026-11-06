using OpenApiUi;
using The_API_Itself.Weather;
using The_API_Itself.Weather.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<IOpenWeatherService>
    (client => client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast"));
    
builder.Services.AddTransient<OpenWeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApiUi(config => config.OpenApiSpecPath = "/openapi/v1.json");
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

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
