using RandomDeckGenerator.Models;

namespace RandomDeckGenerator.Services;

public static class UserService
{
    public static async Task<User> Login(string username, string password)
    {
        try
        {
            var user = await AzureFileShareService.GetSaveFileIfExists(username);
            return password == user.Password ? user : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }

    public static async Task<User> Register(string username, string password)
    {
        var user = new User
        {
            Username = username,
            Password = password,
            StoredList = new()
        };
        
        for(int i = 0; i<52;i++)user.StoredList.Add("");

        await AzureFileShareService.UploadNewUser(user);

        return user;
    }
}