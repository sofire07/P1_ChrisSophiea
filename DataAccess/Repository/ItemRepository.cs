using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _db;

        public ItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateAddItem(Item item)
        {
            _db.Item.Add(item);
            _db.SaveChanges();
        }

        public void DeleteItem(Item item)
        {
            _db.Item.Remove(item);
            _db.SaveChanges();
        }

        public Item FindItemById(int id)
        {
            return _db.Item.Find(id);
        }

        public IEnumerable<ItemCategory> GetAllItemCategories()
        {
            return _db.ItemCategory;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _db.Item.Include(i => i.ItemCategory);
        }

        public Item GetItemById(int id)
        {
            return _db.Item.Include(a => a.ItemCategory).FirstOrDefault(a => a.ItemId == id);
        }

        public Item GetItemByName(string name)
        {
            return _db.Item.AsNoTracking().FirstOrDefault(i => i.ItemName == name);
        }

        public void UpdateItem(Item item)
        {
            _db.Item.Update(item);
            _db.SaveChanges();
        }
    }
}
