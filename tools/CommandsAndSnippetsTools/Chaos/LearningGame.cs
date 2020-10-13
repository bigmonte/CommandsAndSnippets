using System;

namespace CommandsAndSnippetsTools
{
    public class LearningGame
    {
        private void PrintVowels()
        {
            Console.WriteLine("char[] vowels = new char[] {'a','e','i','o','u'}");

        }
        public void RunLearningGame()
        {
            PrintVowels();
            Console.WriteLine("char test  = vowels [^1]");
            Console.WriteLine("What is test?");
            Console.WriteLine("[a][e][i][o][u]");

            HandleUserAnswer(ConsoleKey.U);
           
            PrintVowels();

            Console.WriteLine("char[] test =  vowels [..2]; ");
            Console.WriteLine("What is test?");
            Console.WriteLine("a) a,e b) o,u");
           
            HandleUserAnswer(ConsoleKey.A);

            PrintVowels();

            Console.WriteLine("char[] test = vowels [2..]; ");
            Console.WriteLine("What is test?");
            Console.WriteLine("a) i,o,u     b) a,e,i     c) e,i,o");
           
            HandleUserAnswer(ConsoleKey.A);
            PrintVowels();
            
            Console.WriteLine("char test = vowels [^2]");
            Console.WriteLine("What is test?");
            Console.WriteLine("[e] [o] [i]");
           
            HandleUserAnswer(ConsoleKey.O);

            Console.WriteLine("char[] test = vowels [2..3];");
            Console.WriteLine("What is test?");
            Console.WriteLine("[e] [o] [i]");
           
            HandleUserAnswer(ConsoleKey.I);

        }
        void HandleUserAnswer(ConsoleKey correctKey)
        {
            var readKey = Console.ReadKey(true);
            var userAnswer = readKey.Key;
            
            if (userAnswer == correctKey)
            {
                Console.WriteLine("Correct!");
                return;
            }
       
            Console.WriteLine("Wrong!");
            HandleUserAnswer(correctKey);
            
        }
    }
}