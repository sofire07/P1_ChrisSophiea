using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Repository;
using Models;
using Models.ViewModels;
using Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController(ApplicationDbContext db, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;

        }

        public IActionResult Index()
        {
            //GetShoppingCartList
            List <ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<Inventory> inventoriesInCart = _cartRepository.GetInventoriesInCart(shoppingCartList);

            List<int> itemQtys = _cartRepository.GetItemQtysInCart(shoppingCartList);

            IDictionary<Inventory, int> productList = new Dictionary<Inventory, int>();
            for(int i = 0; i < shoppingCartList.Count(); i++)
            {
                productList.Add(inventoriesInCart[i], itemQtys[i]);
            }
            return View(productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut(double total)
        {
            //Get Logged In User Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            //Get Shopping Cart List
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            int storeId = _cartRepository.GetStoreIdInCart(shoppingCartList);

            Purchase purchase = _cartRepository.CreateAddPurchase(storeId, userId, total);

            List<int> productIds = _cartRepository.GetItemIdsInCart(shoppingCartList);
            List<int> qtys = _cartRepository.GetItemQtysInCart(shoppingCartList);

            _cartRepository.CreateAddItemPurchase(purchase, productIds, qtys);

            IEnumerable<Item> itemList = _cartRepository.GetItemListByIds(productIds);

            ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = _cartRepository.GetUserById(userId),
                ProductList = itemList.ToList(),
                PurchaseTotal = total
            };
            HttpContext.Session.Clear();
            return View(ProductUserVM);
        }



        public IActionResult Remove(int id, int qty) {
            //Inventory inventory = _db.Inventory.FirstOrDefault(x => x.InventoryId == id);
            Inventory inventory = _cartRepository.GetInventoryById(id);

            //Get Shopping Cart List
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppingCartList = _cartRepository.RemoveInventoryFromCart(shoppingCartList, inventory, qty);
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(Index));
        }
    }
}
