using System;
using BallOfMud.Services;

namespace BallOfMud
{
    class Program
    {
        static void Main(string[] args)
        {
            BigClassFacade bigClass = new BigClassFacade();

            bigClass.IncreaseBy(numberToAdd: 50);
            bigClass.DecreaseBy(numberToSubtract: 20);

            Console.WriteLine($"Final Number : {bigClass.GetCurrentValue()}");
        }
    }
}