
namespace Deck
{
  public class Board
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