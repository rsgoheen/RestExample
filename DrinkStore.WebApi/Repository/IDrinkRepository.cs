using DrinkStore.WebApi.Models;

namespace DrinkStore.WebApi.Repository
{
    public interface IDrinkRepository
    {
        Drink CreateDrink(Drink drink);
    }
}