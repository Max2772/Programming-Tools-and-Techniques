using System.Numerics;

namespace _453502_Bibikau_Lab2
{ 
    public class Tariff<T> where T : INumber<T>
    {
        public string Name { get; set; }
        public T Price { get; set; }

        public Tariff(string name, T price)
        {
            Name = name;
            Price = price;
        }

    }
}