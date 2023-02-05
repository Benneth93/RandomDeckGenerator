using RandomDeckGenerator.Models;
using RandomDeckGenerator.Services;

namespace DeckGeneratorTests;

public class DeckGeneratorTests
{
    private DeckModel _deck;
    private static List<string> _cardFaces;
    [SetUp]
    public void Setup()
    {
        _deck = DeckGeneratorService.GenerateDeck();
        _cardFaces = new ()
        {
            "A","2","3","4","5","6","7","8","9","10","J","Q","K"
        };
    }

    [Test]
    public void DeckGeneratorShouldReturn52Cards()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_deck.Clubs.Count, Is.EqualTo(13), $"Number of clubs in deck was not 13 but was: {_deck.Clubs.Count}");
            Assert.That(_deck.Spades.Count, Is.EqualTo(13), $"Number of spades in deck was not 13 but was: {_deck.Spades.Count}");
            Assert.That(_deck.Diamonds.Count, Is.EqualTo(13), $"Number of diamonds in deck was not 13 but was: {_deck.Diamonds}");
            Assert.That(_deck.Hearts.Count, Is.EqualTo(13), $"Number of hearts in deck was not 13 but was: {_deck.Hearts.Count}");
        });
        
        var cardCount = _deck.Clubs.Count + _deck.Spades.Count + _deck.Diamonds.Count + _deck.Hearts.Count;
        Assert.That(cardCount, Is.EqualTo(52), $"Total cards did not equal 52 card count was: {cardCount}");
    }

    [Test]
    public void SuitsShouldContainTheCorrectSymbols()
    {
        for (var i = 0; i < 13; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(_deck.Clubs[i].CardFace, Is.EqualTo(_cardFaces[i]));
                Assert.That(_deck.Diamonds[i].CardFace, Is.EqualTo(_cardFaces[i]));
                Assert.That(_deck.Spades[i].CardFace, Is.EqualTo(_cardFaces[i]));
                Assert.That(_deck.Hearts[i].CardFace, Is.EqualTo(_cardFaces[i]));
            });
        }
    }
}