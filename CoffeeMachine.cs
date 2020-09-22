using System;
using System.Collections.Generic;
using System.Linq;

namespace SPZLab1Var1
{
    public class CoffeeMachine : ICoffeeMachine
    {
        public string Name { get; set; }
        public int CoffeeAmount { get; set; }
        public int MilkAmount { get; set; }
        public int WaterAmount { get; set; }
        public int SugarAmount { get; set; }
        public decimal Cache { get; set; }
        private readonly int _maxCoffeeAmount;
        private readonly int _maxMilkAmount;
        private readonly int _maxWaterAmount;
        private readonly int _maxSugarAmount;
        private readonly Dictionary<DrinkType, DrinkInfo> _drinkInfos = new List<DrinkInfo>
        {
            new DrinkInfo { DrinkType = DrinkType.Water, Price = 10, CoffeeAmount = 0, MilkAmount = 0, WaterAmount = 10, SugarAmount = 0 },
            new DrinkInfo { DrinkType = DrinkType.Milk, Price = 15, CoffeeAmount = 0, MilkAmount = 10, WaterAmount = 0, SugarAmount = 0 },
            new DrinkInfo { DrinkType = DrinkType.Americano, Price = 20, CoffeeAmount = 5, MilkAmount = 0, WaterAmount = 5, SugarAmount = 0 },
            new DrinkInfo { DrinkType = DrinkType.Cappuccino, Price = 25, CoffeeAmount = 6, MilkAmount = 2, WaterAmount = 0, SugarAmount = 2 },
        }.ToDictionary(drinkInfo => drinkInfo.DrinkType);

        public CoffeeMachine
        (
            string name,
            int coffeeAmount,
            int milkAmount,
            int waterAmount,
            int sugarAmount,
            int maxCoffeeAmount,
            int maxMilkAmount,
            int maxWaterAmount,
            int maxSugarAmount,
            decimal cache
        )
        {
            if (string.IsNullOrEmpty(name) || name.Contains(' '))
            {
                throw new ArgumentException("Name of a coffee machine cannot be empty or contain spaces");
            }
            if (cache < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cache), "Amount of cache in a coffee machine must not be less than zero");
            }

            Name = name;
            CoffeeAmount = coffeeAmount;
            MilkAmount = milkAmount;
            WaterAmount = waterAmount;
            SugarAmount = sugarAmount;
            _maxCoffeeAmount = maxCoffeeAmount;
            _maxMilkAmount = maxMilkAmount;
            _maxWaterAmount = maxWaterAmount;
            _maxSugarAmount = maxSugarAmount;
        }

        public string GetStatus() => $"amount of coffee: {CoffeeAmount}, milk: {MilkAmount}, water: {WaterAmount}, sugar: {SugarAmount}; Cache: {Cache}";

        public void RefillCoffee(int amount)
        {
            if (CoffeeAmount + amount > _maxCoffeeAmount)
            {
                throw new InvalidOperationException("Attempted to refill coffee past the maximum allowed value");
            }
            CoffeeAmount += amount;
        }

        public void RefillMilk(int amount)
        {
            if (MilkAmount + amount > _maxMilkAmount)
            {
                throw new InvalidOperationException("Attempted to refill milk past the maximum allowed value");
            }
            MilkAmount += amount;
        }

        public void RefillWater(int amount)
        {
            if (WaterAmount + amount > _maxWaterAmount)
            {
                throw new InvalidOperationException("Attempted to refill water past the maximum allowed value");
            }
            WaterAmount += amount;
        }

        public void RefillSugar(int amount)
        {
            if (SugarAmount + amount > _maxSugarAmount)
            {
                throw new InvalidOperationException("Attempted to refill sugar past the maximum allowed value");
            }
            SugarAmount += amount;
        }

        public void WithdrawCache() => Cache = 0;

        public void BuyDrink(DrinkType drinkType, decimal payment) => PerformPurchase(_drinkInfos[drinkType], payment);

        public void BuyMultipleDrinks(IEnumerable<DrinkType> drinkTypes, decimal payment)
        {
            var drinkTypesSet = drinkTypes.ToHashSet();
            var targetDrinkInfos = _drinkInfos.Values.Where(drinkInfo => drinkTypesSet.Contains(drinkInfo.DrinkType)).ToList();
            PerformPurchase
            (
                new DrinkInfo
                {
                    Price = targetDrinkInfos.Sum(drinkInfo => drinkInfo.Price),
                    CoffeeAmount = targetDrinkInfos.Sum(drinkInfo => drinkInfo.CoffeeAmount),
                    MilkAmount = targetDrinkInfos.Sum(drinkInfo => drinkInfo.MilkAmount),
                    WaterAmount = targetDrinkInfos.Sum(drinkInfo => drinkInfo.WaterAmount),
                    SugarAmount = targetDrinkInfos.Sum(drinkInfo => drinkInfo.SugarAmount),
                },
                payment
            );
        }

        private void PerformPurchase(DrinkInfo drinkInfo, decimal payment)
        {
            if (WaterAmount - drinkInfo.WaterAmount < 0)
            {
                throw new InvalidOperationException("Not enough water");
            }
            if (MilkAmount - drinkInfo.MilkAmount < 0)
            {
                throw new InvalidOperationException("Not enough milk");
            }
            if (CoffeeAmount - drinkInfo.CoffeeAmount < 0)
            {
                throw new InvalidOperationException("Not enough coffee");
            }
            if (SugarAmount - drinkInfo.SugarAmount < 0)
            {
                throw new InvalidOperationException("Not enough sugar");
            }
            if (payment < drinkInfo.Price)
            {
                throw new InvalidOperationException("The payment you've entered is not enough for this purchase");
            }

            WaterAmount -= drinkInfo.WaterAmount;
            MilkAmount -= drinkInfo.MilkAmount;
            CoffeeAmount -= drinkInfo.CoffeeAmount;
            SugarAmount -= drinkInfo.SugarAmount;
            Cache += drinkInfo.Price;

            if (payment > drinkInfo.Price)
            {
                Console.WriteLine($"Here's the change: ${(payment - drinkInfo.Price).ToString("F")}");
            }
        }
    }
}
