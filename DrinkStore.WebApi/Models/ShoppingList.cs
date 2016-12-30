using System.Collections.Generic;
using System.Diagnostics;

namespace DrinkStore.WebApi.Models
{
    public class ShoppingList
    {
        private readonly Dictionary<int, Drink> _drinks = new Dictionary<int, Drink>();

        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Drink> Drinks => _drinks.Values;

        public bool HasDrink(Drink drink)
        {
            return _drinks.ContainsKey(drink.Id);
        }

        public void UpdateDrink(Drink drink)
        {
            _drinks[drink.Id].Quantity = drink.Quantity;
        }

        public void AddDrink(Drink drink)
        {
            Debug.Assert(drink.Id != 0, "Expecting a non-zero Drink ID");
            _drinks.Add(drink.Id, drink);
        }

        public bool RemoveDrink(int id)
        {
            if (!_drinks.ContainsKey(id))
                return false;

            _drinks.Remove(id);
            return true;
        }

        protected bool Equals(ShoppingList other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ShoppingList)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


    }
}