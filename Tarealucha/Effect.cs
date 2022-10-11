namespace Luchalibre;

public partial class CardGame
{
    private void DrawCards(Player player, int numberOfCards, string from, string to)
  {
    
     List<Card>? fromList = null;
     List<Card>? toList = null; 
     if (from == "Deck") fromList = player.Deck.Cards;
     if (to == "Hand") toList = player.Hand;
     if (fromList == null | toList == null) return;
     for (var i = 0; i < numberOfCards; i++)
     {
       toList?.Add(fromList?.Last() ?? throw new InvalidOperationException(
         "No se ha seteado correctamente Draw card"));
       fromList?.RemoveAt(fromList.Count-1);
     }
  }
    private void CardEffectDrawCards(Card card, string cardType,int effectPos)
    {
      Console.WriteLine("Robar cartas desde una carta efecto");
    }
    private void CardEffectLookHand(Card card, string cardType,int effectPos)
    {
      if (cardType == "Maneuver")
      { 
        var effect = card.EffectAsManeuver[effectPos];
        if (effect.Target == "opponent")
        {
          Console.WriteLine("# Puedes ver la mano del oponente");
          ConsolePrint.ShowListOfCards(Opponent.Hand);
        }

      }

      if (cardType == "Action")
      {
        
        var effect = card.EffectAsAction[effectPos];
        if (effect.Target == "opponent")
        {
          Console.WriteLine("# Puedes ver la mano del oponente");
          ConsolePrint.ShowListOfCards(Opponent.Hand);
        }
      }
    }

    public void CardEffectDiscardCards(Card card, string cardType, int effectPos)
    {
      if (cardType == "Maneuver")
      { 
        var effect = card.EffectAsManeuver[effectPos];
        if (effect.Target == "opponent")
        {
          Console.WriteLine($"Se activa el efecto de {card.Title}  y descartas {effect.Value} cartas");
          DiscardCard(Opponent,Opponent.Deck.Cards.Count-1,"Deck");
        }
        if (effect.Target == "player")
        {
          Console.WriteLine($"Se activa el efecto de {card.Title}  y descartas {effect.Value} cartas");
          DiscardCard(CurrentPlayer,CurrentPlayer.Deck.Cards.Count-1,"Deck");
        }

      }

      if (cardType == "Action")
      {
        var effect = card.EffectAsAction[effectPos];
        if (effect.Target == "opponent")
        {
          Console.WriteLine($"Se activa el efecto de {card.Title}  y descartas {effect.Value} cartas");
          DiscardCard(Opponent,Opponent.Deck.Cards.Count-1,"Deck");
        }
        if (effect.Target == "player")
        {
          Console.WriteLine($"Se activa el efecto de {card.Title}  y descartas {effect.Value} cartas");
          DiscardCard(CurrentPlayer,CurrentPlayer.Deck.Cards.Count-1,"Deck");
        }
      }
    }
    private void LoadEffects()
    {
      DictionaryOfCardEffects["draw"] = CardEffectDrawCards;
      DictionaryOfCardEffects["lookhand"] = CardEffectLookHand;
      DictionaryOfCardEffects["discard"] = CardEffectDiscardCards;
      SuperStarActivation.Add("STONE COLD",StoneColdSuperStarHability);
      SuperStarActivation.Add("JERICHO",JerichoSuperStarHability);
      SuperStarActivation.Add("THE ROCK",TheRockSuperStarHability);
      SuperStarActivation.Add("THE UNDERTAKER", UndertakerSuperStarHability);
    }

    public void DiscardCard(Player player, int cardIndex, string from)
    {
      List<Card>? fromList = null;
      var toList = player.RingSide;
      if (from == "Hand") fromList = player.Hand;
      if (from == "Deck") fromList = player.Deck.Cards;
      if (fromList == null) throw new InvalidOperationException("Error DiscardCard, fromList is null");
      MoveACardFromTo(fromList, cardIndex, toList);
    }
    public void MoveACardFromTo(List<Card> from, int cardIndex, List<Card> to)
    {
      to.Add(from[cardIndex]);
      from.RemoveAt(cardIndex);
    }

