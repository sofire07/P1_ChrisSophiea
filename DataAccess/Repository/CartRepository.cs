using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public void CreateAddItemPurchase(Purchase purchase, List<int> itemIds, List<int> itemQtys)
        {
            for (int i = 0; i < itemIds.Count(); i++)
            {
                ItemPurchase itemPurchase = new ItemPurchase()
                {
                    PurchasesPurchaseId = purchase.PurchaseId,
                    Item1ItemId = itemIds[i],
                    ItemQty = itemQtys[i],
                };
                _db.ItemPurchase.Add(itemPurchase);
                _db.SaveChanges();
            }
        }

        public Purchase CreateAddPurchase(int storeId, string userId, double total)
        {
            Purchase purchase = new Purchase
            {
                Store1Id = storeId,
                Customer1Id = userId,
                PurchaseDate = DateTime.Now,
                TotalPrice = total,
            };
            _db.Purchase.Add(purchase);
            _db.SaveChanges();

            return purchase;
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return _db.Inventory.Include(x => x.Store1).Include(x => x.Item1);
        }


        public List<Inventory> GetInventoriesInCart(List<ShoppingCart> shoppingCartList)
        {
            IEnumerable<Inventory> inventories = GetAllInventories();
            List<Inventory> inventoriesInCart = new List<Inventory>();
            foreach (ShoppingCart sc in shoppingCartList)
            {
                foreach (Inventory i in inventories)
                {
                    if (sc.ProductId == i.Item1Id && sc.StoreId == i.Store1Id)
                    {
                        inventoriesInCart.Add(i);
                    }
                }
            }
            return inventoriesInCart;
        }

        public Inventory GetInventoryById(int id)
        {
            return _db.Inventory.FirstOrDefault(x => x.InventoryId == id);
        }

        public List<int> GetItemIdsInCart(List<ShoppingCart> shoppingCartList)
        {
            List<int> itemIds = new List<int>();
            for (int i = 0; i < shoppingCartList.Count(); i++)
            {
                itemIds.Add(shoppingCartList[i].ProductId);
            }
            return itemIds;
        }

        public IEnumerable<Item> GetItemListByIds(List<int> itemIds)
        {
            return _db.Item.Where(i => itemIds.Contains(i.ItemId));
        }

        public List<int> GetItemQtysInCart(List<ShoppingCart> shoppingCartList)
        {
            List<int> itemQtys = new List<int>();
            for(int i = 0; i < shoppingCartList.Count(); i++)
            {
                itemQtys.Add(shoppingCartList[i].ProductQty);
            }
            return itemQtys;
        }


        public int GetStoreIdInCart(List<ShoppingCart> shoppingCartList)
        {
            return shoppingCartList.Select(i => i.StoreId).FirstOrDefault();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _db.ApplicationUser.FirstOrDefault(i => i.Id == userId);
        }

        public List<ShoppingCart> RemoveInventoryFromCart(List<ShoppingCart> shoppingCartList, Inventory inventory, int qty)
        {
            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == inventory.Item1Id && u.StoreId == inventory.Store1Id));
            inventory.InventoryAmount += qty;
            _db.Update(inventory);
            _db.SaveChanges();
            return shoppingCartList;
        }
    }
}
