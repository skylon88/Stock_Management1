using System.Collections.Generic;
using Core.Data;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface IUnitRepository : IGenericRepository<UnitModel>
    {
        void AddRange(IList<UnitModel> listOfUnitModels);
    }
}