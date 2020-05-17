using System.Collections.Generic;
using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class UnitRepository : GenericRepository<UnitModel>, IUnitRepository
    {
        public UnitRepository(IUnitOfWork context)
           : base(context)
        {

        }

        public void AddRange(IList<UnitModel> listOfUnitModels)
        {
            foreach (var item in listOfUnitModels)
            {
                Add(item);
            }
        }
    }
}
