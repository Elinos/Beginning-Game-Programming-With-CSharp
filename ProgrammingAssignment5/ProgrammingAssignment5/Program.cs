using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingAssignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Random generator
            Random rng = new Random();

            int playerOneRoll;
            int playerTwoRoll;

            int playerOneWins;
            int playerTwoWins;
            bool playAnotherGame = true;

            Console.WriteLine("Welcome to WAR!\n");

            while (playAnotherGame)
            {
                playerOneWins = 0;
                playerTwoWins = 0;
                for (int i = 0; i < 21; i++)
                {
                    //Generate both players rolls
                    playerOneRoll = rng.Next(1, 14);
                    playerTwoRoll = rng.Next(1, 14);

                    //Check if the rolls are equal
                    while (playerOneRoll == playerTwoRoll)
                    {
                        Console.WriteLine("WAR! P1:{0}    P2:{1}", playerOneRoll, playerTwoRoll);
                        playerOneRoll = rng.Next(1, 14);
                        playerTwoRoll = rng.Next(1, 14);
                    }

                    //Compare the rolls and print the winner
                    if (playerOneRoll > playerTwoRoll)
                    {
                        Console.WriteLine("BATTLE: P1:{0}   P2:{1}    P1 Wins!", playerOneRoll, playerTwoRoll);
                        playerOneWins++;
                    }
                    else
                    {
                        Console.WriteLine("BATTLE: P1:{0}   P2:{1}    P2 Wins!", playerOneRoll, playerTwoRoll);
                        playerTwoWins++;
                    }
                }
                Console.WriteLine();

                //Check who is the overall winner
                if (playerOneWins > playerTwoWins)
                {
                    Console.WriteLine("P1 is the overall Winner with {0} battles!\n", playerOneWins);
                }
                else
                {
                    Console.WriteLine("P2 is the overall Winner with {0} battles!\n", playerTwoWins);
                }

                //Check if the player wants to play another game
                Console.Write("Do you want to play again (y/n)? ");
                string answer = Console.ReadLine();
                Console.WriteLine();
                if (answer == "n" || answer == "N")
                {
                    playAnotherGame = false;
                }
            }
        }
    }
}
