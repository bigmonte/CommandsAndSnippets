using System.Linq;

namespace CommandsAndSnippetsTools
{
    public class OtherTests
    {
        /*
         *Indices and ranges also work with the CLR types Span<T>
         * and ReadOnlySpan<T> (see “Span<T> and Memory<T>” in Chapter 5).
         * 
         * https://learning.oreilly.com/library/view/c-80-in/9781492051121/ch02.html#predefined_type_examples
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
            
            System.Console.WriteLine($"Last Element: {lastElement}; Second To Last: {secondToLast}");
        }

        public void RunRangeTests()
        {
            System.Console.WriteLine("Running Range Tests... ");

            char[] vowels = new char[] {'a','e','i','o','u'};

            char[] firstTwo =  vowels [..2];    // 'a', 'e'
            char[] lastThree = vowels [2..];    // 'i', 'o', 'u'
            char[] middleOne = vowels [2..3];   // 'i'
            
            System.Console.WriteLine($"vowels [..2]: {CharArrToString(firstTwo)}; vowels [2..]: {CharArrToString(lastThree)} ; vowels [2..3]: {CharArrToString(middleOne)}");

        }

        string CharArrToString(char[] charArr)
        {
            string final = "";
            foreach (var c in charArr)
            {
                final += c + " ";
            }

            return final;
        }
    }
}