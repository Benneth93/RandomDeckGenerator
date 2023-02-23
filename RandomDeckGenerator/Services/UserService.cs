using RandomDeckGenerator.Models;
using System.Security.Cryptography;
using System.Text;

namespace RandomDeckGenerator.Services;

public static class UserService
{
    public static async Task<User> Login(string username, string password)
    {
        var hashedPassword = HashPassword(password);
        try
        {
            var user = await AzureFileShareService.GetSaveFileIfExists(username);
            return hashedPassword == user.Password ? user : null;
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
            Password = HashPassword(password),
            StoredList = new()
        };
        
        for(int i = 0; i<52;i++)user.StoredList.Add("");

        await AzureFileShareService.UploadNewUser(user);

        return user;
    }

    private static string HashPassword(string password)
    {
        var input = Encoding.UTF8.GetBytes(password);
        var hashBytes = MD5.Create().ComputeHash(input);

        return Convert.ToHexString(hashBytes);
    }
}