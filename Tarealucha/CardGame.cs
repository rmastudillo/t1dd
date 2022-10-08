using System.Globalization;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Luchalibre;

public partial class CardGame
{
  public Player PlayerOne { get; set;}
  public Player  PlayerTwo { get; set;} 
  public ConsolePrint ConsolePrint { get; set;}
  public Player CurrentPlayer { get; set;}
  public Player Opponent { get; set;}
  private bool CurrenlyPlaying { get; set; } = true;
  public Dictionary<string, Delegate> DictionaryOfCardEffects { get; set; } = new();

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
    LoadEffects();
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

  public void MenuLookCardsOptions()
  {
    var looking = true;
    while (looking)
    { 
      var playerOption = ConsolePrint.ShowCardsOptions(CurrentPlayer);
      switch (playerOption)
      {
        case 0:
          looking = false;
          break;
        case 1:
          ConsolePrint.ShowListOfCards(CurrentPlayer.Hand);
          if (CurrentPlayer.Hand.Count == 0)Console.WriteLine("No te quedan cartas en la mano!\n");
          break;
        case 2:
          ConsolePrint.ShowListOfCards(CurrentPlayer.RingSide);
          if (CurrentPlayer.RingSide.Count == 0)Console.WriteLine("No hay cartas en tu ringside!\n");
          break;
        case 3:
          ConsolePrint.ShowListOfCards(CurrentPlayer.RingArea);
          if (CurrentPlayer.RingArea.Count == 0)Console.WriteLine("No hay cartas en tu ring area!\n");
          break;
        case 4:
          ConsolePrint.ShowListOfCards(Opponent.RingSide);
          if (Opponent.RingSide.Count == 0)Console.WriteLine("No hay cartas en el ringside de tu oponente!\n");
          break;
        case 5:
          ConsolePrint.ShowListOfCards(Opponent.RingArea);
          if (Opponent.RingArea.Count == 0)Console.WriteLine("No hay cartas en el ring area de tu oponente!\n");
          break;
      }
    }
  }

  public bool FilterValidTypeToBePlayed(Card card, List<string> validTypes)
  {
    IEnumerable<string> res = validTypes.AsQueryable().Intersect(card.Types);
    return res.Any();
  }
  public bool FilterEnoughFortitudeToBePlayed(Card card, Player player)
  {
    /*Pendiente, modificadores de fortitude para jugar cartas*/
    return card.Fortitude <= player.CurrentFortitude;
  }
  public bool OtherRestrictions(Card card, Player player)
  {
    /*Pendiente, revisar alguna otra restriccion como por ejemplo que se tenga que jugar antes que otra cosa*/
    return true;
  }
  public List<Card> GetPlayableCardsFromHand()
  {
    var listOfPlayableCards = new List<Card>();
    var listOfTypesThatCanBePlayed= new List<string>{ "Maneuver","Action" } ;
    foreach (var card in CurrentPlayer.Hand)
    {
      var enoughFortitude = FilterEnoughFortitudeToBePlayed(card, CurrentPlayer);
      var validSubtype = FilterValidTypeToBePlayed(card, listOfTypesThatCanBePlayed);
      var otherRestrictions = OtherRestrictions(card, CurrentPlayer);
      if(enoughFortitude& validSubtype&otherRestrictions) listOfPlayableCards.Add(card);
    }
    return listOfPlayableCards;
  }
  public List<string> ReturnValidTypeToBePlayed(Player player, Card card, List<string> validTypes)
  {
    var returnValidTypes = new List<string>();
    foreach (var type in validTypes)
    {
      if (card.Types.Contains(type))
      {
        returnValidTypes.Add(type);
        /*Pendiente, tengo que elegir el tipo*/
      }
    }

    return returnValidTypes;
  }
  public void PlayingCardsFromHand()
  {
    var validCards = GetPlayableCardsFromHand();
    ConsolePrint.ShowListOfCards(validCards);
    var inputCardSelected = ConsolePrint.SelectCardToBePlayed(validCards);
    if (inputCardSelected != -1)
    {
      var cardSelected = validCards[inputCardSelected];
      Console.WriteLine($"El jugador quiere jugar la carta {cardSelected}");
      var types = new List<string>{"Maneuver","Action"} ;
      var validType = ReturnValidTypeToBePlayed(CurrentPlayer,cardSelected,types);
      var typeSelected = ConsolePrint.SelectType(cardSelected, validType);
      Console.WriteLine($"El jugador escogió el tipo {typeSelected}");


    }
  }
  public void MainTurn()
  {
    var playingOnMainTurn = true;
    while (playingOnMainTurn)
    {
      var currentPlayerOption = ConsolePrint.SelectMainPhaseOptions(CurrentPlayer);
      Console.WriteLine($"El jugador escogió la opcion{currentPlayerOption}");
      switch (currentPlayerOption)
      {
        case 0:
          break;
        case 1:
          MenuLookCardsOptions();
          break;
        case 2:
          PlayingCardsFromHand();
          break;
        case 3:
          playingOnMainTurn = false;
          break;
      }
    }
    ConsolePrint.NewTurnInfo(PlayerOne,PlayerTwo);
    
  }

  public void DamagePhase()
  {
    Console.WriteLine("DamagePhase");
  }
  public void EndTurnPhase()
  {
    Console.WriteLine("EndTurnPhase");
    ChangeCurrentPlayer();
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
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      DamagePhase();
      EndTurnPhase();
      CurrenlyPlaying = false;
    }
  }
}