namespace Luchalibre;
using static System.Text.Json.JsonSerializer;

public class Filemanager
{
    public static Dictionary<string,Card>LoadCards(string path)
    {
        var jsonString = File.ReadAllText(path);
        var cardsDict = new Dictionary<string, Card>();
        var cards = Deserialize<List<Card>>(jsonString);
        if (cards == null) return cardsDict ?? throw new ArgumentNullException(nameof(jsonString));
        cards.ForEach(card => cardsDict[card.Title] = card);
        return cardsDict ?? throw new ArgumentNullException(nameof(jsonString));
    }
}