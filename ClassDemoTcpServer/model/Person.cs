using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoTcpServer.model
{
    public class Person
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }

        public Person():this(0, "dummy", "", "")
        {
        }

        public Person(int id, string name, string address, string mobile)
        {
            Id = id;
            Name = name;
            Address = address;
            Mobile = mobile;
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Name)}={Name}, {nameof(Address)}={Address}, {nameof(Mobile)}={Mobile}}}";
        }
    }
}
