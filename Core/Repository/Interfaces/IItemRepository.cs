using System.Collections.Generic;
using Core.Data;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        void AddRange(IList<Item> entities);

        void CleanUp();
    }

    public interface IPositionRepository : IGenericRepository<Position>
    {
        void AddRange(IList<Position> entities);

        void EditRange(IList<Position> entities);
    }

}
