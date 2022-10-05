// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System;
using Luchalibre;

const string folder = @"src";
const string imageName = "cards_all.json";
var path = Path.Combine (folder,imageName) ;
var filecard = new Filemanager();
var cards = Filemanager.LoadCards(path);
var cards_1 = (Card) cards["Back Body Drop"].Clone();


cards_1.Damage = 12903;
Console.WriteLine(cards["Back Body Drop"].ToString());
Console.WriteLine(cards_1.ToString());

const string deckFolder = @"decks";
const string deckName = "07.txt";
var deckPath = Path.Combine (deckFolder,deckName) ;
var superstar = File.ReadAllLines(deckPath).First();
Console.WriteLine(superstar);
var deck = new Deck(superstar);
void LegitDeck(string path)
{
    var lines = File.ReadAllLines(path);
    foreach (var line in lines.Skip(1))
    {
        var lineCountName = line.Split(new[] { ' ' }, 2);
        var newCardName = lineCountName[1];
        var newCardToAdd = cards[newCardName];
        var numberOfCardsToAdd = Convert.ToInt32(lineCountName[0]);
        var cardsToAdd = Enumerable.Repeat(newCardName, numberOfCardsToAdd);
        foreach (var newCard in cardsToAdd) deck.AddCard(newCardToAdd) ;
    }
}
Console.WriteLine(deck.ToString());
LegitDeck(deckPath);
Console.WriteLine(deck.ToString());
