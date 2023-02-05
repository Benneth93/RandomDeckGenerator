using RandomDeckGenerator.Models;

namespace RandomDeckGenerator.Services;

public static class DeckGeneratorService
{
    private static DeckModel _deck = new();
    public static DeckModel GenerateDeck()
    {
        return _deck;
    }
}