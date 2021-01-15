using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IItemCategoryRepository
    {
        public IEnumerable<ItemCategory> GetItemCategories();
        public ItemCategory GetItemCategoryByName(string name);

        public void CreateAddItemCategory(ItemCategory itemCategory);

        public ItemCategory GetItemCategoryById(int id);

        public void UpdateItemCategory(ItemCategory itemCategory);

        public void DeleteItemCategory(ItemCategory itemCategory);
    }
}
