using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static PersonEdit NewPersonEdit()
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
            var person = Models.Person.GetById(id);

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
            // using data context here
        }

        private void DoInsertUpdate(Models.Person person)
        {
            using (BypassPropertyChecks)
            {
                person.Id = Id;
                person.Name = Name;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // using data context here
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(ReadProperty(IdProperty));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int id)
        {
            // using data context here
        }
        #endregion
    }
}
