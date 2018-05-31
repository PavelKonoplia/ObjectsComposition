using System;

namespace ObjectsComposition.Models
{
    [Serializable]
    public class Manufacter : BaseModel
    {
        public Manufacter() { }

        public Manufacter(string name, string country, string city, string address)
        {
            Name = name;
            Country = country;
            City = city;
            Address = address;
        }

        public Manufacter(int id, string name, string country, string city, string address) : base(id)
        {
            Name = name;
            Country = country;
            City = city;
            Address = address;
        }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}