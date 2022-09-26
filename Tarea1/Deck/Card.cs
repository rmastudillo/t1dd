
namespace Deck
{
  public class Card
  {
    public void Hola()
    {
      Console.WriteLine("GOLA");
    }
    public string read_cards()
    {
      return File.ReadAllText("Tarea1/Deck/cards.json");
    }
  }
}