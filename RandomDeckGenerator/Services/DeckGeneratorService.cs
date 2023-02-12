using Newtonsoft.Json;
using RandomDeckGenerator.Models;
using Microsoft.AspNetCore.Http;

namespace RandomDeckGenerator.Services;

public static class DeckGeneratorService
{
    public static Deck _deck = new();

    private static HttpContext _httpContext;
    static DeckGeneratorService()
    {
    }

    public static Deck GenerateDeck(List<string> inputs)
    {
        var valuesToUse = inputs;
        var rand = new Random();
        int selection = 0;
        
        do
        {
            selection = rand.Next(0, valuesToUse.Count - 1);
            
            _deck._clubs.Add(valuesToUse[selection]);
            valuesToUse.RemoveAt(selection);
            
            
        } while (_deck._clubs.Count != 13);
        
        do
        {
            selection = rand.Next(0, valuesToUse.Count - 1);
            
            _deck._spades.Add(valuesToUse[selection]);
            valuesToUse.RemoveAt(selection);
            
        } while (_deck._spades.Count != 13);
        
        do
        {
            selection = rand.Next(0, valuesToUse.Count - 1);
            
                _deck._hearts.Add(valuesToUse[selection]);
                valuesToUse.RemoveAt(selection);

        } while (_deck._hearts.Count != 13);
        
        do
        {
            selection = rand.Next(0, valuesToUse.Count - 1);
            
                _deck._diamonds.Add(valuesToUse[selection]);
                valuesToUse.RemoveAt(selection);

        } while (_deck._diamonds.Count != 13);
        
        return _deck;
    }
}