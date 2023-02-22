using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IO;
using RandomDeckGenerator.Services;

namespace RandomDeckGenerator.Pages;

public class DeckInput : PageModel
{
    public List<string> inputs = new();

    public void OnGet()
    {
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
    }


    public IActionResult OnPost()
    {
        var listOfInput = Request.Form["dataToInput"].ToList();

        listOfInput.RemoveAll(x => string.IsNullOrEmpty(x));
    
        var rand = new Random();

        while (listOfInput.Count < 52)
        {
            var current = rand.Next(0, listOfInput.Count);
            listOfInput.Add(listOfInput[current]);
        }
        
        var json = JsonConvert.SerializeObject(listOfInput);
        AzureFileShareService.UploadJsonFile(json);
        HttpContext.Session.SetString("currentDataSet", json);
        
        return RedirectToPage("/DeckGenerator");
    }
}