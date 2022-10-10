using System.Globalization;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

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
  public Dictionary<string, Action> SuperStarActivation { get; set; } = new();

  public CardGame(ConsolePrint consolePrint, List<Deck> playersDecks)
  {
    ConsolePrint = consolePrint;
    PlayerOne = new Player(playersDecks[0],"PlayerOne");
    PlayerTwo = new Player(playersDecks[1],"PlayerTwo");
    CurrentPlayer = PlayerOne;
    Opponent = PlayerTwo;
  }

  public void StartGame()
  {
    LoadEffects();
    ConsolePrint.NewTurnInfo(PlayerOne,PlayerTwo);
    CalculateWhoPlaysFirst();
    DrawCards(CurrentPlayer, CurrentPlayer.Deck.Superstar.HandSize,"Deck","Hand");
    DrawCards(Opponent, Opponent.Deck.Superstar.HandSize,"Deck","Hand");
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
  void ChangeCurrentPlayer()
  {
    if (CurrentPlayer.Name == "PlayerOne")
    {
      CurrentPlayer.SuperStarHabilityAvailable = true;
      CurrentPlayer = PlayerTwo;
      Opponent = PlayerOne;
    }
    else
    {
      CurrentPlayer.SuperStarHabilityAvailable = true;
      CurrentPlayer = PlayerOne;
      Opponent = PlayerTwo;
    }
  }
  
  public void PreDrawPhase()
  {
    switch (CurrentPlayer.Deck.Superstar.Name)
    {
      case "KANE":
        KaneSuperStarHability();
        CurrentPlayer.SuperStarHabilityAvailable = false;
        break;
      case "HHH":
        CurrentPlayer.SuperStarHabilityAvailable = false;
        break;
      case "MANKIND":
        CurrentPlayer.SuperStarHabilityAvailable = false;
        break;
      case "THE ROCK":
      {
        if (CurrentPlayer.RingSide.Count > 0)
        {
          Console.WriteLine("Puedes activar tu Super Star Hability, ¿quieres hacerlo?");
          const string inputmessage = "Ingresa una opción:\n" +
                                      "[0] No usar mi habilidad\n" +
                                      "[1] Sí usar mi habilidad y recuperar una carta desde el RingSide";
          var playerInput = ConsolePrint.CheckInput(2, inputmessage);
          if (playerInput == 1)
          {
            SuperStarActivation[CurrentPlayer.Deck.Superstar.Name]();
          }
        }

        CurrentPlayer.SuperStarHabilityAvailable = false;
        break;
      }
    }

    Console.WriteLine("PredrawPhase");
  }
  public void DrawPhase()
  {
    Console.WriteLine("DrawPhase");
    DrawCards(CurrentPlayer,1,"Deck","Hand");
    if (CurrentPlayer.Deck.Superstar.Name == "MANKIND")
    {
      DrawCards(CurrentPlayer,1,"Deck","Hand");
      CurrentPlayer.SuperStarHabilityAvailable = false;
    }
   
  }

  public void MenuLookCardsOptions()
  {
    /*Ta bien*/
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
  public List<int> GetPlayableCardsFromHand(Player playerAptentingtoPlay,List<string> cardvalidType)
  {
    var listOfPlayableCards = new List<int>();
    for(var cardPosition = 0; cardPosition < playerAptentingtoPlay.Hand.Count;cardPosition++)
    {
      var card = playerAptentingtoPlay.Hand[cardPosition];
      var enoughFortitude = FilterEnoughFortitudeToBePlayed(card, playerAptentingtoPlay);
      var validSubtype = FilterValidTypeToBePlayed(card, cardvalidType);
      var otherRestrictions = OtherRestrictions(card, playerAptentingtoPlay);
      if(enoughFortitude& validSubtype&otherRestrictions) listOfPlayableCards.Add(cardPosition);
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

  public List<int> CheckOpponentReversalOptions()
  {
    var reversalType = new List<string> { "Reversal" };
    var playableCards = GetPlayableCardsFromHand(Opponent,reversalType);
    Console.WriteLine("Buscando en la mano del oponente para ver si puede revertir alguna carta");
    return playableCards;
  }

  public int GiveTheOpponentTheChanceToUseReversal(List<int> validCardsIndex)
  {
    var validCards = validCardsIndex.Select(validIndex => Opponent.Hand[validIndex]).ToList();
    ConsolePrint.ShowListOfCards(validCards);
    const string inputMessage = "Escoge la carta que quieres usar para revertir la carta, -1 para no usar ningun reversal";
    var oponentInput = ConsolePrint.CheckInput(validCards.Count,inputMessage, -1);
    return oponentInput;
  }
  public int AtemptToPlayCardWithType(Player playerPlayingTheCard,Card cardTobePlayed,string typeSelected)
  {
    if (!cardTobePlayed.CanBeReversed) return -1;
    var opponentHasCardsToRevertThis = CheckOpponentReversalOptions();
    if (opponentHasCardsToRevertThis.Count <= 0) return -1;
    var indexChosen = GiveTheOpponentTheChanceToUseReversal(opponentHasCardsToRevertThis);
    if (indexChosen == -1) return -1;
    var cardChosen = playerPlayingTheCard.Hand[indexChosen];
    Console.WriteLine($"EL JUGADOR QUIERE JUGAR EL REVERSAL {cardChosen} aaaaa");
    return -1;
  }
  
  public void CardsuccessfullyPlayed(Player player, int cardIndex)
  {
    var cardPlayed = player.Hand[cardIndex];
    MoveACardFromTo(player.Hand,cardIndex,player.RingArea);
    player.CurrentFortitude += (int) cardPlayed.Damage;
    DamagePhase(CurrentPlayer,Opponent,cardPlayed);
  }
  public void PlayingCardsFromHand()
  {
    
    var validCardsIndex = GetPlayableCardsFromHand(CurrentPlayer, new List<string>{ "Maneuver","Action" });
    var validCards = validCardsIndex.Select(validIndex => CurrentPlayer.Hand[validIndex]).ToList();
    ConsolePrint.ShowListOfCards(validCards);
    var inputCardSelected = ConsolePrint.SelectCardToBePlayed(validCards);
    if (inputCardSelected == -1) return;
    var cardSelectedIndex = validCardsIndex[inputCardSelected];
    var cardSelected = CurrentPlayer.Hand[cardSelectedIndex];
    Console.WriteLine($"El jugador quiere jugar la carta {cardSelected}");
    var types = new List<string>{"Maneuver","Action"} ;
    var validType = ReturnValidTypeToBePlayed(CurrentPlayer,cardSelected,types);
    var typeSelected = ConsolePrint.SelectType(cardSelected, validType);
    Console.WriteLine($"El jugador escogió el tipo {typeSelected}");
    var willBePlayed = AtemptToPlayCardWithType(CurrentPlayer, cardSelected, typeSelected);
    if (willBePlayed == -1)
    {
      CardsuccessfullyPlayed(CurrentPlayer,cardSelectedIndex);
    }

  }
  public void MainTurn()
  {
    var playingOnMainTurn = true;
    while (playingOnMainTurn)
    {
      ConsolePrint.NewTurnInfo(PlayerOne,PlayerTwo);
      var currentPlayerOption = ConsolePrint.SelectMainPhaseOptions(CurrentPlayer);
      Console.WriteLine($"El jugador escogió la opcion{currentPlayerOption}");
      switch (currentPlayerOption)
      {
        case 0:
          if (CurrentPlayer.SuperStarHabilityAvailable)
          {
            SuperStarActivation[CurrentPlayer.Deck.Superstar.Name]();
            CurrentPlayer.SuperStarHabilityAvailable = false;
          }
          else
          {
            Console.WriteLine("NO puedes usar tu superstarhability");
          }

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

  public void DamagePhase(Player damageFrom, Player damageTo, Card card)
  {
    ConsolePrint.ShowListOfCards(damageFrom.RingArea);
    Console.WriteLine("DamagePhase");
    var damage = card.Damage;
    if (Opponent.Deck.Superstar.Name == "MANKIND")
    {
      damage--;
    }
    for (var unitDamage = 0; unitDamage < damage; unitDamage++)
    {
      if (damageTo.Deck.Cards != null)
        MoveACardFromTo(damageTo.Deck.Cards, damageTo.Deck.Cards.Count - 1, damageTo.RingSide);
    }
  }
  public void EndTurnPhase()
  {
    ChangeCurrentPlayer();
    Console.WriteLine($"{CurrentPlayer.Name}{Opponent.Name}");
  }

  public void Playing()
  {
    StartGame();
    while (CurrenlyPlaying)
    {
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      PreDrawPhase();
      DrawPhase();
      MainTurn();
      EndTurnPhase();
      CurrenlyPlaying = false;
    }
  }
}