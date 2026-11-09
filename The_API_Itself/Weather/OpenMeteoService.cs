using System.Text.Json.Serialization;
using System.Net.Http.Json;
using The_API_Itself.Weather.Models;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc;

namespace The_API_Itself.Weather;

/// <summary>
/// An interface for a service to get weather data based on lat/long coordinates.
/// </summary>
public interface IOpenMeteoService

{
    HttpClient Client { get; set; }
    
    Task<OpenMeteoAPIResponse> GetWeatherForecast(
        double _lat, 
        double _long, 
        string timezone = "GMT-0800"
    );
}

public sealed class OpenMeteoService(IHttpClientFactory _factory) : IOpenMeteoService
{    
    public HttpClient Client { get; set; } 
        = _factory.CreateClient("weatherForecastService");

    public async Task<OpenMeteoAPIResponse> GetWeatherForecast(
        double latitude, 
        double longitude,
        string timezone = "UTC-0800"
    )
    
    {
        if (Client is null || Client.BaseAddress is null) 
            throw new Exception("HTTP Client not initialized.");

        // cancellation token 
        CancellationToken token = new();
        token.ThrowIfCancellationRequested();

        Dictionary<string, string> queryParams = new Dictionary<string, string>
        {
            ["latitude"] = $"{latitude}",
            ["longitude"] = $"{longitude}",
            ["timezone"] = "UTC-8",
            ["hourly"] = "temperature_2m,apparent_temperature,precipitation_probability,precipitation,rain,showers,snowfall"
        };

        var UriParams = queryParams
            // select each entry in the hash table and formatting it as a string of format 
                // [KeyName]=[Value] 
            .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value?.ToString() ?? "<null>")}");

        string paramsStringForUri = String.Join("&", UriParams);

        string APIURI = $"{Client.BaseAddress?.ToString() ?? "NOT FOUND"}" + 
            "?" + paramsStringForUri;
        
        try
        {
          var response = await Client
            .GetFromJsonAsync<OpenMeteoAPIResponse>($"{APIURI}", token);

            return response as OpenMeteoAPIResponse
                ?? throw new Exception("No data returned.");
        }
        // for debugging purposes only.
        catch (Exception e)
        {
            throw new (e.Message);
        }
    }

}