using System.Collections.Generic;

namespace ObjectsComposition.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> GetItems();

        T GetItemById(int itemId);

        int Create(T item);

        void Update(T item);

        void Delete(int itemId);
    }
}