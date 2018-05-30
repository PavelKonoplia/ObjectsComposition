using System.Configuration;
using ObjectsComposition.Interfaces;
using ObjectsComposition.Models;

namespace ObjectsComposition.Logic.DbLogic
{
    public static class ExceptionProvider
    {
        public static IRepository<HappenedException> HappenedExceptionRepository = new CommandRunner<HappenedException>(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
    }
}
