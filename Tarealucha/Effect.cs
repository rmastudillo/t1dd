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
    private bool CardEffectDrawCards(Player player, Card card,string effectAs)
    {
      Console.WriteLine("Robar cartas desde una carta efecto");
      return true;
    }
    private void LoadEffects()
    {
      DictionaryOfCardEffects["draw"] =  new Func<Player, Card, string,bool>(CardEffectDrawCards);
    }

    public void DiscardCard(Player player, int cardIndex, string from)
    {
      List<Card>? fromList = null;
      var toList = player.RingSide;
      if (from == "Hand") fromList = player.Hand;
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
}