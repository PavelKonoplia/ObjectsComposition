using ObjectsComposition.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        public int Age { get; set; }
    }
}
