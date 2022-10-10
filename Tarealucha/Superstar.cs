namespace Luchalibre;

public  class Superstar
{
    public string Name { set; get; }
    public int HandSize { set; get; }
    public int SuperstarValue {set; get; }
    public string SuperstarHability { set; get; }

    public Superstar(int handSize, int superstarValue, string superstarHability,string name)
    {
        Name = name;
        if (Name == "KANE")
        {
            Console.WriteLine("AQSDJASIKDKASÑDJASÑLDJASÑL");
        }
        HandSize = handSize;
        SuperstarValue = superstarValue;
        SuperstarHability = superstarHability;
    }
    
}