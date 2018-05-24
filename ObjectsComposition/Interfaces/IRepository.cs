using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> GetItems();

        T GetItemById(int itemId);

        void Create(T item);

        void Update(T item);

        void Delete(int itemId);
    }
}
