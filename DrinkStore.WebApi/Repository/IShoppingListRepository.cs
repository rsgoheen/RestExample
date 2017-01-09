using System.Collections.Generic;
using DrinkStore.WebApi.Models;

namespace DrinkStore.WebApi.Repository
{
    public interface IShoppingListRepository
    {
        IEnumerable<ShoppingList> GetShoppingLists();

        ShoppingList GetShoppingList(long id);
        ShoppingList CreateShoppingList(ShoppingList shoppingList);
        void UpdateShoppingList(ShoppingList shoppingList);
    }
}