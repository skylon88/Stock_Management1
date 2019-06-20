using System.Collections.Generic;
using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(IUnitOfWork context)
           : base(context)
        {

        }

        public void AddRange(IList<Supplier> listOfSupplier)
        {
            foreach (var item in listOfSupplier)
            {
                Add(item);
            }
        }
    }
}
