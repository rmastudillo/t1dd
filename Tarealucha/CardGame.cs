namespace Luchalibre;

public class CardGame
{
  public Player? PlayerOne { get; set;}
  public Player?  PlayerTwo { get; set;} 
  public ConsolePrint ConsolePrint { get; set;} 
  public CardGame(ConsolePrint consolePrint) => this.ConsolePrint = consolePrint;

  void ChoosingDeck()
  {
    ConsolePrint.WelcommingMessage();
  }
}