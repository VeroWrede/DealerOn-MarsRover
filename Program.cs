using System;
using System.Collections.Generic;

namespace Mars_Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            //get grid size
            Console.WriteLine("Welcome on Mars!\nEnter Grid bounds:");
            var userInput = Console.ReadLine();
            bool validInput = CheckUserInput(userInput);
            if (!validInput)
            {
                return;
            }
            List<int> gridCoordinates = new List<int>();
            foreach(string coordinate in userInput.Trim().Split(' '))
            {
                gridCoordinates.Add(int.Parse(coordinate));
            }
            
            // get rover starting point
            Console.WriteLine("Please enter the rovers starting point:");
            var startingPoint = Console.ReadLine();
            List<string> startingCoordinates = new List<string>();
            foreach(string coordinate in startingPoint.Trim().Split(' '))
            {
                startingCoordinates.Add(coordinate);
            }
            bool validStartingPoint = CheckStartingPoint(startingCoordinates, gridCoordinates);
            if(!validStartingPoint)
            {
                return;
            }

            // get turning instructions
            Console.WriteLine("Please enter movement instructions:");
            var instructionInput = Console.ReadLine();
            List<string> instructionOptions = new List<string>() {"L", "R", "M"};
            List<string> instructions = new List<string>();
            

        }

        static bool CheckStartingPoint(List<string> startingCoordinates, List<int> gridCoordinates)
        {
            // only three commands
            if (startingCoordinates.Count != 3)
            {
                Console.WriteLine("Enter three starting point coordinates. Try again.");
                return false;
            }
            // item 1 and 2 must be grid coordinates within the given grid
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    int coordinate = int.Parse(startingCoordinates[i]);
                    if (coordinate < 0 || coordinate > gridCoordinates[i])
                    {
                        Console.WriteLine("Rover not in Grid.");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("starting coordinates out of bounds");
                    return false;
                }
            }
            List<string> cardinals = new List<string>(){"N", "S", "E", "W"};
            // third grid coordinate must be valid cardinal 
            if(!cardinals.Contains(startingCoordinates[2].ToUpper()))
            {
                Console.WriteLine("Wrong direction of rover");
                return false;
            }
            return true;
        }

        static bool CheckUserInput(string userInput)
        {
            int inputCount = 0;
            foreach (string bound in userInput.Trim().Split(' '))
            {
                try
                {
                    int.Parse(bound);
                    inputCount ++;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"input {bound} is not a number. \nTry again");
                    return false;
                }
            }
            if (inputCount != 2)
            {
                Console.WriteLine("Please enter two Coordinates \nTry again");
                return false;
            }
            return true;
        }
    }
}
