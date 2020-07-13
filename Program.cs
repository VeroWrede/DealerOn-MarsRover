using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mars_Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome on Mars!");
            //get grid size, must be in main if all rovers are to deploy in the same grid
            Console.WriteLine("Enter Grid bounds:");
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

            // this is optional
            int numOfRovers = RoverNumber();
            for (int i = 0; i < numOfRovers; i++)
            {
                SingleRoverInstruction(gridCoordinates, userInput);
            }

        }

        static int RoverNumber()
        {
            Console.WriteLine("How many rovers would you like to deploy?");
            var rovers = Console.ReadLine();
            try
            {
               int.Parse(rovers);
            }
            catch (Exception e)
            {
                Console.WriteLine("Please enter a numeric value.");
                return 0;
            }
            return int.Parse(rovers);
        }

        static void SingleRoverInstruction(List<int> gridCoordinates, string userInput)
        {
            
            
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
            var instructionInput = Console.ReadLine().ToUpper();
            bool validInstructions = CheckInstructions(instructionInput);
            if (!validInstructions)
            {
                return;
            }
            
            // starting point already set as List<string> startingCoordinates

            List<int> roverPosition = new List<int>() {int.Parse(startingCoordinates[0]), int.Parse(startingCoordinates[1])};

            // instructions already set as var/string instructionInput

            // moving rover
            for (int i = 0; i < instructionInput.Length; i++)
            {
                // instruction is to move
                if(Char.ToString(instructionInput[i]) == "M")
                {
                    //rover looks north (up)
                    if((startingCoordinates[2]) == "N")
                    {
                        roverPosition[1] = roverPosition[1] + 1;
                        if(roverPosition[1] > gridCoordinates[1])
                        {
                            Console.WriteLine("Rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((startingCoordinates[2]) == "S")
                    {
                        roverPosition[1] = roverPosition[1] - 1;
                        if (roverPosition[1] < 0)
                        {
                            Console.WriteLine("rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((startingCoordinates[2]) == "E")
                    {
                        roverPosition[0] = roverPosition[0] + 1;
                        if(roverPosition[0] > gridCoordinates[0])
                        {
                            Console.WriteLine("Rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((startingCoordinates[2]) == "W")
                    {
                        roverPosition[0] = roverPosition[0] - 1;
                        if (roverPosition[0] < 0)
                        {
                            Console.WriteLine("rover left surveilance grid.");
                            return;
                        }
                    }
                }
                // intruction to turn right
                if (Char.ToString(instructionInput[i]) == "R")
                {
                    if ((startingCoordinates[2]) == "N")
                    {
                        startingCoordinates[2] = "E";
                    }
                    else if ((startingCoordinates[2]) == "E")
                    {
                        startingCoordinates[2] = "S";
                    }
                    else if ((startingCoordinates[2]) == "S")
                    {
                        startingCoordinates[2] = "W";
                    }
                    else if ((startingCoordinates[2]) == "W")
                    {
                        startingCoordinates[2] = "N";
                    }
                }
                // instruction to turn left
                if (Char.ToString(instructionInput[i]) == "L")
                {
                    if ((startingCoordinates[2]) == "N")
                    {
                        startingCoordinates[2] = "W";
                    }
                    else if ((startingCoordinates[2]) == "E")
                    {
                        startingCoordinates[2] = "N";
                    }
                    else if ((startingCoordinates[2]) == "S")
                    {
                        startingCoordinates[2] = "E";
                    }
                    else if ((startingCoordinates[2]) == "W")
                    {
                        startingCoordinates[2] = "S";
                    }
                }
            }
            
            /*Console.WriteLine($"rover position x: {roverPosition[0]}");
            Console.WriteLine($"rover position y: {roverPosition[1]}");
            Console.WriteLine($"rover orientation: {startingCoordinates[2]}");
            */
            Console.WriteLine($"{roverPosition[0]} {roverPosition[1]} {startingCoordinates[2]}");

        }

        static bool CheckInstructions(string instructionsInput)
        {
            List<string> instructionOptions = new List<string>() {"L", "R", "M"};
            List<string> instructions = new List<string>();
            for (int i = 0; i < instructionsInput.Length; i++)
            {
                if (!instructionOptions.Contains(Char.ToString(instructionsInput[i])))
                {
                    Console.WriteLine("Invalid instructions.");
                    return false;
                }
            }
            return true;


        }

        static bool CheckStartingPoint(List<string> startingCoordinates, List<int> gridCoordinates)
        {
            // only three commands
            if (startingCoordinates.Count != 3)
            {
                Console.WriteLine("Enter three starting point coordinates. Try again.");
                return false;
            }
            startingCoordinates[2] = startingCoordinates[2].ToUpper();
            // item 1 and 2 in startingCoordinates must be grid coordinates within the given grid
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
            if(!cardinals.Contains(startingCoordinates[2]))
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
