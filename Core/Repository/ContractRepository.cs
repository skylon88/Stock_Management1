using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class ContractRepository : GenericRepository<Contract>, IContractRepository
    {
        public ContractRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {

        }
    }
}
