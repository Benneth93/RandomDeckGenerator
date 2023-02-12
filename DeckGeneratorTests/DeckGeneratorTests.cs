using NUnit.Framework.Internal;
using RandomDeckGenerator.Models;
using RandomDeckGenerator.Services;

namespace DeckGeneratorTests;

public class DeckGeneratorTests
{
   
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GenerateDeckOfCards()
    {
        var list = new List<string>
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M",
            "A","B","C","D","E","F","G","H","I","J","K","L","M",
            "A","B","C","D","E","F","G","H","I","J","K","L","M",
            "A","B","C","D","E","F","G","H","I","J","K","L","M"
        };
       var deck =  DeckGeneratorService.GenerateDeck(list);
       Assert.Multiple(() =>
       {
           Assert.That(deck._clubs.Count, Is.EqualTo(13));
           Assert.That(deck._spades.Count, Is.EqualTo(13));
           Assert.That(deck._diamonds.Count, Is.EqualTo(13));
           Assert.That(deck._hearts.Count, Is.EqualTo(13));
       });
       
       
       TestContext.Progress.WriteLine("All Card counts are 13");
       
       TestContext.Progress.WriteLine($"Hearts: {deck._hearts.Count}");
       TestContext.Progress.WriteLine($"Spades: {deck._spades.Count}");
       TestContext.Progress.WriteLine($"Clubs: {deck._clubs.Count}");
       TestContext.Progress.WriteLine($"Diamonds: {deck._diamonds.Count}");
       
       foreach (var card in deck._hearts)TestContext.Progress.WriteLine(card);
       foreach (var card in deck._clubs)TestContext.Progress.WriteLine(card);
       foreach (var card in deck._spades)TestContext.Progress.WriteLine(card);
       foreach (var card in deck._diamonds)TestContext.Progress.WriteLine(card);

    }


}