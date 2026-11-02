using System.Text.Json.Serialization;
using System.Net.Http.Json;
using The_API_Itself.Weather.Models;
using System.Text.Json;

namespace The_API_Itself.Weather;

/// <summary>
/// An interface for a service to get weather data based on lat/long coordinates.
/// </summary>
public interface IOpenWeatherService

{
    Task<OpenWeatherAPIResponse> GetWeatherForecast(
        string _lat, 
        string _long, 
        string timezone = "GMT-0800"
    );
}

public sealed class OpenWeatherService(IHttpClientFactory _httpClientFactory) : IOpenWeatherService
{    
    
    public async Task<OpenWeatherAPIResponse> GetWeatherForecast(
        string latitude, 
        string longitude,
        string timezone = "GMT-0800"
    )
    
    {
        // because not done yet
        // throw new NotImplementedException("WIP, WIP");

        var client = _httpClientFactory.CreateClient("WeatherForecast");

        // add parameters as a Dictionary here

        // cancellation token 
        CancellationToken token = new();

        var response = await client.GetFromJsonAsync<OpenWeatherAPIResponse>
            (client.BaseAddress, token);

        return (OpenWeatherAPIResponse?)response 
            ?? throw new Exception("No data returned.");
    }

}