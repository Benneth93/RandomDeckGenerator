using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using RandomDeckGenerator.Models;
using RandomDeckGenerator.Services;
using RandomDeckGenerator.StubServices;

namespace RandomDeckGenerator.Pages;

public class DeckInput : PageModel
{
    public List<string> inputs = new();

    public async Task<IActionResult> OnGet()
    {
        if(HttpContext.Session.GetInt32("isLoggedIn") != 1) return RedirectToPage("/Login"); 
        var currentData = HttpContext.Session.GetString("currentDataSet");

        if (currentData == null)
        {
           for(var i = 0;i < 52; i++) inputs.Add("");
        }
        else
        {
           var dataToList = JsonConvert.DeserializeObject<List<string>>(currentData);
           inputs = dataToList;
        }

        return null;
    }


    public async Task<IActionResult> OnPost()
    {
        var listOfInput = Request.Form["dataToInput"].ToList();

        listOfInput.RemoveAll(x => string.IsNullOrEmpty(x));
    
        var rand = new Random();

        while (listOfInput.Count < 52)
        {
            var current = rand.Next(0, listOfInput.Count);
            listOfInput.Add(listOfInput[current]);
        }

        User user;
        if (!AppSettingsService._stubs.AzureFileServiceStub)
        {
            user = await AzureFileShareService
                .GetSaveFileIfExists(HttpContext.Session.GetString("Username"));
        }
        else
        {
            user = await AzureFileServiceStub.GetSaveFileIfExists(HttpContext.Session.GetString("Username"));
        }

        user.StoredList = listOfInput;
        
        var json = JsonConvert.SerializeObject(user);

        if (!AppSettingsService._stubs.AzureFileServiceStub)
        {
            AzureFileShareService.UploadJsonFile(json, user.Username);
        }
        else
        {
            AzureFileServiceStub.UploadJsonFile(json, user.Username);
        }

        HttpContext.Session.SetString("currentDataSet", JsonConvert.SerializeObject(user.StoredList));
        
        return RedirectToPage("/DeckGenerator");
    }
}