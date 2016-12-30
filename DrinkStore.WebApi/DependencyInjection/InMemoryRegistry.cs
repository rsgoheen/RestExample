using DrinkStore.WebApi.Repository;
using StructureMap;

namespace DrinkStore.WebApi.DependencyInjection
{
    public class InMemoryRegistry : Registry
    {
        public InMemoryRegistry()
        {
            ForConcreteType<ShoppingListRepository>().Configure.Singleton();
            For<IShoppingListRepository>().Use<ShoppingListRepository>();
            For<IDrinkRepository>().Use<ShoppingListRepository>();
        }
    }
}