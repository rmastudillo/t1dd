
namespace Deck
{
  public class Card
  {
    public string? Title;
    public string[]? Types;
    public string[]? Subtypes;
    public int? Fortitude;
    public int? Damage;
    public int? StunValue;
    public string? CardEffect;
    public string[]? Restrictions;
    public void SetClass()
    {
      Console.WriteLine("GOLA");
    }
    public string read_cards()
    {
      return File.ReadAllText("Tarea1/Deck/cards.json");
    }
  }
  public class Superstar
  {
  }
}