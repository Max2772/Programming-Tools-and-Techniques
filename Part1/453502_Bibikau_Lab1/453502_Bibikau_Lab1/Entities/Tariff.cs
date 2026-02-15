using System.Numerics;

namespace _453502_Bibikau_Lab1
{ 
    public class Tariff<T> where T : IMultiplyOperators<T, T, T>
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