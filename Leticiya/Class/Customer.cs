namespace Leticiya.Class
{
    internal class Customer
    {
        public int Id;
        public string Surname;
        public string Name;
        public string Patronymic;
        public string Organization;
        public string Telephone;
        public string Address;

        public Customer(int id, string surname, string name, string patronymic, string organization, string telephone, string address)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Organization = organization;
            Telephone = telephone;
            Address = address;
        }
    }
}
