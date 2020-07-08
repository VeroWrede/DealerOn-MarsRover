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
            foreach (string bound in userInput.Trim().Split(' '))
            {
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

            
            return true;
        }
    }
}
