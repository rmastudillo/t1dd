using System.Net.Mail;

namespace Luchalibre;

public class Deck
{
    public string SuperstarCard { get; set;}
    public string Superstar{ get; set;}
    public List<Card> Cards { get; set;} = new();
    public bool IsValid { get; set; } = true;
    public Dictionary<string, int> CardCount { get; set; } = new();
    public int DeckSize{ get; set; }
    public string TypeOfDeck { get; set; } = "fair";

    public Deck(string superstar)
    {
        this.SuperstarCard = superstar;
        Superstar = SuperstarCard switch
        {
            "STONE COLD STEVE AUSTIN (Superstar Card)" => "STONE COLD",
            "THE UNDERTAKER (Superstar Card)" => "THE UNDERTAKER",
            "MANKIND (Superstar Card)" => "MANKIND",
            "HHH (Superstar Card)" => "HHH",
            "THE ROCK (Superstar Card)" => "THE ROCK",
            "KANE (Superstar Card)" => "KANE",
            "CHRIS JERICHO (Superstar Card)" => "CHRIS JERICHO",
            _ =>  throw new ArgumentNullException($"There is no Superstar that mach {SuperstarCard}")
        };
    }
    
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
        if (card.Subtypes.Contains("Unique") & CardCount[card.Title]>0) IsValid = false ;
        if (CardCount[card.Title]>=3 & !card.Subtypes.Contains("SetUp")) IsValid = false ;
        if (card.Subtypes.Contains("Heel")& TypeOfDeck=="Face") IsValid = false ;
        if (card.Subtypes.Contains("Face")& TypeOfDeck=="Heel") IsValid = false ;
        if (card.Subtypes.Contains("StoneCold") & Superstar!="STONE COLD") IsValid = false ;
        if (card.Subtypes.Contains("Undertaker") & Superstar!="THE UNDERTAKER") IsValid = false ;
        if (card.Subtypes.Contains("Mankind") & Superstar!="MANKIND") IsValid = false ;
        if (card.Subtypes.Contains("HHH") & Superstar!="HHH") IsValid = false ;
        if (card.Subtypes.Contains("TheRock") & Superstar!="THE ROCK") IsValid = false ;
        if (card.Subtypes.Contains("Kane") & Superstar!="KANE") IsValid = false ;
        if (card.Subtypes.Contains("Jericho") & Superstar!="CHRIS JERICHO") IsValid = false ;

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