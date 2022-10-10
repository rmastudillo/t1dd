namespace Luchalibre;

public class Player
{
    public string Name { get; set;}
    public Deck Deck { get; set;}
    public List<Card> Hand { get; set;} = new List<Card>();
    public List<Card> RingArea { get; set;} = new List<Card>();
    public List<Card> RingSide { get; set;} = new List<Card>();
    public int CurrentFortitude { get; set; } = 0;
    public bool SuperStarHabilityAvailable = true;
    public bool Playing = true;

    public Player(Deck deck, string name)
    {
        Deck = deck;
        Name = name;
    }
}