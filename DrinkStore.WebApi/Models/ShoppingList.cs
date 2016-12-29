﻿using System.Collections.Generic;

namespace DrinkStore.WebApi.Models
{
    public class ShoppingList
    {
        private readonly Dictionary<string, Drink> _drinks = new Dictionary<string, Drink>();

        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Drink> Drinks => _drinks.Values;

        public bool HasDrink(Drink drink)
        {
            return _drinks.ContainsKey(drink.Name);
        }

        public void UpdateDrink(Drink drink)
        {
            _drinks[drink.Name].Quantity = drink.Quantity;
        }

        public void AddDrink(Drink drink)
        {
            _drinks.Add(drink.Name, drink);
        }

        public bool RemoveDrink(string name)
        {
            if (!_drinks.ContainsKey(name))
                return false;

            _drinks.Remove(name);
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