using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RandomDeckGenerator.Models;
using RandomDeckGenerator.Services;

namespace RandomDeckGenerator.Pages;

public class DeckGenerator : PageModel
{
    public Deck _deck { get; set; }

    public void OnGet()
    {
            var listOfData = new List<string>()
            {
                "a", "b", "will", "bill", "phillis", "dandy", "randy", "mandy", "willbur", "holly", "molly", "Jon",
                "Michael",
                "Michele", "Phillis", "Christina", "Ben", "wallace", "Simon", "Simone", "Billy", "Millie", "Miles",
                "Giles", "Rosie", "Lotty",
                "Scotty", "Scott", "Richard", "Sophie", "Helen", "Karen", "Morfydd", "Spencer", "Harry", "Hermione",
                "Verstappen", "Norris", "Hamilton",
                "Kriss", "Sally", "Verona", "Hill", "Bryn", "Antonio", "Bendaras", "Alanah", "Pedro", "Bella", "Gordon",
                "Ramsey", "Pascal"
            };

            HttpContext.Session.SetString("currentUser", "Ben");
            var json = JsonConvert.SerializeObject(listOfData);
            HttpContext.Session.SetString("currentDataSet", json);
            var contextdata = HttpContext.Session.GetString("currentDataSet");
            
            var dataList = JsonConvert.DeserializeObject<List<string>>(contextdata);
            var DeckGenerated = HttpContext.Session.GetInt32("DeckGenerated");
            if ( DeckGenerated == 0 || DeckGenerated == null )
            {
                _deck = DeckGeneratorService.GenerateDeck(dataList);
                HttpContext.Session.SetString("Deck",JsonConvert.SerializeObject(_deck));
                HttpContext.Session.SetInt32("DeckGenerated", 1);
            }
            else
                _deck = JsonConvert.DeserializeObject<Deck>(HttpContext.Session.GetString("Deck"));


            foreach (var card in _deck._clubs) Console.WriteLine($"Club: {card}");
            foreach (var card in _deck._spades) Console.WriteLine($"Spade: {card}");
            foreach (var card in _deck._hearts) Console.WriteLine($"Heart: {card}");
            foreach (var card in _deck._diamonds) Console.WriteLine($"Diamond: {card}");
            
    }
}