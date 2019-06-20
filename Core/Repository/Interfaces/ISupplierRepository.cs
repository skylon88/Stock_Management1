using System.Collections.Generic;
using Core.Data;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        void AddRange(IList<Supplier> listOfSupplier);
    }
}
