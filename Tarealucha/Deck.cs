using System.Net.Mail;

namespace Luchalibre;

public class Deck
{
    public List<Card> Cards { get; set;} = new();
    public bool IsValid { get; set; } = true;
    public List<string>? InvalidReason { get; set; }
    public Dictionary<string, int> CardCount { get; set; } = new();
    public int DeckSize{ get; set; }
    public string TypeOfDeck { get; set; } = "fair";

    public void AddCard(Card card)
    {
        var newCard = (Card)card.Clone();
        if (!CardCount.ContainsKey(card.Title)) CardCount[newCard.Title] = 0;
        CheckValidCard(newCard);
        Cards.Add(newCard);
        CardCount[newCard.Title] ++;
        DeckSize++;
        if (newCard.Subtypes.Contains("Heel")) TypeOfDeck = "Heel";
        if (newCard.Subtypes.Contains("Face")) TypeOfDeck = "Face" ;
    }

    private void CheckValidCard(Card card)
    {
        if (DeckSize>=60) IsValid = false;
        if (card.Subtypes.Contains("unique") & CardCount[card.Title]>0) IsValid = false ;
        if (CardCount[card.Title]>=3 & !card.Subtypes.Contains("setup")) IsValid = false ;
        if (card.Subtypes.Contains("Heel")& TypeOfDeck=="Face") IsValid = false ;
        if (card.Subtypes.Contains("Face")& TypeOfDeck=="Heel") IsValid = false ;

    }

    public override string ToString()
    {
        var deckCards = $"[{string.Join(",", Cards.Select(card => card.Title).ToArray())}]\n";
        return $"Deck Cards: {deckCards}" +
               $"Deck Size: {DeckSize}\n" +
               $"Type of deck: {TypeOfDeck}\n" +
               $"Is valid?: {IsValid.ToString()}\n";
    }
}