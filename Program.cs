using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mars_Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mars!\n");
            bool correctGridInput = false;
            (int x, int y)? gridCoordinates = null;
            while (!correctGridInput)
            {
                Console.WriteLine("Please enter two numbers separated by a space as Grid bounds (Example: 8 5).");
                var gridInput = Console.ReadLine();
                try
                {
                    gridCoordinates = ParseGridInput(gridInput);
                    correctGridInput = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("There was a problem with your input, please try again!");
                }
            }
            bool run = true;
            
            while (run)
            {
                Console.WriteLine("Please enter the rover's starting point in the grid and the direction it should face all separated by one space (Example: 2 4 N ).");
                var startingPoint = Console.ReadLine();
                bool validCoordinates = CheckStartingPoint(startingPoint, gridCoordinates.Value);
                
                if (validCoordinates)
                {
                    (int xCoordinate, int yCoordinate, string orientation) startingCoordinates = ParseStartingCoordinates(startingPoint, gridCoordinates.Value);

                    Console.WriteLine("Please enter movement instructions as a single continuous string. You can chose L (turn left), R (turn right), or M (move one step forward).");
                    var instructionInput = Console.ReadLine().ToUpper();
                    
                    bool validInstructions = CheckInstructions(instructionInput);
                    if (validInstructions)
                    {
                        ProcessRoverInstruction(gridCoordinates.Value , startingCoordinates, instructionInput);
                    }
                }
                
                Console.WriteLine("Do you want to deploy another rover? (y/n) ");
                string repeat = Console.ReadLine();
                run = ParseRepeat(repeat);
            }
        }

        public static bool ParseRepeat(string repetition)
        {
            if (repetition.ToUpper().Trim() != "Y")
            {
                Console.WriteLine("Thank you for using this application! Good bye.");
                return false;
            }
            return true;
        }

        static (int x, int y, string orientation) ParseStartingCoordinates(string startingPoint, (int x , int y) gridCoordinates)
        {
            var xCoordinate = startingPoint.Trim().Split(' ')[0];
            var yCoordinate = startingPoint.Trim().Split(' ')[1];
            var orientation = startingPoint.Trim().Split(' ')[2].ToUpper();

            int parsedXCoordinate = int.Parse(xCoordinate);
            int parsedYCoordinate = int.Parse(yCoordinate);
            return (parsedXCoordinate, parsedYCoordinate, orientation);
        }


        static void ProcessRoverInstruction((int gridX, int gridY) gridCoordinates, (int x, int y, string orientation) roverCoordinates, string instructionInput)
        {
            List<string> cardinals = new List<string>(){"W","N", "E", "S"};
            int tracker = cardinals.IndexOf(roverCoordinates.orientation);

            // moving rover
            for (int i = 0; i < instructionInput.Length; i++)
            {
                // instruction is to move
                if(Char.ToString(instructionInput[i]) == "M")
                {
                    //rover looks north (up)
                    if((roverCoordinates.orientation) == "N")
                    {
                        roverCoordinates.y += 1;
                        if(roverCoordinates.y > gridCoordinates.gridY)
                        {
                            Console.WriteLine("Rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((roverCoordinates.orientation) == "S")
                    {
                        roverCoordinates.y -= 1;
                        if (roverCoordinates.y < 0)
                        {
                            Console.WriteLine("rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((roverCoordinates.orientation) == "E")
                    {
                        roverCoordinates.x += 1;
                        if(roverCoordinates.x > gridCoordinates.gridX)
                        {
                            Console.WriteLine("Rover left surveilance grid.");
                            return;
                        }
                    }
                    else if ((roverCoordinates.orientation) == "W")
                    {
                        roverCoordinates.x -= 1;
                        if (roverCoordinates.x < 0)
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
            roverCoordinates.orientation = cardinals[tracker];
            Console.WriteLine($"{roverCoordinates.x} {roverCoordinates.y} {roverCoordinates.orientation}");
        }

        public static bool CheckInstructions(string instructionsInput)
        {
            List<string> instructionOptions = new List<string>() {"L", "R", "M"};
            for (int i = 0; i < instructionsInput.Length; i++)
            {
                var currentChar = Char.ToString(instructionsInput[i]);
                
                if (!instructionOptions.Contains(currentChar))
                {
                    Console.WriteLine($"Instruction {currentChar} is invalid. Rover deployment aborted.");
                    return false;
                }
            }
            return true;
        }

        static bool CheckStartingPoint(string startingPoint, (int x , int y) gridCoordinates)
        {
            var coordinatesArr = (startingPoint.Trim().Split(' '));
            // only three commands
            if (coordinatesArr.Length != 3)
            {
                Console.WriteLine("You must enter three starting point coordinates. Please try again!");
                return false;
            }
            // item 1 and 2 in startingCoordinates must be grid coordinates within the given grid
            try
            {
                int coordinate = int.Parse(coordinatesArr[0]);
                if (coordinate < 0 || coordinate > gridCoordinates.x)
                {
                    Console.WriteLine("Rover's X coordinate is not within the Grid.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("starting coordinates out of bounds");
                return false;
            }
            try
            {
                int coordinate = int.Parse(coordinatesArr[1]);
                if (coordinate < 0 || coordinate > gridCoordinates.y)
                {
                    Console.WriteLine("Rover's Y coordinate is not within the Grid.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("starting coordinates out of bounds");
                return false;
            }
            List<string> cardinals = new List<string>(){"N", "S", "E", "W"};
            // third grid coordinate must be valid cardinal 
            if(!cardinals.Contains(coordinatesArr[2].ToUpper()))
            {
                Console.WriteLine("Invalid orientation Input. Rover deployment aborted.");
                return false;
            }
            return true;
        }

        public static (int x, int y) ParseGridInput(string gridInput)
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
