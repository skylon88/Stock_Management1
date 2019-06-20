using System.Collections.Generic;
using Core.Data;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface IPoRepository : IGenericRepository<PoModel>
    {
         IList<PoModel> GetAllPoModels();
    }
}
