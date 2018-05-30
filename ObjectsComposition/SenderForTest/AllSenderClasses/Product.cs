using System;
using System.Runtime.Serialization;

namespace PortSender
{
    [Serializable]
    public class Product : BaseModel
    {
        public Product() { }

       // public Product(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public Product(string name, int batch, string produced, int price)
        {
            Name = name;
            Batch = batch;
            Produced = produced;
            Price = price;
        }

        public Product(int id, string name, int batch, string produced, int price) : base(id)
        {
            Name = name;
            Batch = batch;
            Produced = produced;
            Price = price;
        }

        [Encryption("PortSender.EncryptionService", 1111, 1111)]
        public string Name { get; set; }

        public int Batch { get; set; }

        public string Produced { get; set; }

        public int Price { get; set; }
    }
}
