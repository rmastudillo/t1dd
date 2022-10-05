using static System.Text.Json.JsonSerializer;

namespace Luchalibre;

public class Effect
{
    public string? Target { get; set; }
    public string? Type { get; set; }
    public string? From { get; set; }
}
    public class EffectAsAction:Effect
    {

        public int? Value { get; set; }
        public List<object> ?Options { get; set; }
        public string? To { get; set; }
        public string? Condition { get; set; }
        public int? Max { get; set; }
        public object? Upto { get; set; }
        public string? ApplyTo { get; set; }
        public string? Stat { get; set; }
        public string? HowLong { get; set; }
        public IfStatement? IfStatement { get; set; }
    }

    public class EffectAsDiscardByOpponent:Effect
    {
        public int Max { get; set; }
    }

    public class EffectAsManeuver:Effect
    {
        public object? Value { get; set; }
        public string? ApplyTo { get; set; }
        public string ?Stat { get; set; }
        public string? HowLong { get; set; }
        public List<List<Effect>>? Options { get; set; }
        public IfStatement? IfStatement { get; set; }
        public List<MayDo>? MayDo { get; set; }
        public string ?SpecialCondition { get; set; }
        public string ?CardTitle { get; set; }
        public string? To { get; set; }
        public string ?CardName { get; set; }
        public string ?NewSubtype { get; set; }
    }

    public class IfStatement
    {
        public string ?CardTypeBefore { get; set; }
        public int? MinDamageRequired { get; set; }
        public string ?CardTypeAfter { get; set; }
        public int? MaxDamageRequired { get; set; }
    }

    public class LoosRestriction
    {
        public string? CardPlayedBefore { get; set; }
        public string ?Restriction { get; set; }
        public int? Value { get; set; }
        public string ?CardOnRingArea { get; set; }
        public bool? PlayedAsAnAction { get; set; }
    }

    public class MayDo:Effect
    {
        public int Value { get; set; }

    }

    public class RestrictionToBePlayed
    {
        public string? CardPlayedBefore { get; set; }
        public string? CardTypeBefore { get; set; }
        public int? MinDamageRequired { get; set; }
        public string? OpponentFortitude { get; set; }
    }

    public class Card:ICloneable
    {
        public string Title { get; set; }
        public List<string> Types { get; set; }
        public List<string> Subtypes { get; set; }
        public int Fortitude { get; set; }
        public object Damage { get; set; }
        public int StunValue { get; set; }
        public string CardEffect { get; set; }
        public List<EffectAsManeuver>?EffectAsManeuver { get; set; }
        public RestrictionToBePlayed? RestrictionToBePlayed { get; set; }
        public bool? CanBeReversed { get; set; } = true;
        public LoosRestriction? LoosRestriction { get; set; }
        public List<EffectAsAction>? EffectAsAction { get; set; }
        public List<EffectAsDiscardByOpponent>? EffectAsDiscardByOpponent { get; set; }
 

    

/*7 List < Person > Load () {
    8 string fileName = " data . json ";
    9 string jsonString = File . ReadAllText ( fileName ) ;
    10 List < Person > personList = JsonSerializer . Deserialize < List < Person
    > >( jsonString ) ;
    11 return personList ;
    12 }*/
    public override string ToString()
    {
        var typeText = "[" + string.Join(",", this.Types) + "]";
        var subtypeText = "[" + string.Join(",", this.Subtypes) + "]";
        return 
            $"\n"+
            $"Title:  {Title}\n" +
            $"    Types: {typeText}\n" +
            $"    Subtypes: {subtypeText}\n" +
            $"    Fortitude: {Fortitude}\n" +
            $"    Damage: {Damage}\n" +
            $"    StunValue: {StunValue}\n" +
            $"    CardEffect: {this.CardEffect}\n" +
            $"    CanBeReversed: {this.CanBeReversed}\n";
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
    }

