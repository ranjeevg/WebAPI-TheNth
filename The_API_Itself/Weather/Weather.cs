using System.Text.Json.Serialization;

namespace The_API_Itself.Weather;

/// <summary>
/// An interface for a service to get weather data based on lat/long coordinates.
/// </summary>
public interface IOpenWeatherClient
{
    Task<WeatherDatum> GetWeatherForecast(string _lat, string _long);
}



/// <summary>
/// The API returns an array of these.
/// </summary>
public sealed record WeatherDatum
    (   float latitude, 
        float longitude, 
        string start_hour, 
        string end_hour, 
        int ForecastDays = 7);
