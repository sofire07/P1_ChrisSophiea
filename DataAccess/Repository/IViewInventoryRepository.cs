using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IViewInventoryRepository
    {
        public HomeVM GetHomeVM(int id);

        public DetailVM GetDetailVM(int id);

        public Inventory GetInventoryById(int id);

        public List<ShoppingCart> AddShoppingCart(List<ShoppingCart> shoppingCartList, Inventory inventory, int storeId, int qty);

        public IEnumerable<Inventory> GetAllInventories();

        public Inventory GetInventoryToRemove(List<ShoppingCart> shoppingCartList, IEnumerable<Inventory> inventories, int id);

        public ShoppingCart GetShoppingCartByItemId(List<ShoppingCart> shoppingCartList, int id);

        public List<ShoppingCart> RemoveFromCart(List<ShoppingCart> shoppingCartList, ShoppingCart cart, Inventory inventory);
    }
}
