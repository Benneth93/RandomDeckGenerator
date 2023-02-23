using Newtonsoft.Json;

namespace RandomDeckGenerator.Services;

public class ConnectionStrings
{
    public string AzureFileServiceConnectionString { get; set; }
}

public static class AppSettingsService
{
    private static IConfiguration _configuration;
    private static string _connectionStringsFileLocation;

    public static ConnectionStrings _connectionStrings { get; private set; }

    public static void AppSettingsConfigure(IConfiguration config)
    {
        _configuration = config;
        _connectionStringsFileLocation = _configuration.GetSection("FileLocations")["ConnectionStringsFileLocation"] ?? string.Empty;
        MapConnectionStrings();
        Console.WriteLine("MappedConnectionStrings");
    }

    private static void MapConnectionStrings()
    {
        _connectionStrings = JsonConvert.DeserializeObject<ConnectionStrings>(File.ReadAllText(_connectionStringsFileLocation));
    }
}