using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Models
{
    public class Country : BaseModel
    {
        public Country() { }

        public Country(int id, string name, string language, int population, string president) : base(id)
        {
            Name = name;
            Language = language;
            Population = population;
            President = president;
        }

        public string Name { get; }

        public string Language { get; }

        public int Population { get; }

        public string President { get; }
    }
}
