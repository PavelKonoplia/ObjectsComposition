using System;
using ObjectsComposition.Common.Attributes;

namespace ObjectsComposition.Models
{
    [Serializable]
    public class User : BaseModel
    {
        public User() : base() { }

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

        [Encryption("ObjectsComposition.Common.Services.EncryptionService", 1111, 1111)]
        public string Name { get; set; }

        public string Login { get; set; }

        [Encryption("ObjectsComposition.Common.Services.EncryptionService", 1111, 1111)]
        public string Password { get; set; }

        [Encryption("ObjectsComposition.Common.Services.EncryptionService", 1111, 1111)]
        public int Age { get; set; }
    }
}