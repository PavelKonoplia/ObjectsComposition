using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Models
{
    [Serializable]
    public class Country : BaseModel
    {
        public Country() { }

        public Country(string name, string language, int population, string president)
        {
            Name = name;
            Language = language;
            Population = population;
            President = president;
        }

        public Country(int id, string name, string language, int population, string president) : base(id)
        {
            Name = name;
            Language = language;
            Population = population;
            President = president;
        }

        public string Name { get; set; }

        public string Language { get; set; }

        public int Population { get; set; }

        public string President { get; set; }
    }
}
