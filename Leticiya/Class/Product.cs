using System.Data;

namespace Leticiya.Class
{
    internal class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int Cout;

        public Product(int id, string name, double price, int cout)
        {
            Id = id;
            Name = name;
            Price = price;
            Cout = cout;
        }
    }
}
