using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IItemRepository
    {
        public IEnumerable<Item> GetAllItems();
        public IEnumerable<ItemCategory> GetAllItemCategories();

        public Item FindItemById(int id);
        public Item GetItemById(int id);
        public Item GetItemByName(string name);

        public void CreateAddItem(Item item);
        public void UpdateItem(Item item);

        public void DeleteItem(Item item);
    }
}
