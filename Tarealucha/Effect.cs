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
    
}