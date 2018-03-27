using Client.Core.Models;
using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.Business
{
    [Serializable]
    public class PersonInfo : ReadOnlyBase<PersonInfo>
    {
        public static PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }
        public PersonInfo()
        {
        }

        private void Child_Fetch(Person item)
        {
            Id = item.Id;
            Name = item.Name;
        }
    }
}
