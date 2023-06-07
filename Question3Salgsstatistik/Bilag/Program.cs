using System;
using System.Linq;

namespace Bilag
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Bilag bilag = new Bilag();
            string input = null;
            while (input != "q")
            {
                Console.WriteLine("Find bog");
                input = Console.ReadLine()?.Trim();

                if (input.Length == 0)
                {
                    Console.WriteLine("\n-----LINQ Methode Find Alle----");
                    bilag.linqfuncFindAll(); 
                    return;
                }
                else
                {
                    Console.WriteLine("-----LINQ Methode----");
                    bilag.linqfuncFindBog(input);
            
                    Console.WriteLine("\n-----Lampda Methode----");
                    bilag.lampdafuncFindBog(input);

                }
            }    
            
            
            
            
            
            
            
        }
        
    }
}