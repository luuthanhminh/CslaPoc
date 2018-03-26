using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CslaPoc.Core.Business.Person
{
    [Serializable]
    public class PersonInfo : ReadOnlyBase<PersonInfo>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static PersonInfo GetInfo(Models.Person entity)
        {
            return DataPortal.Fetch<PersonInfo>(entity);
        }

        protected void DataPortal_Fetch(Models.Person entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
