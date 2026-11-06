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
    // the 'x-axis', as it were.
    public ICollection<DateTime> time { get; set; } = [];

    // each of these can be considered a 'y-axis' for the 'x-axis' above.
    // (each collection name corresponds to a param passed in the URI for the API call)
    public ICollection<float> temperature_2m { get; set; } = [];
    public ICollection<string> preciptiation_probability { get; set; } = [];
    public ICollection<float> precipitation { get; set; } = []; 
    public ICollection<float> rain { get; set; } = [];
    public ICollection<float> snowfall { get; set; } = [];



}

public sealed class UnitsUsed
{
    public string time { get; set; } 
        = DateTime.MinValue.ToString("yyyy-MM-ddThh:mm tt");
    
    public string temperature_2m { get; set; }
        = string.Empty;
    public string rainfall { get; set; }
        = string.Empty;
    public string apparent_temperature { get; set; }
        = string.Empty;
    public string precipitation_probability { get; set; }
        = string.Empty;
    public string precipitation { get; set; }
        = string.Empty;
    public string rain { get; set; }
        = string.Empty;
    public string showers { get; set; }
        = string.Empty;
    public string snowfall { get; set; }
        = string.Empty;
}

    
public sealed class OpenMeteoAPIResponse
{
    // basically 'header data' for coordinates and timezones and the like, 
    //  alongside another response model for the actual data.

    public double latitude { get; set; }
    public double longitude { get; set; }
    public string timezone { get; set; }
        = "UTC-8";
    public UnitsUsed hourly_units { get; set; }
        = new();
    public IndividualWeatherData hourly { get; set; } 
        = new();
    
}