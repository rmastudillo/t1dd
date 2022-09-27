
namespace Players
{
  public class Player
  {
    public string[]? deck_list;
    public void Hola()
    {
      Console.WriteLine("Bienvenida");
    }
    public string read_cards()
    {
      return File.ReadAllText("Tarea1/Deck/cards.json");
    }
  }
}