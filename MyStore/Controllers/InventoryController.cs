using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository;

namespace MyStore.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
           _inventoryRepository = inventoryRepository;
        }
        public IActionResult Index()
        {
            InventoryVM inventoryVM = new InventoryVM()
            {
                Inventories = _inventoryRepository.GetAllInventories(),
                Stores = _inventoryRepository.GetAllStores()
            };
            return View(inventoryVM);            
        }

        public IActionResult Create(int? id)
        {
            CreateInventoryVM inventoryVM = new CreateInventoryVM()
            {
                Inventory = new Inventory(),
                StoreSelectList = _inventoryRepository.GetAllStores().Select(i => new SelectListItem
                {
                    Text = i.StoreAddress,
                    Value = i.StoreId.ToString()
                }),
                ItemSelectList = _inventoryRepository.GetAllItems().Select(i => new SelectListItem
                {
                    Text = i.ItemName,
                    Value = i.ItemId.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return View(inventoryVM);
            }
            else
            {
                inventoryVM.Inventory = _inventoryRepository.GetInventoryById((int)id);
                if (inventoryVM.Inventory == null) { return NotFound(); }
            }
            return View(inventoryVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateInventoryVM inventoryVM)
        {
            Inventory inventory = inventoryVM.Inventory;
            int storeId = inventory.Store1Id;
            int itemId = inventory.Item1Id;
            

            var existingInventory = _inventoryRepository.GetInventoryByStoreItemIds(storeId,itemId);
            if (ModelState.IsValid && existingInventory==null)
            {
                _inventoryRepository.CreateAddInventory(inventory);
                return RedirectToAction("Index");

            }
            inventoryVM.StoreSelectList = _inventoryRepository.GetAllStores().Select(i => new SelectListItem
            {
                Text = i.StoreAddress,
                Value = i.StoreId.ToString()
            });
            inventoryVM.ItemSelectList = _inventoryRepository.GetAllItems().Select(i => new SelectListItem
            {
                Text = i.ItemName,
                Value = i.ItemId.ToString()
            });

            return View(inventoryVM);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _inventoryRepository.GetInventoryById((int)id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inventory obj)
        {
            if (ModelState.IsValid)
            {
                _inventoryRepository.UpdateInventory(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Inventory inventory = _inventoryRepository.GetInventoryById((int)id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _inventoryRepository.GetInventoryById((int)id);
            if (obj == null)
            {
                return NotFound();
            }

            _inventoryRepository.DeleteInventory(obj);

            return RedirectToAction("Index");
        }
    }
}
