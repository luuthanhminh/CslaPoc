using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CslaPoc.Core.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Person GetById(int id)
        {
            return PersonFake.FirstOrDefault(e => e.Id == id);
        }

        public static IEnumerable<Person> GetAll()
        {
            return PersonFake;
        }

        public static List<Person> PersonFake = new List<Person>()
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
    }
}
