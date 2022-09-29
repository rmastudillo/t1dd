namespace Deck
{
  public class Card
  {
    public string? Title  { get ; private set ; }
    public string[] Types { get ; private set ; }
    public string[] Subtypes { get ; private set ; }
    public int Fortitude { get ; private set ; }
    public int Damage  { get ; private set ; }
    public int StunValue { get ; private set ; }
    public string CardEffect { get ; private set ; }
    public Dictionary<string,List<string>> Effect { get ; private set ; }
    public string CardRequieredPlayedBeforeThis  { get ; private set ; }
    public bool CanBeReversed { get ; private set ; }
    public Card ( string title , string[] types ,string[] subtypes,int fortitude,int damage,int stunvalue,Dictionary<string,List<string>> effect,string cardeffect,string cardrequieredplayedbeforethis,bool canbereversed ) {
    Title=title; 
    Types=types;
    Subtypes=subtypes;
    Fortitude=fortitude; 
    Damage=damage;
    StunValue=stunvalue;
    CardEffect=cardeffect;
    Effect=effect;
    CardRequieredPlayedBeforeThis=cardrequieredplayedbeforethis;
    CanBeReversed=canbereversed;
    }
    private string DirectToString (Dictionary<string,List<string>> dictionary)
    {
    return "{\n" + string.Join(",\n", dictionary.Select( kv =>"    " + kv.Key + " = " +"[" + string.Join(",", kv.Value) + "]" ).ToArray()) + "}";
    }
    public override string ToString()
    {
        string type_text = "[ " + string.Join(",", this.Types) + "]";
        string subtype_text = "[ " + string.Join(",", this.Subtypes) + "]";
        string effect_text = DirectToString(this.Effect);
        return String.Format("Title:  {0},\n    Types: {1},\n    Subtypes: {2},\n    Fortitude: {3},\n    Damage: {4},\n    StunValue: {5},\n    Effect: {6},\n    CardRequieredPlayedBeforeThis: {7},\n    CanBeReversed: {8}\n",
        Title, type_text, subtype_text, Fortitude,Damage,StunValue,effect_text,CardRequieredPlayedBeforeThis,CanBeReversed);
    }

  }
  public class Superstar
  {
  }
}