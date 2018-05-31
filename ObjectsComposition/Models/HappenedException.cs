using ObjectsComposition.Common;

namespace ObjectsComposition.Models
{
    public class HappenedException : BaseModel
    {
        public HappenedException() { }

        public HappenedException(ObjectException exception)
        {
            Type = exception.GetType().ToString();
            Description = exception.Message;
        }

        public HappenedException(string exceptionType, string exeptionMessage)
        {
            Type = exceptionType;
            Description = exeptionMessage;
        }

        public HappenedException(int id, string exceptionType, string exeptionMessage) : base(id)
        {
            Type = exceptionType;
            Description = exeptionMessage;
        }

        public string Type { get; }

        public string Description { get; }
    }
}