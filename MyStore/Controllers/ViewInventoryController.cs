using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;
using Models.ViewModels;
using Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository;

namespace MyStore.Controllers
{
    public class ViewInventoryController : Controller
    {
        private readonly IViewInventoryRepository _viRepo;

        public ViewInventoryController(IViewInventoryRepository viRepo)
        {
            _viRepo = viRepo;
        }
        public IActionResult Index(int id)
        {
            HomeVM homeVM = _viRepo.GetHomeVM(id);
            return View(homeVM);
        }

        public IActionResult Detail(int id)
        {
            //Get Shopping Cart List
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }


            DetailVM detailVM = _viRepo.GetDetailVM(id);

            Inventory inventory = _viRepo.GetInventoryById(id);

            foreach (var item in shoppingCartList)
            {
                if (item.ProductId == inventory.Item1Id)
                {
                    detailVM.ExistsInCart = true;
                }
            }

            return View(detailVM);
        }

        [HttpPost, ActionName("Detail")]
        public IActionResult DetailPost(int id, int qty)
        {
            Inventory inventory = _viRepo.GetInventoryById(id);
            int storeid = inventory.Store1Id;


            //Get Shopping Cart List
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            //Makes sure items in shopping cart are all from same store
            foreach (ShoppingCart sc in shoppingCartList)
            {
                if(sc.StoreId != storeid)
                {
                    shoppingCartList = new List<ShoppingCart>();
                }
            }


            shoppingCartList = _viRepo.AddShoppingCart(shoppingCartList, inventory, storeid, qty);
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);


            return RedirectToAction("Index", new { id = storeid });
        }

        public IActionResult RemoveFromCart(int id)
        {
            Inventory inventory = _viRepo.GetInventoryById(id);

            //get shopping cart list
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            //get all inventories
            IEnumerable<Inventory> inventories = _viRepo.GetAllInventories();


            inventory = _viRepo.GetInventoryToRemove(shoppingCartList, inventories, inventory.Item1Id);

            ShoppingCart shoppingCart = _viRepo.GetShoppingCartByItemId(shoppingCartList, inventory.Item1Id);


            Inventory inventory2 = _viRepo.GetInventoryById(id);

            int returnToStoreId = inventory2.Store1Id;

            shoppingCartList = _viRepo.RemoveFromCart(shoppingCartList, shoppingCart, inventory);

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction("Index", new { id = returnToStoreId });
        }
    }
}
