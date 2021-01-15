using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICartRepository
    {
        public IEnumerable<Inventory> GetAllInventories();


        public List<Inventory> GetInventoriesInCart(List<ShoppingCart> shoppingCartList);

        public List<int> GetItemQtysInCart(List<ShoppingCart> shoppingCartList);



        public int GetStoreIdInCart(List<ShoppingCart> shoppingCartList);

        public Purchase CreateAddPurchase(int storeId, string userId, double total);

	    public List<int> GetItemIdsInCart(List<ShoppingCart> shoppingCartList);


        public void CreateAddItemPurchase(Purchase purchase, List<int> itemIds, List<int> itemQtys);

        public IEnumerable<Item> GetItemListByIds(List<int> itemIds);

        public Inventory GetInventoryById(int id);

        public ApplicationUser GetUserById(string userId);

        public List<ShoppingCart> RemoveInventoryFromCart(List<ShoppingCart> shoppingCartList, Inventory inventory, int qty); 


    }
}
