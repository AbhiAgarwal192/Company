using AjiraTech.Entities;
using System;

namespace AjiraTech
{
    class Program
    {
        static void Main(string[] args)
        {
            var rules = new Rules();
            rules.Initialize();
            Platoon myPlatoon = Console.ReadLine().ParsePlatoonString();
            Platoon opponentPlatoon = Console.ReadLine().ParsePlatoonString();
            var result  = rules.DeterminePlatoonForWinning(myPlatoon,opponentPlatoon);
            Console.WriteLine(result);
        } 
    }
}
