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
            
            if (contextdata == null) return RedirectToPage("/DeckInput");
            
            var dataList = JsonConvert.DeserializeObject<List<string>>(contextdata);
            var DeckGenerated = HttpContext.Session.GetInt32("DeckGenerated");
            
            _deck = DeckGeneratorService.GenerateDeck(dataList);
            HttpContext.Session.SetString("Deck",JsonConvert.SerializeObject(_deck));
            HttpContext.Session.SetInt32("DeckGenerated", 1);

            foreach (var card in _deck._clubs) Console.WriteLine($"Club: {card}");
            foreach (var card in _deck._spades) Console.WriteLine($"Spade: {card}");
            foreach (var card in _deck._hearts) Console.WriteLine($"Heart: {card}");
            foreach (var card in _deck._diamonds) Console.WriteLine($"Diamond: {card}");

            return null;

    }
}