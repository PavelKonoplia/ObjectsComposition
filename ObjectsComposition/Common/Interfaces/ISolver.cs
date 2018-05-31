using System.Xml;
using ObjectsComposition.Models;

namespace ObjectsComposition.Interfaces
{
    public interface ISolver
    {
        void Solve(string xmlString);

        XmlDocument ConvertStringToXml(string xmlString);

        void CreateOrUpdate(BaseModel bm);
    }
}
