using System.Globalization;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
namespace Luchalibre;

public class CardGame
{
  public Player PlayerOne { get; set;}
  public Player  PlayerTwo { get; set;} 
  public ConsolePrint ConsolePrint { get; set;}
  public Player CurrentPlayer { get; set;}
  public Player Opponent { get; set;}

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
    ConsolePrint.NewGame(PlayerOne.Name,PlayerTwo.Name);
    CalculateWhoPlaysFirst();
    DrawCards(CurrentPlayer, CurrentPlayer.Deck.Superstar.HandSize,"Deck","Hand");
    DrawCards(Opponent, Opponent.Deck.Superstar.HandSize,"Deck","Hand");
  }

  private void DrawCards(Player player, int numberOfCards, string from, string to)
  {
     List<Card>? fromList = null;
     List<Card>? toList = null; 
     if (from == "Deck") fromList = player.Deck.Cards;
     if (to == "Hand") toList = player.Hand;
     if (fromList == null | toList == null) return;
     for (var i = 0; i <= numberOfCards; i++)
     {
       toList?.Add(fromList?.Last() ?? throw new InvalidOperationException(
         "No se ha seteado correctamente Draw card"));
       fromList?.RemoveAt(fromList.Count-1);
     }
  }
}