using System.Xml;
using ObjectsComposition.Models;

namespace ObjectsComposition.Interfaces
{
    public interface ISolver
    {
        void Solve(XmlDocument xml);
        
        void CreateOrUpdate(BaseModel bm);
    }
}
