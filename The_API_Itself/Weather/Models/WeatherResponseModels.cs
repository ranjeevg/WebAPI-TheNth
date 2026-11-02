namespace The_API_Itself.Weather.Models;

public sealed record WeatherAPIQueryParams
{

}

/// <summary>
/// A wrapper for individual method data. 
/// <br /><br />
/// Arrays of other things (moisture percentages, etc.) to get added here in a similar manner.
/// </summary>
public sealed class IndividualWeatherData
{
    public ICollection<String> time { get; set; } = [];
    public ICollection<float> temperature_2m { get; set; } = [];
}

public sealed class UnitsUsed
{
    public string time { get; set; } 
        = DateTime.MinValue.ToString("yyyy-MM-ddThh:mm tt");
    
    public string temperature_2m { get; set; }
        = string.Empty;
}

    
public sealed class OpenWeatherAPIResponse
{
    // basically 'header data' for coordinates and timezones and the like, 
    //  alongside another response model for the actual data.

    public double latitude { get; set; }
    public double longitude { get; set; }
    public double timezone { get; set; }
    public UnitsUsed hourly_units { get; set; }
        = new();
    public IndividualWeatherData hourly { get; set; } 
        = new();
    
}