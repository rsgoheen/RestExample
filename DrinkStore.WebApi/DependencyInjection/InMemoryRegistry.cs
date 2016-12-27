using DrinkStore.WebApi.Repository;
using StructureMap;

namespace DrinkStore.WebApi.DependencyInjection
{
    public class InMemoryRegistry : Registry
    {
        public InMemoryRegistry()
        {
            For<IShoppingListRepository>().Use<ShoppingListRepository>();
        }
    }
}