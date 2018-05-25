using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Models
{
    [Serializable]
    public abstract class BaseModel
    {
        public BaseModel() { }

        protected BaseModel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
