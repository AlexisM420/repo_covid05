using System;
using System.Collections.Generic;
using System.Text;

namespace BillingManagement.Business
{
    interface IDataService<T>
    {
        IEnumerable<T> GetAll();
    }
}
