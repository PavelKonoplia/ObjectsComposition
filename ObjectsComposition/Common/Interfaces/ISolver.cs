using System.Xml;
using ObjectsComposition.Models;

namespace ObjectsComposition.Interfaces
{
    public interface ISolver
    {
        BaseModel ObjectFromXml(XmlDocument xml);
        
        void CreateOrUpdate(BaseModel bm);
    }
}
