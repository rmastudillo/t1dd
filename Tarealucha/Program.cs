// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System;
using Luchalibre;
const string cardsFolder = @"src";
const string imageName = "cards_all.json";
const string deckFolder = @"decks";
var allCardsPath = Path.Combine (cardsFolder,imageName) ;


ConsolePrint consolePrint = new ConsolePrint();
CardGame RawDeal = new CardGame(consolePrint);


var filecard = new Filemanager();
var cards = Filemanager.LoadCards(allCardsPath);

var deckPathp = consolePrint.ChooseDeck(deckFolder);
var superstar = File.ReadAllLines(deckPathp).First();
Console.WriteLine(superstar);
var deck = new Deck(superstar);
void LegitDeck(string deckPath)
{
    var lines = File.ReadAllLines(deckPath);
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
LegitDeck(deckPathp);
Console.WriteLine(deck.ToString());
