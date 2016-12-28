namespace DrinkStore.WebApi.Models
{
    public class Drink
    {
        public Drink(string name, int quantity = 1)
        {
            Name = name;
            Quantity = quantity;
        }

        protected bool Equals(Drink other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Drink) obj);
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        public string Name { get; }
        public int Quantity { get; set; }
    }
}