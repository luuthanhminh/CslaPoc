using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Client.Core.Models;

namespace Client.Core.Business
{
    [Serializable]
    public class PersonList : ReadOnlyListBase<PersonList, PersonInfo>
    {
        public PersonList()
        {

        }

        public static Task<PersonList> GetPersonList()
        {
            return DataPortal.FetchAsync<PersonList>();
        }

        private async Task DataPortal_Fetch()
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            var items = await $"{AppConstants.ENDPOINT}api/persons".GetJsonAsync<List<Person>>();

            foreach (var item in items)
                Add(DataPortal.FetchChild<PersonInfo>(item));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }
    }
}
