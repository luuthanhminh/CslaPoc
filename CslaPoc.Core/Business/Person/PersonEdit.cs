using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CslaPoc.Core.DataContext;

namespace CslaPoc.Core.Business.Person
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

        public static PersonEdit GetPersonEdit(int Id)
        {
            return DataPortal.Fetch<PersonEdit>(Id);
        }

        public static void DeletePersonEdit(int Id)
        {
            DataPortal.Delete<PersonEdit>(Id);
        }
        #endregion

        #region Data Access

       
        private void DataPortal_Fetch(int id)
        {
            var dbContext = new DbContext();
            var person = dbContext.GetPerson(id);

            using (BypassPropertyChecks)
            {
                Id = person.Id;
                Name = person.Name;
            }
            BusinessRules.CheckRules();

        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int id)
        {
            var dbContext = new DbContext();
            using (BypassPropertyChecks)
            {
                dbContext.DeletePerson(id);
            }
        }
        #endregion

        #region Overrides

        protected override void DataPortal_Create()
        {
            var dbContext = new DbContext();
            var person = dbContext.Create();
            using (BypassPropertyChecks)
            {
                Id = person.Id;
                Name = person.Name;
            }
            BusinessRules.CheckRules();
        }


        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            var dbContext = new DbContext();

            using (BypassPropertyChecks)
            {
                var person = new Models.Person()
                {
                    Name = this.Name
                };
                Id = dbContext.InsertPerson(person);
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.Id);
        }

        #endregion
    }
}
