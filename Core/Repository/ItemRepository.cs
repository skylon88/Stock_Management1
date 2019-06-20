
using System.Collections.Generic;
using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(IUnitOfWork context)
           : base(context)
        {

        }



        public void AddRange(IList<Item> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }

        public void CleanUp()
        {
            //_entities.Items.RemoveRange(_entities.Items);
        }
    }

    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(IUnitOfWork context)
           : base(context)
        {

        }

        public void AddRange(IList<Position> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }

        public void EditRange(IList<Position> entities)
        {
            foreach (var item in entities)
            {
                Edit(item);
            }
        }
    }
}
