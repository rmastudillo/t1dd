namespace Luchalibre;

public class ConsolePrint
{
    public void WelcommingMessage()
    {
        var welcome = new List<string>(new string[]
        {
            "########################################\n",
            "#   Bienvenido a Raw Deal Card Game    #\n",
            "########################################\n"
        });
        var welcomeString = string.Join("",welcome) ;
        Console.WriteLine(welcomeString);
    }

    private string CheckDeckInput(int optionCounter,IReadOnlyList<string> files)
    {
        string? deck = null;
        while (deck==null)
        {
            
            try
            {
                Console.Write("Escribe el número de tu Deck: ");
                var userDeckNumber = Convert.ToInt32(Console.ReadLine());
                if (Enumerable.Range(0, optionCounter).Contains(userDeckNumber))
                {
                    Console.WriteLine("Escogiste el Mazo {0} !",
                        Path.GetFileNameWithoutExtension(files[userDeckNumber]));
                    deck = files[userDeckNumber];
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
        return deck;
    }
    public string ChooseDeck(string deckPath)
    {
        var files = Directory.GetFiles(deckPath);
        var optionCounter = 0;
        var deckSelectionMessage = new List<string>(new string[]
        {
            "# Selecciona tu deck\n"
        });
        var welcomeString = string.Join("",deckSelectionMessage) ;
        Console.WriteLine(welcomeString);
        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            Console.WriteLine("[{1}] {0}", fileName, optionCounter.ToString());
            optionCounter++;
        }
        return CheckDeckInput(optionCounter, files);
    }
}