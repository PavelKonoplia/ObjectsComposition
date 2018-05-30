using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PortSender
{
    [Serializable]
    public abstract class BaseModel : IXmlSerializable
    {
        public BaseModel() { }

        protected BaseModel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
       
        public void WriteXml(XmlWriter writer)
        {
            Type type = GetType();
            IEncryptionService encryptionService;
            EncryptionAttribute encryptionAttribute;
            PropertyInfo[] propsInfos = type.GetProperties();
            FieldInfo[] fieldsInfos = type.GetFields();

            foreach (PropertyInfo prop in propsInfos)
            {
                if (Attribute.IsDefined(prop, typeof(EncryptionAttribute)))
                {
                    encryptionAttribute = Attribute.GetCustomAttribute(prop, typeof(EncryptionAttribute)) as EncryptionAttribute;
                    encryptionService = encryptionAttribute.EncryptionService;
                    var temp = Convert.ToBase64String(encryptionService.Encrypt(prop.GetValue(this)));
                    writer.WriteElementString(prop.Name, Convert.ToBase64String(encryptionService.Encrypt(prop.GetValue(this))));
                }
                else
                {

                    var temp = prop.GetValue(this).ToString();
                    writer.WriteElementString(prop.Name, prop.GetValue(this).ToString());
                }
            }

            foreach (FieldInfo field in fieldsInfos)
            {
                if (Attribute.IsDefined(field, typeof(EncryptionAttribute)))
                {
                    encryptionAttribute = Attribute.GetCustomAttribute(field, typeof(EncryptionAttribute)) as EncryptionAttribute;
                    encryptionService = encryptionAttribute.EncryptionService;
                    writer.WriteValue(encryptionService.Encrypt(field.GetValue(this)));
                }
                else
                {
                    writer.WriteValue(field.GetValue(this));
                }
            }
        }

        public void ReadXml(XmlReader reader) { }


        public XmlSchema GetSchema()
        {
            return (null);
        }

        protected void GetServiceFromAttribute(PropertyInfo property)
        {
            var type = GetType();
        }
    }
}
