using System.Diagnostics;
using System.Net.Mail;

namespace Luchalibre;

public class Deck
{
    public string SuperstarCard { get; set;}
    public Superstar Superstar{ get; set;}
    public List<Card>? Cards { get; set;} = new();
    public bool IsValid { get; set; } = true;
    public Dictionary<string, int> CardCount { get; set; } = new();
    public int DeckSize{ get; set; }
    public string TypeOfDeck { get; set; } = "fair";

    public Deck(string superstar)
    {
        this.SuperstarCard = superstar;
        Superstar = SuperstarCard switch
        {
            "STONE COLD STEVE AUSTIN (Superstar Card)" => new Superstar(
                7,
                5,
                "Once during your turn, you may draw a card, " +
                "but you must then take a card from your hand and place it on the bottom of your Arsenal.",
                "STONE COLD"),
            "THE UNDERTAKER (Superstar Card)" => new Superstar(
                6,
                4,
                "Once during your turn, you may discard 2 cards to the Ringside pile and take 1 card" +
                " from the Ringside pile and place it into your hand.",
                "THE UNDERTAKER"),
            "MANKIND (Superstar Card)" => new Superstar(
                2,
                4,
                "You must always draw 2 cards, if possible, during your draw segment. " +
                "All damage from opponent is at -1D.",
                "MANKIND"),
            "HHH (Superstar Card)" => new Superstar(
                10,
                3,
                "None, isn't the starting hand size enough!",
                "HHH"), 
            "THE ROCK (Superstar Card)" => new Superstar(
                5,
                5,
                "At the start of your turn, before your draw segment," +
                " you may take 1 card from your Ringside pile and place it on the bottom of your Arsenal.",
                "THE ROCK"), 
            "KANE (Superstar Card)" => new Superstar(
                7,
                2,
                "At the start of your turn, before your draw segment," +
                " opponent must take the top card from his Arsenal and place it into his Ringside pile.",
                "KANE"), 
            "CHRIS JERICHO (Superstar Card)" => new Superstar(
                7,
                3,
                "Once during your turn, " +
                "you may discard a card from your hand to force your opponent to discard a card from his hand.",
                "CHRIS JERICHO"), 
            _ =>  throw new ArgumentNullException($"There is no Superstar that mach {SuperstarCard}")
        };
    }
    
    public void AddCard(Card card)
    {
        var newCard = (Card)card.Clone();
        if (!CardCount.ContainsKey(card.Title)) CardCount[newCard.Title] = 0;
        CheckValidCard(newCard);
        Cards?.Add(newCard);
        CardCount[newCard.Title] ++;
        DeckSize++;
        if (newCard.Subtypes.Contains("Heel")) TypeOfDeck = "Heel";
        if (newCard.Subtypes.Contains("Face")) TypeOfDeck = "Face" ;
    }

    private void CheckValidCard(Card card)
    {
        if (DeckSize>=60) IsValid = false;
        if (card.Subtypes.Contains("Unique") & CardCount[card.Title]>0) IsValid = false ;
        if (CardCount[card.Title]>=3 & !card.Subtypes.Contains("SetUp")) IsValid = false ;
        if (card.Subtypes.Contains("Heel")& TypeOfDeck=="Face") IsValid = false ;
        if (card.Subtypes.Contains("Face")& TypeOfDeck=="Heel") IsValid = false ;
        if (card.Subtypes.Contains("StoneCold") & Superstar.Name!="STONE COLD") IsValid = false ;
        if (card.Subtypes.Contains("Undertaker") & Superstar.Name!="THE UNDERTAKER") IsValid = false ;
        if (card.Subtypes.Contains("Mankind") & Superstar.Name!="MANKIND") IsValid = false ;
        if (card.Subtypes.Contains("HHH") & Superstar.Name!="HHH") IsValid = false ;
        if (card.Subtypes.Contains("TheRock") & Superstar.Name!="THE ROCK") IsValid = false ;
        if (card.Subtypes.Contains("Kane") & Superstar.Name!="KANE") IsValid = false ;
        if (card.Subtypes.Contains("Jericho") & Superstar.Name!="CHRIS JERICHO") IsValid = false ;
    }

    public override string ToString()
    {
        var deckCards = $"[{string.Join(",", Cards.Select(card => card.Title).ToArray())}]\n";
        return $"Superstar: {Superstar} Superstarcard: {SuperstarCard}\n" +
               $"Deck Cards: {deckCards}" +
               $"Deck Size: {DeckSize}\n" +
               $"Type of deck: {TypeOfDeck}\n" +
               $"Is valid?: {IsValid.ToString()}\n";
    }
}