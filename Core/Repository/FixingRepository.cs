using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class FixingRepository : GenericRepository<FixModel>, IFixingRepository
    {
        public FixingRepository(IUnitOfWork context)
           : base(context)
        {

        }
    }
}
