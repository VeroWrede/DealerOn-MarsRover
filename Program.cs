using System;
using System.Collections.Generic;

namespace Mars_Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome on Mars!\n");
            bool validInput = false;
            while (validInput == false)
            {
                validInput = CheckUserInput();
            }

            Console.WriteLine("valid user input!");
            
        }

        static bool CheckUserInput()
        {
            Console.WriteLine("Enter bounds of grid you'd like to explore:");
            var userInput = Console.ReadLine();
            int inputCount = 0;
            foreach (string bound in userInput.Trim().Split(' '))
            {
                inputCount ++;
                try
                {
                    int.Parse(bound);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"input {bound} is not a number");
                    return false;
                }
            }
            if (inputCount != 2)
            {
                Console.WriteLine("Please enter only two Coordinates \n");
                return false;
            }


            return true;
        }
    }
}
