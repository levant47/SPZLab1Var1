using System.Collections.Generic;

namespace SPZLab1Var1
{
    public interface ICoffeeMachine
    {
        void BuyDrink(DrinkType drinkType, decimal payment);

        void BuyMultipleDrinks(IEnumerable<DrinkType> drinkTypes, decimal payment);

        void RefillCoffee(int amount);

        void RefillMilk(int amount);

        void RefillWater(int amount);

        void RefillSugar(int amount);

        void WithdrawCache();
    }
}
