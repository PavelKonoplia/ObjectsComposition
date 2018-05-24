namespace ObjectsComposition.Interfaces
{
    public interface ISolver
    {
        object ObjectFromXml(string xml);

        bool Validate(object o);

        string CreateOrUpdate(object o);
    }
}
