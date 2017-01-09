using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using DrinkStore.WebApi.Models;

namespace DrinkStore.WebApi.Repository
{
    class ShoppingListRepository : IShoppingListRepository, IDrinkRepository
    {
        private const string CacheKey = "ShoppingLists";

        public IEnumerable<ShoppingList> GetShoppingLists()
        {
            return ShoppingLists.Values;
        }

        private Dictionary<long, ShoppingList> ShoppingLists
        {
            get
            {
                var cache = MemoryCache.Default;
                var shoppingListDict = cache[CacheKey] as Dictionary<long, ShoppingList>;

                if (shoppingListDict != null)
                    return shoppingListDict;

                shoppingListDict = new Dictionary<long, ShoppingList>();
                cache.Set(CacheKey, shoppingListDict, new CacheItemPolicy());

                return shoppingListDict;
            }
        }

        public ShoppingList GetShoppingList(long id)
        {
            ShoppingList shoppingList = null;
            ShoppingLists.TryGetValue(id, out shoppingList);

            return shoppingList;
        }

        public ShoppingList CreateShoppingList(ShoppingList shoppingList)
        {
            var id = ShoppingLists.Any()
                ? ShoppingLists.Keys.Max() + 1
                : 1;

            shoppingList.Id = id;
            ShoppingLists[id] = shoppingList;

            return shoppingList;
        }

        public void UpdateShoppingList(ShoppingList shoppingList)
        {
            if(!ShoppingLists.ContainsKey(shoppingList.Id))
                throw new ArgumentException("Shopping list not found in repository");

            ShoppingLists[shoppingList.Id] = shoppingList;
        }

        public Drink CreateDrink(Drink drink)
        {
            var drinks = ShoppingLists
                .Select(x => x.Value)
                .SelectMany(x => x.Drinks)
                .ToList();

            var id = drinks.Any()
                ? drinks.Max(x => x.Id) + 1
                : 1;

            drink.Id = id;

            return drink;
        }
    }
}