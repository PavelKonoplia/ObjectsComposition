using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Common
{
    public class ObjectException : Exception
    {
        protected ObjectException(string message) : base(message) { }
    }

    public class IncorectFormatException : ObjectException
    {
        public IncorectFormatException() : base("Incorect format of input request") { }
    }

    public class NoEncryptionException : ObjectException
    {
        public NoEncryptionException() : base("No encryption in the model on attributed fields") { }
    }

    public class IncorrectEncryptionException : ObjectException
    {
        public IncorrectEncryptionException() : base("Incorrect enscryption algorithm") { }
    }

    public class IncorrectObjectIdException : ObjectException
    {
        public IncorrectObjectIdException() : base("Object has incorrect identification") { }
    }
}
