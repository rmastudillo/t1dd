using System.Globalization;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
namespace Luchalibre;

public partial class CardGame
{
  public Player PlayerOne { get; set;}
  public Player  PlayerTwo { get; set;} 
  public ConsolePrint ConsolePrint { get; set;}
  public Player CurrentPlayer { get; set;}
  public Player Opponent { get; set;}
  private bool CurrenlyPlaying { get; set; } = true;

  public CardGame(ConsolePrint consolePrint, List<Deck> playersDecks)
  {
    ConsolePrint = consolePrint;
    PlayerOne = new Player(playersDecks[0],"PlayerOne");
    PlayerTwo = new Player(playersDecks[1],"PlayerTwo");
    CurrentPlayer = PlayerOne;
    Opponent = PlayerTwo;
  }

  void ChangeCurrentPlayer()
  {
    if (CurrentPlayer.Name == "PlayerOne")
    {
      CurrentPlayer = PlayerTwo;
      Opponent = PlayerOne;
    }
    else
    {
      CurrentPlayer = PlayerOne;
      Opponent = PlayerOne;
    }
  }
  private void CalculateWhoPlaysFirst()
  {
    if (PlayerOne.Deck.Superstar.SuperstarValue < PlayerTwo.Deck.Superstar.SuperstarValue)
    {
      ChangeCurrentPlayer();
    }
    else if (PlayerOne.Deck.Superstar.SuperstarValue == PlayerTwo.Deck.Superstar.SuperstarValue)
    {
      var random = new Random();
      var listOfPlayers = new List<string>{"PlayerOne","PlayerTwo"};
      var index = random.Next(listOfPlayers.Count);
      if (listOfPlayers[index] != "PlayerTwo") return;
      ChangeCurrentPlayer();
    }
  }
  public void StartGame()
  {
    ConsolePrint.NewTurnInfo(PlayerOne,PlayerTwo);
    CalculateWhoPlaysFirst();
    DrawCards(CurrentPlayer, CurrentPlayer.Deck.Superstar.HandSize,"Deck","Hand");
    DrawCards(Opponent, Opponent.Deck.Superstar.HandSize,"Deck","Hand");
  }
  
  public void PreDrawPhase()
  {
    Console.WriteLine("PredrawPhase");
  }
  public void DrawPhase()
  {
    Console.WriteLine("DrawPhase");
    ConsolePrint.NewTurnInfo(PlayerOne,PlayerTwo);
    DrawCards(CurrentPlayer,1,"Deck","Hand");
    ConsolePrint.NewTurnInfo(PlayerOne,PlayerTwo);
  }
  public void MainTurn()
  {
    var currentPlayerOption = ConsolePrint.SelectMainPhaseOptions(CurrentPlayer);
    Console.WriteLine($"El jugador escogió la opcion{currentPlayerOption}");
    ConsolePrint.NewTurnInfo(PlayerOne,PlayerTwo);
  }

  public void DamagePhase()
  {
    Console.WriteLine("DamagePhase");
  }
  public void EndTurnPhase()
  {
    Console.WriteLine("EndTurnPhase");
  }

  public void Playing()
  {
    StartGame();
    while (CurrenlyPlaying)
    {
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      DamagePhase();
      EndTurnPhase();
      CurrenlyPlaying = false;
    }
  }
}