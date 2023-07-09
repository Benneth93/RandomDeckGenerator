using Azure;
using Azure.Storage.Files.Shares;
using Newtonsoft.Json;
using RandomDeckGenerator.Models;

namespace RandomDeckGenerator.Services;

public static class AzureFileShareService
{
    private static string shareName = "deckgeneratorfileshare";
    private static string folderName = "saves";

    public static void UploadJsonFile(string json, string userName)
    {
        var fileName = $"{userName}.json";
        
        ShareClient share = new(AppSettingsService._connectionStrings.AzureFileServiceConnectionString, shareName);
        
        var directory = share.GetDirectoryClient(folderName);
        directory.CreateFileAsync(fileName, 24);
        
        var file = directory.GetFileClient(fileName);
        var jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        
        file.CreateAsync(jsonStream.Length);
        file.UploadRangeAsync(new HttpRange(0, jsonStream.Length), jsonStream);

    }

    public static async Task<User> GetSaveFileIfExists(string userName)
    {
        var fileName = $"{userName}.json";
        ShareClient share = new(AppSettingsService._connectionStrings.AzureFileServiceConnectionString, shareName);

        var directory = share.GetDirectoryClient(folderName);
        var file = directory.GetFileClient(fileName);

        var data = "";
        
        await using (var stream = await file.OpenReadAsync().ConfigureAwait(false))
        using (var reader = new StreamReader(stream))
        {
            while (!reader.EndOfStream)data = await reader.ReadLineAsync().ConfigureAwait(false);
            
        }

        var userInfo = JsonConvert.DeserializeObject<User>(data);

        return userInfo;
    }

    public static async Task UploadNewUser(User userIn)
    {
        var fileName = $"{userIn.Username}.json";
        
        ShareClient share = new(AppSettingsService._connectionStrings.AzureFileServiceConnectionString, shareName);
        
        var directory = share.GetDirectoryClient(folderName);
        await directory.CreateFileAsync(fileName, 24);
        
        var file = directory.GetFileClient(fileName);
        var data = JsonConvert.SerializeObject(userIn);
        var jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data));
        
        await file.CreateAsync(jsonStream.Length);
        await file.UploadRangeAsync(new HttpRange(0, jsonStream.Length), jsonStream);
    }
}