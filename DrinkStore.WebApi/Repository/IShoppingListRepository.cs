using DrinkStore.WebApi.Models;

namespace DrinkStore.WebApi.Repository
{
    public interface IShoppingListRepository
    {
        ShoppingList GetList(long id);

        void Create(ShoppingList shoppingList);
        void Update(ShoppingList shoppingList);
    }
}