using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleCards;

namespace ProgrammingAssignment3
{
    /// <summary>
    /// Let you play a hand of Blackjack
    /// </summary>
    class Program
    {
        /// <summary>
        /// Let you play a hand of Blackjack
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            //create deck and two hands
            Deck deck = new Deck();
            BlackjackHand dealerHand = new BlackjackHand("Dealer");
            BlackjackHand playerHand = new BlackjackHand("Player");

            //print welcome message
            Console.WriteLine("Welcome friend!");
            Console.WriteLine("You are now going to play a single hand of Blackjack\n");

            //shuffle the deck
            deck.Shuffle();

            //deal two cards for player
            playerHand.AddCard(deck.TakeTopCard());
            playerHand.AddCard(deck.TakeTopCard());

            //deal two cards for dealer
            dealerHand.AddCard(deck.TakeTopCard());
            dealerHand.AddCard(deck.TakeTopCard());

            //make all player's cards face up
            playerHand.ShowAllCards();

            //make first dealer's card face up
            dealerHand.ShowFirstCard();

            //print both hands
            playerHand.Print();
            dealerHand.Print();

            //ask player if he wants to "hit"
            playerHand.HitOrNot(deck);

            //make all dealer's card face up
            dealerHand.ShowAllCards();

            //print both hands
            playerHand.Print();
            dealerHand.Print();

            //print scores
            Console.WriteLine("Player score: {0}", playerHand.Score);
            Console.WriteLine("Dealer score: {0}", dealerHand.Score);
        }
    }
}
