using System;

namespace SPZLab1Var1
{
    class Program
    {
        static void Main(string[] args)
        {
            var coffeeMachine = new CoffeeMachine
            (
                "MyCoffeeMachine",
                100, 100, 100, 100,
                100, 100, 100, 100,
                0
            );

            Console.WriteLine($"Initial {coffeeMachine.GetStatus()}\n");

            Console.WriteLine("Buying Americano");
            coffeeMachine.BuyDrink(DrinkType.Americano, 20);
            Console.WriteLine($"After the purchase {coffeeMachine.GetStatus()}\n");

            Console.WriteLine("Buying multiple drinks: milk and cappucino");
            coffeeMachine.BuyMultipleDrinks(new[] { DrinkType.Milk, DrinkType.Cappuccino }, 40);
            Console.WriteLine($"After the purchase {coffeeMachine.GetStatus()}\n");

            Console.WriteLine($"Refilling coffee");
            coffeeMachine.RefillCoffee(10);
            Console.WriteLine($"After the refill {coffeeMachine.GetStatus()}\n");

            Console.WriteLine($"Withdrawing money from the machine");
            coffeeMachine.WithdrawCache();
            Console.WriteLine($"After the withdrawal {coffeeMachine.GetStatus()}\n");
        }
    }
}
