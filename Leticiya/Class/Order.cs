using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Leticiya.Class
{
    class Order
    {
        public Customer customer;
        public string Address;
        public string Status;
        public string DataOrder;
        public string DataDelevery;
        public double DeleveryPrice;
        public List<Product> products = new List<Product>();
        public string Comment;

        public void AddCustomer(int id, string surname, string name, string patronymic, string organization, string telephone, string address)
        {
            customer = new Customer(id, surname, name, patronymic, organization, telephone, address);
        }

        public void AddProduct(int id, string name, double price, int cout)
        {
            products.Add(new Product(id, name, price ,cout));
        }

        public double OrderPrice()
        {
            double sum = 0;
            for (int i = 0; i < products.Count; i++)
                sum += products[i].Cout * products[i].Price;
            return sum + DeleveryPrice;
        }
    }
}
