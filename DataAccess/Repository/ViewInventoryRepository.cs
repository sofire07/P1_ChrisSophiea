using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ViewInventoryRepository : IViewInventoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ViewInventoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public HomeVM GetHomeVM(int id)
        {
            HomeVM homeVM = new HomeVM()
            {
                Inventories = _db.Inventory.Include(x => x.Item1).Include(x => x.Store1).Include(x => x.Item1.ItemCategory).Where(x => x.Store1Id == id && x.InventoryAmount > 0),
                ItemCategories = _db.ItemCategory,
                Store = _db.Store.FirstOrDefault(x => x.StoreId == id),
            };
            return homeVM;
        }

        public DetailVM GetDetailVM(int id)
        {
            DetailVM detailVM = new DetailVM()
            {
                Inventory = _db.Inventory.Include(i => i.Item1).Include(i => i.Store1).Include(i => i.Item1.ItemCategory).FirstOrDefault(x => x.InventoryId == id),
                ExistsInCart = false
            };
            return detailVM;
        }

        public Inventory GetInventoryById(int id)
        {
            return _db.Inventory.AsNoTracking().Include(i => i.Store1).FirstOrDefault(x => x.InventoryId == id);
        }

        public List<ShoppingCart> AddShoppingCart(List<ShoppingCart> shoppingCartList, Inventory inventory, int storeId, int qty)
        {
            shoppingCartList.Add(new ShoppingCart
            {
                ProductId = inventory.Item1Id,
                StoreId = storeId,
                ProductQty = qty
            });
            inventory.InventoryAmount -= qty;
            _db.Inventory.Update(inventory);
            _db.SaveChanges();

            return shoppingCartList;
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return _db.Inventory.AsNoTracking().Include(i => i.Store1).Include(i => i.Item1);
        }

        public Inventory GetInventoryToRemove(List<ShoppingCart> shoppingCartList, IEnumerable<Inventory> inventories, int id)
        {
            Inventory inventory = new Inventory();
            foreach (ShoppingCart sc in shoppingCartList)
            {
                foreach (Inventory i in inventories)
                {
                    if (i.Item1Id == sc.ProductId && i.Store1Id == sc.StoreId && id == i.Item1Id)
                    {
                        inventory = i;
                    }
                }

            }
            return inventory;
        }

        public ShoppingCart GetShoppingCartByItemId(List<ShoppingCart> shoppingCartList, int id)
        {
            return shoppingCartList.Find(x => x.ProductId == id);
        }

        public List<ShoppingCart> RemoveFromCart(List<ShoppingCart> shoppingCartList, ShoppingCart cart, Inventory inventory)
        {
            var itemToRemove = shoppingCartList.SingleOrDefault(i => i.ProductId == inventory.Item1Id);

            inventory.InventoryAmount += cart.ProductQty;

            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }

            _db.Update(inventory);
            _db.SaveChanges();
            return shoppingCartList;
        }
    }
}
