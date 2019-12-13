using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;

namespace RestWithAspNet.Services.Impl
{
    public class PersonServiceImpl : IPersonService
    {
        public MySqlContext _context;

        public PersonServiceImpl(MySqlContext ctx)
        {
            _context = ctx;
        }
        //volatile garante que toda vez que a aplicacao rodar o count vai ser reinicializado
        private volatile int count;

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return person;
        }

        public void Delete(long id)
        {
            var personDb = FindById(id);
            try
            {
                if (personDb != null)
                {
                    _context.Persons.Remove(personDb);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        //private Person MockPerson(int i)
        //{
        //    Person p = new Person()
        //    {
        //        Id = IncrementAndGet(),
        //        LastName = "Last"+i,
        //        FirstName = "First" + i,
        //        Address = "Rua x, campinas",
        //        Gender = "Male"
        //    };
        //    return p;
        //}

        //private long IncrementAndGet()
        //{
        //    return Interlocked.Increment(ref count);
        //}

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id == id);
        }

        public Person Update(Person person)
        {
            var personDb = FindById(person.Id);
            if (personDb == null)
            {
                return new Person();
            }
            try
            {
                _context.Entry(personDb).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return person;
        }
    }
}
