using ObjectsComposition.Interfaces;
using ObjectsComposition.Models;

namespace ObjectsComposition.Logic.DbLogic
{
    public class DataProvider
    {
        public DataProvider(string connectionString)
        {
            UserRepository = new CommandRunner<User>(connectionString);
            ProductRepository = new CommandRunner<Product>(connectionString);
            ManufacterRepository = new CommandRunner<Manufacter>(connectionString);
            CountryRepository = new CommandRunner<Country>(connectionString);
            HappenedExceptionRepository = new CommandRunner<HappenedException>(connectionString);
        }

        public IRepository<User> UserRepository { get; }

        public IRepository<Product> ProductRepository { get; }

        public IRepository<Manufacter> ManufacterRepository { get; }

        public IRepository<Country> CountryRepository { get; }

        public IRepository<HappenedException> HappenedExceptionRepository { get; }
    }
}
