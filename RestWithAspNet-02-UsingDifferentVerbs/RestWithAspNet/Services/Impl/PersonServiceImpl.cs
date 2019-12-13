using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAspNet.Model;

namespace RestWithAspNet.Services.Impl
{
    public class PersonServiceImpl : IPersonService
    {
        //volatile garante que toda vez que a aplicacao rodar o count vai ser reinicializado
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person p = MockPerson(i);
                persons.Add(p);
            }
            return persons;
        }

        private Person MockPerson(int i)
        {
            Person p = new Person()
            {
                Id = IncrementAndGet(),
                LastName = "Last"+i,
                FirstName = "First" + i,
                Address = "Rua x, campinas",
                Gender = "Male"
            };
            return p;
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public Person FindById(long id)
        {
            return MockPerson((int)id);
        }

        public Person Update(Person person)
        {
            person.FirstName = "Jorge";
            person.LastName = "Renato";
           return person;
        }
    }
}
