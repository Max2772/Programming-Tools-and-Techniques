namespace _453502_Bibikau_Lab3
{ 
    public class Tariff
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Tariff(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

    }
}