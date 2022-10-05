namespace Luchalibre;

public class Player
{
    public string Name { get; set;}
    public Deck Deck { get; set;}
    public List<Card> Hand { get; set;} = new List<Card>();
    public List<Card> RingArea { get; set;} = new List<Card>();
    public List<Card> DiscardPile { get; set;} = new List<Card>();
    public int CurrentFortitude { get; set; } = 0;

    public Player(Deck deck, string name)
    {
        Deck = deck;
        Name = name;
    }
}