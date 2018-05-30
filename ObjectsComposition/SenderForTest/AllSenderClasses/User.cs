using System;
using System.Runtime.Serialization;

namespace PortSender
{
    [Serializable]
    public class User : BaseModel
    {
        public User() : base() { }

       // public User(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public User(string name, string login, string password, int age) : base()
        {
            Name = name;
            Login = login;
            Password = password;
            Age = age;
        }

        public User(int id, string name, string login, string password, int age) : base(id)
        {
            Name = name;
            Login = login;
            Password = password;
            Age = age;
        }

        public string Name { get; set; }

        public string Login { get; set; }

        [Encryption("PortSender.EncryptionService", 1111, 1111)]
        public string Password { get; set; }

        public int Age { get; set; }
    }
}
