using OpenApiUi;
using The_API_Itself.Weather;
using The_API_Itself.Weather.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IOpenMeteoService, OpenMeteoService>();
    
builder.Services
    .AddHttpClient<IOpenMeteoService, OpenMeteoService>("weatherForecastService", client => 
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

/// <summary>
/// The 'Weather' API - consuming the OpenMeteo version.
/// </summary>
/// <returns>
/// A <see cref="OpenMeteoAPIResponse"></see>, with each individual item requested being returned in individual arrays.
/// </returns>
app.MapGet("/Weather/GetWeatherForecast", async (IHttpClientFactory _factory) =>
{
    var weatherService = new OpenMeteoService(_factory);

    try
    {
        OpenMeteoAPIResponse forecast = await  weatherService
            .GetWeatherForecast(49.102, -123.1);

        return forecast;
    }
    catch (Exception e) {
        throw new(e.Message);
    }
})
.WithName("GetWeatherForecast");

app.MapGet("/Weather/GetNorthPolarForecast", async (IHttpClientFactory _factory) => 
{ 
    var weatherService = new OpenMeteoService(_factory);

    try
    {
        OpenMeteoAPIResponse polarForecast = await weatherService
            .GetWeatherForecast(90.0000, 0.0000);

        return polarForecast;
    }
    catch (Exception e)
    {
        throw new (e.Message);
    }
})
.WithName("GetPolarWeatherForecast");

app.MapGet("/Weather/GetSouthPoleForecast", async (IHttpClientFactory _factory) =>
{
    var weatherService = new OpenMeteoService(_factory);

    try
    {
        OpenMeteoAPIResponse polarForecast = await weatherService
            .GetWeatherForecast(-90.0000, 0.0000);
            
        return polarForecast;
    }
    catch (Exception e)
    {
        throw new (e.Message);
    }
})
.WithName("GetSouthPoleWeatherPredictions");

app.Run();