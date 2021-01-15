using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IInventoryRepository
    {
        public IEnumerable<Inventory> GetAllInventories();

        public IEnumerable<Store> GetAllStores();

        public IEnumerable<Item> GetAllItems();

        public Inventory GetInventoryById(int id);

        public Inventory GetInventoryByStoreItemIds(int store, int item);

        public void CreateAddInventory(Inventory inventory);

        public void UpdateInventory(Inventory inventory);

        public void DeleteInventory(Inventory inventory);
    }
}
