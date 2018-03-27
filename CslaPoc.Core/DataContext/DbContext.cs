using CslaPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CslaPoc.Core.DataContext
{
    public class DbContext
    {
        public static List<Person> Persons = new List<Person>()
        {
             new Person()
            {
                Id = 1,
                Name = "Name 1",
            },
            new Person()
            {
                Id = 2,
                Name = "Name 2",
            },
            new Person()
            {
                Id = 3,
                Name = "Name 3",
            },
            new Person()
            {
                Id = 4,
                Name = "Name 4",
            },
            new Person()
            {
                Id = 5,
                Name = "Name 5",
            },
            new Person()
            {
                Id = 6,
                Name = "Name 6",
            },
            new Person()
            {
                Id = 7,
                Name = "Name 7",
            },
            new Person()
            {
                Id = 8,
                Name = "Name 8",
            },
            new Person()
            {
                Id = 9,
                Name = "Name 9",
            },
            new Person()
            {
                Id = 10,
                Name = "Name 10",
            }
        };


        public Person Create()
        {
            return new Person { Id = -1 };
        }

        public Person GetPerson(int id)
        {
            var entity = Persons.FirstOrDefault(_ => _.Id == id);
            if (entity == null)
                throw new Exception("Index not found");
            return entity;
        }

        public int InsertPerson(Person data)
        {
            var newId = 1;
            if (Persons.Count > 0)
                newId = Persons.Max(_ => _.Id) + 1;
            data.Id = newId;
            Persons.Add(data);
            return newId;
        }

        public void DeletePerson(int id)
        {
            var entity = GetPerson(id);
            Persons.Remove(entity);
        }
    }
}
