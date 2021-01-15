using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _db;

        public InventoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateAddInventory(Inventory inventory)
        {
            _db.Inventory.Add(inventory);
            _db.SaveChanges();
        }

        public void DeleteInventory(Inventory inventory)
        {
            _db.Inventory.Remove(inventory);
            _db.SaveChanges();
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return _db.Inventory.Include(i => i.Store1).Include(i => i.Item1);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _db.Item;
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _db.Store;
        }

        public Inventory GetInventoryById(int id)
        {
            return _db.Inventory.Include(x => x.Store1).Include(x => x.Item1).FirstOrDefault(x => x.InventoryId == id);
        }

        public Inventory GetInventoryByStoreItemIds(int storeId, int itemId)
        {
            return _db.Inventory.AsNoTracking().FirstOrDefault(u => u.Store1Id == storeId && u.Item1Id == itemId);
        }

        public void UpdateInventory(Inventory inventory)
        {
            _db.Inventory.Update(inventory);
            _db.SaveChanges();
        }
    }
}
