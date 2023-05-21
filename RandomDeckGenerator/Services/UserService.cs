using RandomDeckGenerator.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;

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

    public static async Task<UserRegistrationResponse> Register(string username, string password)
    {
        try
        {
            var userCheck = await AzureFileShareService.GetSaveFileIfExists(username);
            if (userCheck != null) return new UserRegistrationResponse()
            {
                User = null,
                Message = "User already Exists",
                isSuccess = false
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        var user = new User
        {
            Username = username,
            Password = HashPassword(password),
            StoredList = new()
        };
        
        
        
        for(int i = 0; i<52;i++)user.StoredList.Add("");

        await AzureFileShareService.UploadNewUser(user);
        
        var userRegistrationResponse = new UserRegistrationResponse()
        {
            User = user,
            Message = "Registration Successful",
            isSuccess = true
        };
        
        return userRegistrationResponse;
    }

    private static string HashPassword(string password)
    {
        var input = Encoding.UTF8.GetBytes(password);
        var hashBytes = MD5.Create().ComputeHash(input);

        return Convert.ToHexString(hashBytes);
    }
}