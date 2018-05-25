using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ObjectsComposition.Common;
using ObjectsComposition.Interfaces;
using ObjectsComposition.Logic.DbLogic;
using ObjectsComposition.Models;

namespace ObjectsComposition.Logic
{
    public class Solver : ISolver
    {
        // IRepository<User> UserRepository = new 
        private XmlSerializer _xmlSerializer;
        private DataProvider _dataProvider;
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public Solver()
        {
            _xmlSerializer = new XmlSerializer(typeof(User));
            _dataProvider = new DataProvider(connectionString);
        }

        public BaseModel ObjectFromXml(XmlDocument xml)
        {
            try
            {
                using (XmlReader reader = new XmlNodeReader(xml))
                {
                    BaseModel model = (BaseModel)_xmlSerializer.Deserialize(reader);
                    //
                    CreateOrUpdate(model);
                    if (Validate(model))
                    {
                        return model;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public bool Validate(BaseModel model)
        {
            return false;
        }

        public bool CreateOrUpdate(BaseModel bm)
        {
            Type type = bm.GetType();
            BaseModel item;
            switch (type.Name)
            {
                case "User":
                    if (bm.Id != 0)
                    {
                        item = _dataProvider.UserRepository.GetItemById(bm.Id);
                        if (item != null)
                        {
                            _dataProvider.UserRepository.Update((User)item);
                        }
                        else
                        {
                            throw new IncorrectObjectIdException();
                        }
                    }
                    else
                    {
                        int result = _dataProvider.UserRepository.Create((User)bm);
                    }

                    // item = _dataProvider.UserRepository
                    break;
                case "Product":
                    break;
                case "Manufacter":
                    break;
                case "Country":
                    break;
                case "HappendException":
                    break;
                default:
                    break;
            }
            return false;
        }

        private void CheckAndResolve(BaseModel bm)
        {
            if (true)
            {

            }
        }
    }
}
