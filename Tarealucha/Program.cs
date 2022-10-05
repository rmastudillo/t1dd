// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System;
using Luchalibre;
const string cardsFolder = @"src";
const string imageName = "cards_all.json";
const string deckFolder = @"decks";
var allCardsPath = Path.Combine (cardsFolder,imageName) ;
List<Deck> GameStart(ConsolePrint consoleP, string cardsPath,string decksFolder)
{
    var playersDecks = new List<Deck>();
    consoleP.WelcommingMessage();
    var cards = Filemanager.LoadCards(cardsPath);
    for (var i = 0; i <= 1; i++)
    {
        var deckPathp = consoleP.ChooseDeck(decksFolder);
        var superstar = File.ReadAllLines(deckPathp).First();
        var deck = new Deck(superstar);
        BuildDeck(deckPathp,cards,deck);
        playersDecks.Add(deck);
    }
    
    return playersDecks;
}

void BuildDeck(string deckPath, IReadOnlyDictionary<string, Card> cards,Deck deck)
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

bool CheckIfGameIsValid(List<Deck> decks)
{
    var gameIsValid = true;
    for (var i = 0; i <= 1; i++)
    {
        Console.WriteLine($"El Deck del jugador {i+1} {decks[i].Superstar} NO es válido");
        gameIsValid = false;
    }
    if (!gameIsValid) Console.WriteLine("El juego terminará en este momento");
    return gameIsValid;
}




var filecard = new Filemanager();
ConsolePrint consolePrint = new ConsolePrint();
CardGame RawDeal = new CardGame(consolePrint);
var game = GameStart(consolePrint, allCardsPath, deckFolder);
CheckIfGameIsValid(game);

