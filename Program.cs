﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mars_Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mars!\n");
            Console.WriteLine("Please enter two numbers separated by a space as Grid bounds (Example: 8 5).\n");
            var gridInput = Console.ReadLine();
            bool run = true;
            try 
            {
                (int x, int y) gridCoordinates = ParseGridInput(gridInput);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem with your input, please try again!");
            }
            while (run)
            {
                Console.WriteLine("Do you want to deploy a rover? (y/n) \n");
                string repeat = Console.ReadLine();
                run = ParseRepeat(repeat);
            }
            // int numOfRovers = RoverNumber();
            // for (int i = 0; i < numOfRovers; i++)
            // {
            //     SingleRoverInstruction(gridCoordinates);
            // }
        }

        static bool ParseRepeat(string repetition)
        {
            if (repetition.ToUpper().Trim() != "Y")
            {
                Console.WriteLine("Thank you for using this application! Good bye.");
                return false;
            }
            Console.WriteLine("You chose to deploy another rover.\n");
            return true;
        }

        static (int x, int y, string orientation) SetStartingCoordinates(string startingPoint, List<int> gridCoordinates)
        {
            bool validCoordinates = CheckStartingPoint(startingPoint, gridCoordinates);

            if (validCoordinates)
            {
                var xCoordinate = startingPoint.Trim().Split(' ')[0];
                var yCoordinate = startingPoint.Trim().Split(' ')[1];
                var orientation = startingPoint.Trim().Split(' ')[2].ToUpper();

                return (int.Parse(xCoordinate), int.Parse(yCoordinate), orientation);
            }
            return (-1, -1, null);
        }


        static void SingleRoverInstruction(List<int> gridCoordinates)
        {
            // get rover starting point
            Console.WriteLine("Please enter the rover's starting point in the grid and the direction it should face all separated by one space (Example: 2 4 N ).");
            var startingPoint = Console.ReadLine();
            (int xCoordinate, int yCoordinate, string orientation) startingCoordinates = SetStartingCoordinates(startingPoint, gridCoordinates);
            if(startingCoordinates.orientation == null)
            {
                return;
            }

            // get turning instructions
            Console.WriteLine("Please enter movement instructions as a single continuous string. You can chose L (turn left), R (turn right), or M (move one step forward).");
            var instructionInput = Console.ReadLine().ToUpper();
            bool validInstructions = CheckInstructions(instructionInput);
            if (!validInstructions)
            {
                return;
            }
            
            // starting point already set as List<string> startingCoordinates

            List<int> roverPosition = new List<int>() {startingCoordinates.xCoordinate, startingCoordinates.yCoordinate};

            // instructions already set as var/string instructionInput

            // start with "W" so that modulo results match indexes
            List<string> cardinals = new List<string>(){"W","N", "E", "S"};
            int tracker = cardinals.IndexOf(startingCoordinates.orientation);

            // moving rover
            for (int i = 0; i < instructionInput.Length; i++)
            {
                // instruction is to move
                if(Char.ToString(instructionInput[i]) == "M")
                {
                    //rover looks north (up)
                    if((startingCoordinates.orientation) == "N")
                    {
                        roverPosition[1] += 1;
                        if(roverPosition[1] > gridCoordinates[1])
                        {
                            Console.WriteLine("Rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((startingCoordinates.orientation) == "S")
                    {
                        roverPosition[1] -= 1;
                        if (roverPosition[1] < 0)
                        {
                            Console.WriteLine("rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((startingCoordinates.orientation) == "E")
                    {
                        roverPosition[0] += 1;
                        if(roverPosition[0] > gridCoordinates[0])
                        {
                            Console.WriteLine("Rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((startingCoordinates.orientation) == "W")
                    {
                        roverPosition[0] -= 1;
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
                    tracker += 1;
                }
                // instruction to turn left
                if (Char.ToString(instructionInput[i]) == "L")
                {
                    tracker -= 1;
                }
                if (tracker <= 0)
                {
                    tracker += 4;
                }
            }
            tracker = (tracker%4);
            startingCoordinates.orientation = cardinals[tracker];
            Console.WriteLine($"{roverPosition[0]} {roverPosition[1]} {startingCoordinates.orientation}");
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

        static bool CheckStartingPoint(string startingPoint, List<int> gridCoordinates)
        {
            var coordinatesArr = (startingPoint.Trim().Split(' '));
            // only three commands
            if (coordinatesArr.Length != 3)
            {
                Console.WriteLine("Must enter three starting point coordinates.");
                return false;
            }
            // item 1 and 2 in startingCoordinates must be grid coordinates within the given grid
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    int coordinate = int.Parse(coordinatesArr[i]);
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
            if(!cardinals.Contains(coordinatesArr[2].ToUpper()))
            {
                Console.WriteLine("Invalid orientation.");
                return false;
            }
            return true;
        }

        static (int x, int y) ParseGridInput(string gridInput)
        {
            var xValue = gridInput.Trim().Split(' ')[0];
            int parsedX = 0;
            var yValue = gridInput.Trim().Split(' ')[1];
            int parsedY = 0;
            foreach (string bound in gridInput.Trim().Split(' '))
            {
                try
                {
                    parsedX = int.Parse(xValue);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("You entered an invalid Y Value.");
                }
                try
                {
                    parsedY = int.Parse(yValue);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("You entered an invalid Y Value.");
                }
            }
            return (parsedX, parsedY);
        }
    }
}
