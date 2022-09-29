using Deck;
using Players;
using System.Text.Json;
/* string decks_dir = "decks"; */

Player player1;
Player player2;
Console.WriteLine("Hello, World!");
player1 = new Player();
player2 = new Player();
Board board = new Board();

/* void select_deck()
{
  bool choosing_deck = true;
  string[] files = Directory.GetFiles(decks_dir);
  while (choosing_deck)
  {
    Console.WriteLine("Selecciona tu deck");
    int option_counter = 0;
    foreach (var file in files)
    {
      string file_name = Path.GetFileNameWithoutExtension(file);
      Console.WriteLine("[{1}] {0}", file_name, option_counter.ToString());
      option_counter++;
    }
    try
    {
      Console.Write("Escribe el número de tu Deck: ");
      int user_deck_number = Convert.ToInt32(Console.ReadLine());
      if (Enumerable.Range(0, option_counter).Contains(user_deck_number))
      {
        Console.WriteLine("Escogiste el Mazo de {0} !",
          Path.GetFileNameWithoutExtension(files[user_deck_number]));
        choosing_deck = false;

      }
      else
      {
        Console.WriteLine("No existe el deck con el número escogido  D: !!!");
      }
    }
    catch
    {
      Console.WriteLine("ERROR: Tiene que ser el número que corresponde al deck");
    }

  }
}
 select_deck();  */
 
List<Card>Load(){
  string fileName = "Tarea1/Deck/cards.json";
  string jsonString = File.ReadAllText(fileName);
  List<Card> cardList= JsonSerializer.Deserialize<List<Card>>(jsonString);
 return cardList; 
};
List<Card> cartas = Load();
string hola = "0";
int h = Convert.ToInt32(hola);
Console.WriteLine(h);
foreach(var car in cartas)
{
  Console.WriteLine(car.ToString());
} 

