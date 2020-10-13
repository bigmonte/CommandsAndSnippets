using System;
using System.Linq;
using System.Text;

namespace CommandsAndSnippetsTools
{
    public class CSharpChaos
    {
        /*
         *Indices and ranges also work with the CLR types Span<T>
         * and ReadOnlySpan<T> 
         *          
         * Indices let you refer to elements relative to the end of an array,
         * with the ^ operator. ^1 refers to the last element,
         * ^2 refers to the second-to-last element, and so on:
         */
        
        public void RunIndiceTests()
        {
            System.Console.WriteLine("Running Indice Tests... char[] {'a','e','i','o','u'}");
            
            char[] vowels = new char[] {'a','e','i','o','u'};
            char lastElement  = vowels [^1];   
            char secondToLast = vowels [^2];   
            
            var d = System.Console.ReadKey();

            if (d.Key == ConsoleKey.A)
            {
                System.Console.WriteLine("\nOK");
            }
            
            System.Console.WriteLine($"Last Element: {lastElement}; Second To Last: {secondToLast}");
        }

     
        
        
        public void RunRangeTests()
        {
            System.Console.WriteLine("Running Range Tests... ");

            char[] vowels = new char[] {'a','e','i','o','u'};

            char[] firstTwo =  vowels [..2];    // 'a', 'e'
            
            // or     Range firstTwoRange = 0..2;
            //        char[] firstTwo = vowels [firstTwoRange]
            
            char[] lastThree = vowels [2..];    // 'i', 'o', 'u'
            char[] middleOne = vowels [2..3];   // 'i'
            char[] lastTwo = vowels [^2..];
            
            System.Console.WriteLine($"[..2]: {CharArrToString(firstTwo)}");
            System.Console.WriteLine($"[2..]: {CharArrToString(lastThree)}");
            System.Console.WriteLine($"[2..3]: {CharArrToString(middleOne)}");
            System.Console.WriteLine($"[^2..]: {CharArrToString(lastTwo)}");
            

        }

        public void StringBuilderAndGC()
        {
            System.Console.WriteLine("Running String Builder Tests... ");

            StringBuilder ref1 = new StringBuilder ("object1");
            Console.WriteLine (ref1);
            // The StringBuilder referenced by ref1 is now eligible for GC.

            StringBuilder ref2 = new StringBuilder ("object2");
            StringBuilder ref3 = ref2;
            // The StringBuilder referenced by ref2 is NOT yet eligible for GC.

            Console.WriteLine (ref3);                   // object2
        }
        
        static void Split (string name, out string firstNames,
            out string lastName)
        {
            int i = name.LastIndexOf (' ');
            firstNames = name.Substring (0, i);
            lastName   = name.Substring (i + 1);
        }

        void DiscardAndShorten()
        {
            Split ("Stevie Ray Vaughan", out string a, out string b);
            Console.WriteLine (a);                      // Stevie Ray
            Console.WriteLine (b);  
           
            // discard lastName
            Split ("Stevie Ray Vaughan", out string y, out _);   // Discard the 2nd param
            Console.WriteLine (y);
        }
        
        static string x = "imStatic";
        static ref readonly string Prop => ref x; // I don't create unnecessary copy, and I can't change the original

        static void RefLocals()
        {

            int[] numbers = { 0, 1, 2, 3, 4 };
            ref int numRef = ref numbers [2];
        }

        void NullCoalescing() // C# 8
        {
            string s1 = null;
            string s2 = s1 ?? "nothing"; 
            string s3 = null;
            
            s3 ??= "something";
            Console.WriteLine (s3);  // something

            s3 ??= "everything";
            Console.WriteLine (s3);  // something
        }

        string CharArrToString(char[] charArr)
        {
            return charArr.Aggregate("", 
                (current, c) => current + $"{c} ");
        }
    }
}