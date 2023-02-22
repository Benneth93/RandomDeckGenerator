using Azure;
using Azure.Storage.Files.Shares;

namespace RandomDeckGenerator.Services;

public static class AzureFileShareService
{
    public static void UploadJsonFile(string json)
    {
        var shareName = "deckgeneratorfileshare";
        var folderName = "saves";
        var fileName = "save1.json";
        
        ShareClient share = new(AppSettingsService._connectionStrings.AzureFileServiceConnectionString, shareName);
        
        var directory = share.GetDirectoryClient(folderName);
        directory.CreateFileAsync(fileName, 24);
        
        var file = directory.GetFileClient(fileName);
        var jsonStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        
        file.CreateAsync(jsonStream.Length);
        file.UploadRangeAsync(new HttpRange(0, jsonStream.Length), jsonStream);

    }

    public static string GetSaveFileIfExists(string user)
    {
        
        return null;
    }
}