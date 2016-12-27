using System;
using System.Collections.Generic;

namespace DrinkStore.WebApi.Models
{
    public class ShoppingList
    {
        public long Id { get; set; }
        public IEnumerable<Drink> Drinks { get; set; }

        public void AddDrink(Drink drink)
        {
            throw new NotImplementedException();
        }

        public void RemoveDrink(Drink drink)
        {
            throw new NotImplementedException();
        }
    }
}