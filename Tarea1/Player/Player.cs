
namespace Players
{
  public class Player
  {
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