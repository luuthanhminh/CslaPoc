using Client.Core.Models;
using Csla;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.Business
{
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
                catch (Exception ex)
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
}
