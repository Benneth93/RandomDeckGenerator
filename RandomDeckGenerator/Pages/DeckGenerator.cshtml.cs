using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RandomDeckGenerator.Models;
using RandomDeckGenerator.Services;

namespace RandomDeckGenerator.Pages;

public class DeckGenerator : PageModel
{
    public Deck _deck { get; set; }

    public IActionResult OnGet()
    {
            var contextdata = HttpContext.Session.GetString("currentDataSet");

            if (HttpContext.Session.GetInt32("isLoggedIn") != 1) return RedirectToPage("/Login");
            if (contextdata == null) return RedirectToPage("/DeckInput");
            
            var dataList = JsonConvert.DeserializeObject<List<string>>(contextdata);
            
            _deck = DeckGeneratorService.GenerateDeck(dataList);
            HttpContext.Session.SetString("Deck",JsonConvert.SerializeObject(_deck));
            HttpContext.Session.SetInt32("DeckGenerated", 1);
            
            return null;

    }
}