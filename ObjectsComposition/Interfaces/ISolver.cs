using System.Xml;
using ObjectsComposition.Models;

namespace ObjectsComposition.Interfaces
{
    public interface ISolver
    {
        BaseModel ObjectFromXml(XmlDocument xml);

        string CreateOrUpdate(BaseModel bm);

        bool Validate(XmlDocument xml);
    }
}
