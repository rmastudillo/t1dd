namespace Luchalibre;
using static System.Text.Json.JsonSerializer;

public class Filemanager
{
    public static List<Card>LoadCards(string jsonString)
    {
        var cards = Deserialize<List<Card>>(jsonString);
        return cards ?? throw new ArgumentNullException(nameof(jsonString));
    }
}