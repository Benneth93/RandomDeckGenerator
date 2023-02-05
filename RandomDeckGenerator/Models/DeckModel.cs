namespace RandomDeckGenerator.Models;

public class DeckModel
{
    public List<CardModel> Clubs { get; set; }
    public List<CardModel> Spades { get; set; }
    public List<CardModel> Diamonds { get; set; }
    public List<CardModel> Hearts { get; set; }

    private static List<string> _cardFaces = new ()
    {
        "A","2","3","4","5","6","7","8","9","10","J","Q","K"
    };
    public DeckModel()
    {
        Clubs = new();
        Spades = new();
        Diamonds = new();
        Hearts = new();
        
        for (var i = 0; i < 13; i++)
        {
            Clubs.Add(new(){CardFace = _cardFaces[i]});
            Spades.Add(new (){CardFace = _cardFaces[i]});
            Diamonds.Add(new (){CardFace = _cardFaces[i]});
            Hearts.Add(new(){CardFace = _cardFaces[i]});
        }

       
    }
}