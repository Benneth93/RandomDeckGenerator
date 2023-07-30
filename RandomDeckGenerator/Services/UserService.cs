using RandomDeckGenerator.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using RandomDeckGenerator.StubServices;

namespace RandomDeckGenerator.Services;

public static class UserService
{
    public static async Task<User> Login(string username, string password)
    {
        var hashedPassword = HashPassword(password);
        try
        {
            User? user = new();

            user = !AppSettingsService._stubs.AzureFileServiceStub
                ? await AzureFileShareService.GetSaveFileIfExists(username)
                : await AzureFileServiceStub.GetSaveFileIfExists(username);

            return (hashedPassword == user?.Password ? user : null) ?? throw new InvalidOperationException();
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
            User? userCheck = null;

            userCheck = !AppSettingsService._stubs.AzureFileServiceStub
                ? await AzureFileShareService.GetSaveFileIfExists(username)
                : await AzureFileServiceStub.GetSaveFileIfExists(username);

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
        
        
        
        for(var i = 0; i<52;i++)user.StoredList.Add("");


        if (!AppSettingsService._stubs.AzureFileServiceStub)
            await AzureFileShareService.UploadNewUser(user);
        else 
            await AzureFileServiceStub.UploadNewUser(user);
        
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