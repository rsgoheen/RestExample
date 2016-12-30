using DrinkStore.WebApi.Models;

namespace DrinkStore.WebApi.Repository
{
    public interface IShoppingListRepository
    {
        ShoppingList GetShoppingList(long id);
        ShoppingList CreateShoppingList(ShoppingList shoppingList);
        void UpdateShoppingList(ShoppingList shoppingList);
    }
}