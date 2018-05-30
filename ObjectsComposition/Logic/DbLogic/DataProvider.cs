using System.Configuration;
using ObjectsComposition.Interfaces;
using ObjectsComposition.Models;

namespace ObjectsComposition.Logic.DbLogic
{
    public static class ExceptionProvider
    {
        public static IRepository<HappenedException> HappenedExceptionRepository = new CommandRunner<HappenedException>(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

       // private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }

    public class DataProvider
    {
        public DataProvider(string connectionString)
        {
            UserRepository = new CommandRunner<User>(connectionString);
            ProductRepository = new CommandRunner<Product>(connectionString);
            ManufacterRepository = new CommandRunner<Manufacter>(connectionString);
            CountryRepository = new CommandRunner<Country>(connectionString);
        }
        
        public IRepository<User> UserRepository { get; }

        public IRepository<Product> ProductRepository { get; }

        public IRepository<Manufacter> ManufacterRepository { get; }

        public IRepository<Country> CountryRepository { get; }        
    }
}
