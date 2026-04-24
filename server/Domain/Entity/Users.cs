using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Users: BaseEntity
    {

        public string Name { get; set; }    
        public string Email { get; set; }
        public string AzureOid { get; set; }

        public List<ExpensesEntity> Expenses { get; set; } = new List<ExpensesEntity>();
    }
}
