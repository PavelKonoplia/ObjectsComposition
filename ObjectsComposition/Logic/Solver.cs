using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
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
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private XmlSerializer _xmlSerializer;
        private DataProvider _dataProvider;

        public Solver()
        {
            SetSerializer(typeof(BaseModel));
            _dataProvider = new DataProvider(connectionString);
        }

        public BaseModel ObjectFromXml(XmlDocument xml)
        {
            try
            {
                string inputModelxml = xml.DocumentElement.Name;
               
                Assembly ass = Assembly.GetExecutingAssembly();
                ObjectHandle objectHandle = Activator.CreateInstance(ass.FullName, $"{typeof(BaseModel).Namespace}.{inputModelxml}");
                BaseModel inputModel = (BaseModel)objectHandle.Unwrap();
                SetSerializer(inputModel.GetType());

                using (XmlReader reader = new XmlNodeReader(xml))
                {
                    BaseModel model = (BaseModel)_xmlSerializer.Deserialize(reader);
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

        public bool CreateOrUpdate(BaseModel bm)
        {
            BaseModel item;
            try
            {
                if (bm is User)
                {
                    if (bm.Id != 0)
                    {
                        item = _dataProvider.UserRepository.GetItemById(bm.Id);
                        if (item != null)
                        {
                            _dataProvider.UserRepository.Update(bm as User);
                            return true;
                        }
                    }
                    else
                    {
                        _dataProvider.UserRepository.Create(bm as User);
                        return true;
                    }
                }

                if (bm is Manufacter)
                {
                    if (bm.Id != 0)
                    {
                        item = _dataProvider.ManufacterRepository.GetItemById(bm.Id);
                        if (item != null)
                        {
                            _dataProvider.ManufacterRepository.Update(bm as Manufacter);
                            return true;
                        }
                    }
                    else
                    {
                        _dataProvider.ManufacterRepository.Create(bm as Manufacter);
                        return true;
                    }
                }

                if (bm is Product)
                {
                    if (bm.Id != 0)
                    {
                        item = _dataProvider.ProductRepository.GetItemById(bm.Id);
                        if (item != null)
                        {
                            _dataProvider.ProductRepository.Update(bm as Product);
                            return true;
                        }
                    }
                    else
                    {
                        _dataProvider.ProductRepository.Create(bm as Product);
                        return true;
                    }
                }

                if (bm is Country)
                {
                    if (bm.Id != 0)
                    {
                        item = _dataProvider.CountryRepository.GetItemById(bm.Id);
                        if (item != null)
                        {
                            _dataProvider.CountryRepository.Update(bm as Country);
                            return true;
                        }
                    }
                    else
                    {
                        _dataProvider.CountryRepository.Create(bm as Country);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw new IncorrectObjectIdException();
            }

            return false;
        }

        public bool Validate(BaseModel model)
        {
            return false;
        }

        private void CheckAndResolve(BaseModel bm)
        {
            if (true)
            {
                // todo
            }
        }

        private void SetSerializer(Type type)
        {
            _xmlSerializer = new XmlSerializer(type);
        }
    }
}
