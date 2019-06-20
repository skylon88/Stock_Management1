using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class PoRepository : GenericRepository<PoModel>, IPoRepository
    {
        public PoRepository(IUnitOfWork context)
           : base(context)
        {

        }

        public IList<PoModel> GetAllPoModels()
        {
            return GetAll().Include(x => x.Contracts.Select(z=>z.RequestHeaders)).ToList();
        }
    }
}
