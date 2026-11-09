namespace The_API_Itself.Weather.Data;

/// <summary>
/// Weather codes returned by the OpenMeteo API.
/// </summary>
public enum WeatherCodes
{
    ClearSky = 0,
    MainlyClear = 1,
    PartlyCloudy = 2,
    Fog = 45,
    DepositingRimeFog = 48,
    LightDrizzle = 51,
    ModerateDrizzle = 53,
    DenseDrizzle = 55,
    LightSleet = 56,
    HeavySleet = 57,
    SlightRain = 61,
    ModerateRain = 63,
    HeavyRain = 65,
    LightFreezingRain = 66,
    HeavyFreezingRain = 67,
    LightSnow = 71,
    ModerateSnow = 73,
    HeavySnowfall = 75, 
    Flurries = 77,
    SlightRainShowers = 80,
    ModerateRainShowers = 81,
    ViolentRainShowers = 82,
    SlightSnowShowers = 85,
    HeavySnowShowers = 86, 
    Thunderstorm = 95,
    ThunderstormWithSlightHail = 96,
    HeavyThunderstorm = 97,
    ThunderstormWithHeavyHail = 99
}
