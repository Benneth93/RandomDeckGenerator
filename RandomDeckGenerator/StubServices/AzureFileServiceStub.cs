using Newtonsoft.Json;
using RandomDeckGenerator.Models;
using RandomDeckGenerator.Services;

namespace RandomDeckGenerator.StubServices;

public static class AzureFileServiceStub
{
    static string _directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RandomDeckGeneratorUsers\\";

    static AzureFileServiceStub()
    {
        if (!Directory.Exists(_directory)) Directory.CreateDirectory(_directory);
    }

    public static async void UploadJsonFile(string json, string userName)
    {
        var filename = $"{userName}.json";
        await using StreamWriter outputFile = new StreamWriter(Path.Combine(_directory, filename));
        await outputFile.WriteAsync(json);
    }

    public static async Task<User?> GetSaveFileIfExists(string userName)
    {
        var fileName = $"{userName}.json";
        var data = "";
        
        using (var reader = new StreamReader(Path.Combine(_directory, fileName)))
        {
            while (!reader.EndOfStream) data = await reader.ReadLineAsync().ConfigureAwait(false);
        }

        var userInfo = JsonConvert.DeserializeObject<User>(data);
        return userInfo;
    }

    public static async Task UploadNewUser(User userIn)
    {
        var filename = $"{userIn.Username}.json";
        var json = JsonConvert.SerializeObject(userIn);
        
        await using StreamWriter outputFile = new StreamWriter(Path.Combine(_directory, filename));
        await outputFile.WriteAsync(json);
    }
}