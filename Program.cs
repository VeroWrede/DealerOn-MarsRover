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
            var gridInput = Console.ReadLine();
            List<int> gridCoordinates = CheckGridInput(gridInput);
            if (gridCoordinates[0] == -1)
            {
                return;
            }
            // this is optional
            int numOfRovers = RoverNumber();
            for (int i = 0; i < numOfRovers; i++)
            {
                SingleRoverInstruction(gridCoordinates);
            }

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

        static void SingleRoverInstruction(List<int> gridCoordinates)
        {
            // get rover starting point
            Console.WriteLine("Please enter the rovers starting point:");
            var startingPoint = Console.ReadLine();
            (int xCoordinate, int yCoordinate, string orientation) startingCoordinates = SetStartingCoordinates(startingPoint, gridCoordinates);
            if(startingCoordinates.orientation == null)
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
            
            List<int> roverPosition = new List<int>() {startingCoordinates.xCoordinate, startingCoordinates.yCoordinate};

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
                            Console.WriteLine("Rover left surveilance grid.");
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

        static List<int> CheckGridInput(string gridInput)
        {
            int inputCount = 0;
            List<int> gridCoordinatesNew = new List<int>();
            List<int> badInput = new List<int>() {-1};
            foreach (string bound in gridInput.Trim().Split(' '))
            {
                try
                {
                    gridCoordinatesNew.Add(int.Parse(bound));
                    inputCount ++;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"input {bound} is not a number. \nTry again");
                    return badInput;
                }
            }
            if (inputCount != 2)
            {
                Console.WriteLine("Please enter two Coordinates \nTry again");
                return badInput;
            }
            return gridCoordinatesNew;
        }
    }
}
