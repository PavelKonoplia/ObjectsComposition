using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Models
{
    public class HappenedException : BaseModel
    {
        public HappenedException() { }

        public HappenedException(string exceptionType, string exeptionMessage)
        {
            ExceptionType = exceptionType;
            ExceptionMessage = exeptionMessage;
        }

        public HappenedException(int id, string exceptionType, string exeptionMessage) : base(id)
        {
            ExceptionType = exceptionType;
            ExceptionMessage = exeptionMessage;
        }

        public string ExceptionType { get; }

        public string ExceptionMessage { get; }
    }
}
