using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CslaPoc.Core.Business.Person
{
    [Serializable]
    public class PersonList : ReadOnlyListBase<PersonList, PersonInfo>
    {
        public PersonList()
        {

        }

        public static PersonList GetList()
        {
            return DataPortal.Fetch<PersonList>();
        }

        private void DataPortal_Fetch()
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            foreach (var item in Models.Person.GetAll())
            {
                this.Add(PersonInfo.GetInfo(item));
            }

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
    }
}
