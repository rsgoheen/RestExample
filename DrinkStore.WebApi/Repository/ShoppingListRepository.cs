using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using DrinkStore.WebApi.Models;

namespace DrinkStore.WebApi.Repository
{
    class ShoppingListRepository : IShoppingListRepository
    {
        private const string CacheKey = "ShoppingLists";

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

        public ShoppingList GetList(long id)
        {
            ShoppingList shoppingList = null;
            ShoppingLists.TryGetValue(id, out shoppingList);

            return shoppingList;
        }

        public ShoppingList Create(ShoppingList shoppingList)
        {
            var id = ShoppingLists.Any()
                ? ShoppingLists.Keys.Max() + 1
                : 1;

            shoppingList.Id = id;
            ShoppingLists[id] = shoppingList;

            return shoppingList;
        }

        public void Update(ShoppingList shoppingList)
        {
            throw new System.NotImplementedException();
        }
    }
}