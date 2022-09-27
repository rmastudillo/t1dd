using Deck;
using Players;
string decks_dir = "decks";
Player player1;
Player player2;
Console.WriteLine("Hello, World!");
Card hola = new Card();
player1 = new Player();
player2 = new Player();
Board board = new Board();
player1.Hola();
board.Hola();
void select_deck()
{
  bool choosing_deck = true;
  while (choosing_deck)
  {
    Console.WriteLine("Selecciona tu deck");
    var files = from file in Directory.EnumerateFiles(decks_dir) select file;
    int option_counter = files.Count<string>();
    foreach (var file in files)
    {
      option_counter--;
      string file_name = Path.GetFileNameWithoutExtension(file);
      Console.WriteLine("[{1}] {0}", file_name, option_counter.ToString());
    }
    option_counter = files.Count<string>();
    try
    {
      Console.Write("Escribe el número de tu Deck: ");
      int user_deck_number = Convert.ToInt32(Console.ReadLine());
      if (Enumerable.Range(0, option_counter).Contains(user_deck_number))
      {
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
select_deck();