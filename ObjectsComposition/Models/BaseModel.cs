using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ObjectsComposition.Common;
using ObjectsComposition.Common.Attributes;
using ObjectsComposition.Common.Interfaces;

namespace ObjectsComposition.Models
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
                    writer.WriteElementString(prop.Name, Convert.ToBase64String(encryptionService.Encrypt(prop.GetValue(this))));
                }
                else
                {
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

        public void ReadXml(XmlReader reader)
        {
            Type type = GetType();
            IEncryptionService encryptionService;
            EncryptionAttribute encryptionAttribute;
            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    PropertyInfo property = type.GetProperty(reader.Name);
                    FieldInfo field = type.GetField(reader.Name);
                    if (reader.Read())
                    {
                        string val = reader.Value;
                        if (property != null)
                        {
                            if (Attribute.IsDefined(property, typeof(EncryptionAttribute)))
                            {
                                encryptionAttribute = Attribute.GetCustomAttribute(property, typeof(EncryptionAttribute)) as EncryptionAttribute;
                                encryptionService = encryptionAttribute.EncryptionService;

                                if (Regex.IsMatch(val, @"^[a-zA-Z0-9]+$"))
                                {
                                    throw new NoEncryptionException();
                                }
                                else
                                {
                                    try
                                    {
                                        byte[] bytes = Convert.FromBase64String(val);
                                        property.SetValue(this, Convert.ChangeType(encryptionService.Decrypt(bytes), property.PropertyType));
                                    }
                                    catch
                                    {
                                        throw new IncorrectEncryptionException();
                                    }
                                }
                            }
                            else
                            {
                                property.SetValue(this, Convert.ChangeType(val, property.PropertyType));
                            }
                        }
                        else
                        {
                            if (field != null)
                            {
                                if (Attribute.IsDefined(field, typeof(EncryptionAttribute)))
                                {
                                    encryptionAttribute = Attribute.GetCustomAttribute(property, typeof(EncryptionAttribute)) as EncryptionAttribute;
                                    encryptionService = encryptionAttribute.EncryptionService;

                                    if (Regex.IsMatch(val, @"^[a-zA-Z0-9]+$"))
                                    {
                                        throw new NoEncryptionException();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            byte[] bytes = Convert.FromBase64String(val);
                                            field.SetValue(this, Convert.ChangeType(encryptionService.Decrypt(bytes), field.FieldType));
                                        }
                                        catch
                                        {
                                            throw new IncorrectEncryptionException();
                                        }
                                    }
                                }
                                else
                                {
                                    field.SetValue(this, Convert.ChangeType(val, field.FieldType));
                                }
                            }
                            else
                            {
                                throw new IncorectFormatException();
                            }
                        }
                    }
                }
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        protected void GetServiceFromAttribute(PropertyInfo property)
        {
            var type = GetType();
        }
    }
}