    public void KaneSuperStarHability()
    {
      Console.WriteLine("\n# Se activa la habilidad de KANE y el oponente descarta 1 carta desde el arsenal\n");
      if (Opponent.Deck.Cards != null)
        MoveACardFromTo(Opponent.Deck.Cards, Opponent.Deck.Cards.Count - 1, Opponent.RingSide);
    }

    public void JerichoDiscard(Player player)
    {
      ConsolePrint.ShowListOfCards(player.Hand);
      const string discardMessage = "Se activó la hablidad de Jericho, " +
                                    "debes escoger una carta de tu mano para que sea descartada";
      var playerInputMessage =  $"\n (Ingresa un número entre 0 y {player.Hand.Count - 1}): ";
      var playerInput = ConsolePrint.SelectACard(
        player.Hand, discardMessage, playerInputMessage, true);
      DiscardCard(player,playerInput,"Hand");
    }
    public void JerichoSuperStarHability()
    {
      JerichoDiscard(CurrentPlayer);
      JerichoDiscard(Opponent);
      
    }
    public void StoneColdSuperStarHability()
    {
      DrawCards(CurrentPlayer,1,"Deck","Hand");
      ConsolePrint.ShowListOfCards(CurrentPlayer.Hand);
      const string discardMessage = "Activaste la habilidad de Stone Cold, " +
                                    "debes escoger una carta de tu mano para que sea descartada";
      var playerInputMessage =  $"\n (Ingresa un número entre 0 y {CurrentPlayer.Hand.Count - 1}): ";
      var playerInput = ConsolePrint.SelectACard(
        CurrentPlayer.Hand, discardMessage, playerInputMessage, true);
      CurrentPlayer.Deck.Cards.Insert(0, CurrentPlayer.Hand[playerInput]);
      CurrentPlayer.Hand.RemoveAt(playerInput);
    }
    public void TheRockSuperStarHability()
    { 
      ConsolePrint.ShowListOfCards(CurrentPlayer.RingSide);
      const string discardMessage = "Activaste la habilidad de The Rock, " +
                                    "debes escoger una carta de tu Ring side para volver al fondo de tu Arsenal";
      var playerInputMessage =  $"\n (Ingresa un número entre 0 y {CurrentPlayer.RingSide.Count - 1}): ";
      var playerInput = ConsolePrint.SelectACard(
        CurrentPlayer.RingSide, discardMessage, playerInputMessage, true);
      CurrentPlayer.Deck.Cards.Insert(0, CurrentPlayer.RingSide[playerInput]);
      CurrentPlayer.RingSide.RemoveAt(playerInput);
    }
    public void UndertakerSuperStarHability()
    {
      if (CurrentPlayer.Hand.Count < 2) return;
      ConsolePrint.ShowListOfCards(CurrentPlayer.Hand);
      const string discardMessage = "Activaste la habilidad de The Undertaker, " +
                                    "debes escoger dos cartas de tu mano para descartar";
      var playerInputMessage =  $"\n (Ingresa un número entre 0 y {CurrentPlayer.Hand.Count - 1}): ";
      var playerInput = ConsolePrint.SelectACard(
        CurrentPlayer.Hand, discardMessage, playerInputMessage, true);
      DiscardCard(CurrentPlayer,playerInput,"Hand");
      ConsolePrint.ShowListOfCards(CurrentPlayer.Hand);
      const string discardMessageTwo = "Activaste la habilidad de The Undertaker, " +
                                       "debes escoger otra carta de tu mano para descartar";
      var playerInputTwo = ConsolePrint.SelectACard(
        CurrentPlayer.Hand, discardMessageTwo, playerInputMessage, true);
      DiscardCard(CurrentPlayer,playerInputTwo,"Hand");
      ConsolePrint.ShowListOfCards(CurrentPlayer.RingSide);
      const string drawFromRingsideMessage = "Ahora debes escoger una carta de tu Ring side para poner en tu mano";
      var playerInputRingSideMessage =  $"\n (Ingresa un número entre 0 y {CurrentPlayer.RingSide.Count - 1}): ";
      var playerInputRingside = ConsolePrint.SelectACard(
        CurrentPlayer.RingSide, drawFromRingsideMessage, playerInputRingSideMessage, true);
      CurrentPlayer.Hand.Add(CurrentPlayer.RingSide[playerInputRingside]);
      CurrentPlayer.RingSide.RemoveAt(playerInputRingside);
    }
}