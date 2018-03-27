using Csla;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms
{
    public class AppConstants
    {
        public const string ENDPOINT = "http://local.agiledevops.io:6655/";
    }

    #region Model

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }



    #endregion

    #region CSLA Model
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

    [Serializable]
    public class PersonEdit : BusinessBase<PersonEdit>
    {
        #region Properties
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name, "Name");
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }
        #endregion

        #region Business Rules
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
        }
        #endregion

        #region Factory Methods
        public static PersonEdit CreatePerson()
        {
            return DataPortal.Create<PersonEdit>();
        }

        public static async Task DeletePersonEdit(int Id)
        {
           await DataPortal.DeleteAsync<PersonEdit>(Id);
        }
        #endregion

        #region Data Access

        private async Task DataPortal_Delete(int id)
        {
            using (BypassPropertyChecks)
            {
                try
                {
                    var result = await $"{AppConstants.ENDPOINT}api/persons/{id}".DeleteAsync().ReceiveString();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
            }
        }
        #endregion

        #region Overrides

        protected override void DataPortal_Create()
        {
             var person = new Person { Id = -1 };
            using (BypassPropertyChecks)
            {
                Id = person.Id;
                Name = person.Name;
            }
            BusinessRules.CheckRules();
        }


        private async Task DataPortal_Insert()
        {
            using (BypassPropertyChecks)
            {
                var person = new 
                {
                    Name = this.Name
                };
                try
                {
                    var result = await $"{AppConstants.ENDPOINT}api/persons".PostJsonAsync(person).ReceiveJson<Person>();
                    Id = result.Id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

        protected override async void DataPortal_DeleteSelf()
        {
           await DataPortal_Delete(this.Id);
        }

        #endregion
    }
    #endregion

}
