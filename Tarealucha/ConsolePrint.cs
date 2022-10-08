namespace Luchalibre;

public class ConsolePrint
{
    public void WelcommingMessage()
    {
        var welcome = new List<string>(new []
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
        var deckSelectionMessage = new List<string>(new []
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

    public void NewTurnInfo(Player playerOne, Player playerTwo)
    {
        var newGameMessage = new List<string>(new []
        {
            "----------------------------------------\n",
            "Se enfrentan:\n",
            $"(Player 1) {playerOne.Deck.Superstar.Name} vs (Player 2) {playerTwo.Deck.Superstar.Name}"});
        Console.WriteLine(string.Join("",newGameMessage));
        var players = new List<Player> { playerOne, playerTwo };
        foreach (var player in players)
        {
            var playerSuperStar = player.Deck.Superstar.Name;
            var playerCurrentFortitude = player.CurrentFortitude;
            var playerHandSize = player.Hand.Count;
            if (player.Deck.Cards == null) throw new InvalidOperationException(
                "Error NewTurnInfo, player.Deck.Cards is null");
            var playerDeckSize = player.Deck.Cards.Count;
            Console.WriteLine($"{player.Name}: {playerSuperStar} tiene {playerCurrentFortitude}F, " +
                              $"{playerHandSize} cartas en su mano y le quedan {playerDeckSize} cartas en su arsenal.");
        }
        Console.WriteLine("----------------------------------------");
    }
    public void MainTurn(Player playerOne, Player playerTwo)
    {
        NewTurnInfo(playerOne,playerTwo);
  
    }
    public void PredrawMsg(Player playerOne, Player  playerTwo)
    {
        Console.WriteLine("PreDraw;s");
    }

    public int MenuOptions(int numberOfOptions, IEnumerable<string> menuMessage, string inputMessage)
    {
        Console.WriteLine(string.Join("",menuMessage));
        return CheckInput(numberOfOptions, inputMessage);
    }
    public int SelectMainPhaseOptions(Player currentPlayer)
    {
        var mainPhaseOptions = new List<string>(new []
        {
            $"Es el turno de {currentPlayer.Name}: {currentPlayer.Deck.Superstar.Name}\n",
            "¿Qué quieres hacer?:\n",
            "        [0] Usar mi super habilidad \n",
            "        [1] Ver mis cartas o las de mi oponente\n",
            "        [2] Jugar una carta de mi mano\n",
            "        [3] Terminar mi turno"});
        const int mainPhaseNumberofOptions = 4;
        var mainPhaseInputMessage = $"\n (Ingresa un número entre 0 y {mainPhaseNumberofOptions - 1}: ";
        var playerOption = MenuOptions(
            mainPhaseNumberofOptions,
            mainPhaseOptions,
            mainPhaseInputMessage);
        return playerOption;
    }
    private int CheckInput(int numberOfOptions,string inputMessage)
    {
        int? output = null;
        while (output==null)
        {
            
            try
            {
                Console.Write(inputMessage);
                var userInputNumber = Convert.ToInt32(Console.ReadLine());
                if (Enumerable.Range(0, numberOfOptions).Contains(userInputNumber))
                {
                    output = userInputNumber;
                }
                else
                {
                    Console.WriteLine("La opción escrita no es válida!");
                }
            }
            catch
            {
                Console.WriteLine($"ERROR: Tiene que ser un número entre 0 y {numberOfOptions - 1}");
            }
        }
        return (int)output;
    }

    public int ShowCardsOptions(Player currentPlayer)
    {
        var mainPhaseOptions = new List<string>(new []
        {
            $"Es el turno de {currentPlayer.Name}: {currentPlayer.Deck.Superstar.Name}\n",
            "¿Qué cartas quieres ver?:\n",
            "        [0] Volver \n",
            "        [1] Mi mano\n",
            "        [2] Mi ringside\n",
            "        [3] El ringside de mi oponente\n",
            "        [4] El ring area de mi oponente" });
        const int numberofOptions = 5;
        var mainPhaseInputMessage = $"\n (Ingresa un número entre 0 y {numberofOptions - 1}: ";
        var playerOption = MenuOptions(
            numberofOptions,
            mainPhaseOptions,
            mainPhaseInputMessage);
        return playerOption;
    }
}