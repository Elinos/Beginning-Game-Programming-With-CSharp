using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingAssignmentOne
{
    /// <summary>
    /// Application that calculates average gold-collecting performance.
    /// </summary>
    class ProgrammingAssignmentOne
    {
        /// <summary>
        /// Application that calculates average gold-collecting performance.
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            //Print welcome message
            Console.WriteLine("Welcome!");
            Console.WriteLine("This is an application, that will calculate your average gold-collecting performance.");
            Console.WriteLine();

            //Ask user to enter the gold they've collected
            Console.Write("Please enter the total gold you've collected: ");
            int totalGoldCollected = int.Parse(Console.ReadLine());
            Console.WriteLine();

            //Ask user to enter the hours they've played
            Console.Write("Please enter the total hours you've played: ");
            float totalHoursPlayed = float.Parse(Console.ReadLine());
            Console.WriteLine();

            //Initialize constant for minutes per hour
            const int MINUTES_PER_HOUR = 60;

            //Calculate total minutes played
            float totalMinutesPlayed = totalHoursPlayed * MINUTES_PER_HOUR;

            //Calculate gold / minute ratio
            float goldPerMinute = totalGoldCollected / totalMinutesPlayed;

            //Print the results
            Console.WriteLine("You've collected {0} gold", totalGoldCollected);
            Console.WriteLine("and played {0} hours", totalHoursPlayed );
            Console.WriteLine("Your gold / minute ratio is: {0}", goldPerMinute);
        }
    }
}
