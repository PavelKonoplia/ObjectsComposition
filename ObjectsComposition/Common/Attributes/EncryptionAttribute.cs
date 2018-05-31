using System;
using System.Linq;
using System.Reflection;
using ObjectsComposition.Common.Interfaces;

namespace ObjectsComposition.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EncryptionAttribute : Attribute
    {
        public IEncryptionService EncryptionService { get; set; }

        public EncryptionAttribute(string serviceName, params int[] parameters)
        {
            this.SetService(serviceName, parameters);
        }

        private void SetService(string serviceName, params int[] parameters)
        {
            try
            {
                Type service = Assembly.GetExecutingAssembly().GetType(serviceName);
                ConstructorInfo[] constructorInfo = service.GetConstructors();
                this.EncryptionService = constructorInfo[0].Invoke(parameters.Cast<object>().ToArray()) as IEncryptionService;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}