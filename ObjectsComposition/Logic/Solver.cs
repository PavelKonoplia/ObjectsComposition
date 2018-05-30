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

        public Solver()
        {
            SetSerializer(typeof(BaseModel));
        }

        public void Solve(XmlDocument xml)
        {
            string inputModelxml = xml.DocumentElement.Name;

            Assembly ass = Assembly.GetExecutingAssembly();
            try
            {
                ObjectHandle objectHandle = Activator.CreateInstance(ass.FullName, $"{typeof(BaseModel).Namespace}.{inputModelxml}");
                BaseModel inputModel = (BaseModel)objectHandle.Unwrap();
                SetSerializer(inputModel.GetType());
            }
            catch
            {
                throw new IncorectFormatException();
            }

            using (XmlReader reader = new XmlNodeReader(xml))
            {
                BaseModel model = (BaseModel)_xmlSerializer.Deserialize(reader);
                CreateOrUpdate(model);
            }
        }

        public void CreateOrUpdate(BaseModel bm)
        {
            BaseModel item;
            Type repoType = typeof(CommandRunner<>);
            object runner = Activator.CreateInstance(repoType.MakeGenericType(bm.GetType()), connectionString);
            Type runnerType = runner.GetType();

            MethodInfo updateMethodInfo = runnerType.GetMethod("Update");
            MethodInfo createMethodInfo = runnerType.GetMethod("Create");
            MethodInfo getMethodInfo = runnerType.GetMethod("GetItemById");

            if (bm.Id != 0)
            {
                item = (BaseModel)getMethodInfo.Invoke(runner, new object[] { bm.Id });
                if (item != null)
                {
                    updateMethodInfo.Invoke(runner, new object[] { bm });
                }
                else
                {
                    throw new IncorrectObjectIdException();
                }
            }
            else
            {
                createMethodInfo.Invoke(runner, new object[] { bm });
            }
        }

        private void SetSerializer(Type type)
        {
            _xmlSerializer = new XmlSerializer(type);
        }
    }
}
