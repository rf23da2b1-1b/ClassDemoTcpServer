using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoTcpServer.model
{
    public class PersonRepo
    {
        private readonly List<Person> _personer;
        private int _nextId = 1;

        public PersonRepo()
        {
            _personer = new List<Person>();
        }

        public List<Person> GetAll()
        {
            return new List<Person>(_personer);
        }

        public Person Get(int id)
        {
            Person? p = _personer.Find(p => p.Id == id); 
            if (p is null)
            {
                throw new KeyNotFoundException();
            }

            return p;
        }

        public Person Add(Person person)
        {
            person.Id = _nextId++;
            _personer.Add(person);
            return person;
        }

        public Person Delete(int id)
        {
            Person p = Get(id);
            _personer.Remove(p);
            return p;
        }

        public Person Update(int id, Person person)
        {
            Person p = Get(id);
            int index = _personer.IndexOf(person);
            _personer[index] = person;

            return person;
        }

    }
}
