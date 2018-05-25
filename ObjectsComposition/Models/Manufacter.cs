using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Models
{
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

        public string Name { get; }

        public string Country { get; }

        public string City { get; }

        public string Address { get; }
    }
}
