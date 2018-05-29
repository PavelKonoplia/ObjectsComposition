using System;
using System.Reflection;
using System.Runtime.Serialization;
using ObjectsComposition.Common;
using ObjectsComposition.Common.Attributes;
using ObjectsComposition.Common.Interfaces;

namespace ObjectsComposition.Models
{
    [Serializable]
    public abstract class BaseModel : ISerializable
    {
        public BaseModel() { }

        protected BaseModel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
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
                    info.AddValue(prop.Name, encryptionService.Encrypt(prop.GetMethod));
                }
                else
                {
                    info.AddValue(prop.Name, prop.GetMethod);
                }
            }

            foreach (FieldInfo field in fieldsInfos)
            {
                if (Attribute.IsDefined(field, typeof(EncryptionAttribute)))
                {
                    encryptionAttribute = Attribute.GetCustomAttribute(field, typeof(EncryptionAttribute)) as EncryptionAttribute;
                    encryptionService = encryptionAttribute.EncryptionService;
                    info.AddValue(field.Name, encryptionService.Encrypt(field.GetValue(field.GetType())));
                }
                else
                {
                    info.AddValue(field.Name, field.GetValue(field.GetType()));
                }
            }
        }

        public BaseModel(SerializationInfo info, StreamingContext context)
        {
            Type type = GetType();
            IEncryptionService encryptionService;
            EncryptionAttribute encryptionAttribute;
            PropertyInfo[] propsInfos = type.GetProperties();
            FieldInfo[] fieldsInfos = type.GetFields();

            try
            {
                foreach (PropertyInfo prop in propsInfos)
                {
                    if (Attribute.IsDefined(prop, typeof(EncryptionAttribute)))
                    {
                        encryptionAttribute = Attribute.GetCustomAttribute(prop, typeof(EncryptionAttribute)) as EncryptionAttribute;
                        encryptionService = encryptionAttribute.EncryptionService;

                        try
                        {
                            prop.SetValue(this, info.GetValue(prop.Name, prop.PropertyType));
                            throw new NoEncryptionException();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                prop.SetValue(this, encryptionService.Decrypt((byte[])info.GetValue(prop.Name, typeof(byte[]))));
                            }
                            catch (Exception)
                            {
                                throw new IncorrectEncryptionException();
                            }
                        }                       
                    }
                    else
                    {
                        prop.SetValue(this, info.GetValue(prop.Name, prop.PropertyType));
                    }
                }

                foreach (FieldInfo field in fieldsInfos)
                {
                    if (Attribute.IsDefined(field, typeof(EncryptionAttribute)))
                    {
                        encryptionAttribute = Attribute.GetCustomAttribute(field, typeof(EncryptionAttribute)) as EncryptionAttribute;
                        encryptionService = encryptionAttribute.EncryptionService;

                        try
                        {
                            field.SetValue(this, info.GetValue(field.Name, field.GetType()));
                            throw new NoEncryptionException();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                field.SetValue(this, encryptionService.Decrypt((byte[])info.GetValue(field.Name, typeof(byte[]))));
                            }
                            catch (Exception)
                            {
                                throw new IncorrectEncryptionException();
                            }
                        }
                    }
                    else
                    {
                        field.SetValue(this, info.GetValue(field.Name, field.GetType()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }                        
        }

        protected void GetServiceFromAttribute(PropertyInfo property)
        {
            var type = GetType();
        }
    }
}
