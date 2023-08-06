using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RandomDeckGenerator.Models;

namespace RandomDeckGenerator.Pages;

public class CustomizeLists : PageModel
{
    public List<UserStoredList> userLists = new();
    public async Task<IActionResult> OnGet()
    {
        if(HttpContext.Session.GetInt32("isLoggedIn") != 1) return RedirectToPage("/Login");
        var userListJson = "";

        var json = await tryGetLists();
        userLists = JsonConvert.DeserializeObject<List<UserStoredList>>(json);
        
        return null;
    }

    private async Task<string> tryGetLists()
    {
        var maxRetries = 5;
        var retryCount = 0;
        string listJson = null;

        while (string.IsNullOrEmpty(listJson) && retryCount < maxRetries)
        {
            listJson = HttpContext.Session.GetString("userLists");
            retryCount++;

            if (string.IsNullOrEmpty(listJson))
            {
                await Task.Delay(500);
            }
        }

        if (string.IsNullOrEmpty(listJson))
        {
            throw new InvalidDataException("Unable to get lists");
        }

        return listJson;
    }
}