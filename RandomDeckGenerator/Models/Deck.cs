namespace RandomDeckGenerator.Models;


public class Deck
{
    public List<string> _hearts { get; set; }
    public List<string> _spades { get; set; }
    public List<string> _diamonds { get; set; }
    public List<string> _clubs { get; set; }

    public Deck()
    {
        _hearts = new();
        _spades = new();
        _diamonds = new();
        _clubs = new();
    }
}