using System.Text.Json;
using Luchalibre;

// See https://aka.ms/new-console-template for more information

string folder = @"src";
string imageName = "cards_all.json";
Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
string path = Path.Combine (folder,imageName) ;
Console.WriteLine("Hello, World!");
string jsonString = File.ReadAllText(path);
Filemanager filecard = new Filemanager();
List<Card> ?cards = Filemanager.LoadCards(jsonString);
Console.WriteLine(cards[4].EffectAsManeuver[0].Options[0][0].Type);