using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ObjectsComposition.Models
{
    [Serializable]
    [XmlRoot("User")]
    public class User : BaseModel
    {
        public User() { }

        public User(int id, string name, string login, string password, int age) : base(id)
        {
            Name = name;
            Login = login;
            Password = password;
            Age = age;
        }

        public string Name { get; set; }
        
        public int Age { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
