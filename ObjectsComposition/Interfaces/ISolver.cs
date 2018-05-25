using System.Xml;
using ObjectsComposition.Models;

namespace ObjectsComposition.Interfaces
{
    public interface ISolver
    {
        BaseModel ObjectFromXml(XmlDocument xml);
        
        bool Validate(BaseModel model);

        bool CreateOrUpdate(BaseModel bm);
    }
}
