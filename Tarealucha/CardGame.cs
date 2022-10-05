namespace Luchalibre;

public class CardGame
{
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
}