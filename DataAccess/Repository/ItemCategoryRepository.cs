using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ItemCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public void CreateAddItemCategory(ItemCategory itemCategory)
        {
            _db.ItemCategory.Add(itemCategory);
            _db.SaveChanges();
        }

        public void DeleteItemCategory(ItemCategory itemCategory)
        {
            _db.ItemCategory.Remove(itemCategory);
            _db.SaveChanges();
        }

        public IEnumerable<ItemCategory> GetItemCategories()
        {
            return _db.ItemCategory;
        }

        public ItemCategory GetItemCategoryById(int id)
        {
            return _db.ItemCategory.Find(id);
        }

        public ItemCategory GetItemCategoryByName(string name)
        {
            return _db.ItemCategory.AsNoTracking().FirstOrDefault(i => i.CategoryName == name);
        }

        public void UpdateItemCategory(ItemCategory itemCategory)
        {
            _db.ItemCategory.Update(itemCategory);
            _db.SaveChanges();
        }
    }
}
