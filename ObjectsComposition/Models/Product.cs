using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Models
{
    public class Product : BaseModel
    {
        public Product() { }

        public Product(int id, string name, int batch, string produced, int price) : base(id)
        {
            Name = name;
            Batch = batch;
            Produced = produced;
            Price = price;
        }

        public string Name { get; }

        public int Batch { get; }

        public string Produced { get; }

        public int Price { get; }
    }
}
