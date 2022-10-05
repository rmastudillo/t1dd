using System.Text.Json;
using Luchalibre;
using System;

// See https://aka.ms/new-console-template for more information

string folder = @"src";
string imageName = "cards_all.json";
Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
string path = Path.Combine (folder,imageName) ;
Console.WriteLine("Hello, World!");
string jsonString = File.ReadAllText(path);
Filemanager filecard = new Filemanager();
Dictionary<string, Card> cards = Filemanager.LoadCards(jsonString);
Card cards_1 = (Card) cards["Back Body Drop"].Clone();
cards_1.Damage = 12903;
Console.WriteLine(cards["Back Body Drop"].ToString());
Console.WriteLine(cards_1.ToString());
