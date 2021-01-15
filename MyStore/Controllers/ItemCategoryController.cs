using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository;

namespace MyStore.Controllers
{
    public class ItemCategoryController : Controller
    {
        private readonly IItemCategoryRepository _itemCategoryRepository;

        public ItemCategoryController(IItemCategoryRepository itemCategoryRepository)
        {
            _itemCategoryRepository = itemCategoryRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<ItemCategory> itemCategoryList = _itemCategoryRepository.GetItemCategories();
            return View(itemCategoryList);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemCategory obj)
        {
            var existingCategory = _itemCategoryRepository.GetItemCategoryByName(obj.CategoryName);
            if (ModelState.IsValid && existingCategory==null)
            {
                _itemCategoryRepository.CreateAddItemCategory(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _itemCategoryRepository.GetItemCategoryById((int)id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ItemCategory obj)
        {
            var existingCategory = _itemCategoryRepository.GetItemCategoryByName(obj.CategoryName);
            if (ModelState.IsValid && existingCategory == null)
            {
                _itemCategoryRepository.UpdateItemCategory(obj);
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

            var obj = _itemCategoryRepository.GetItemCategoryById((int)id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _itemCategoryRepository.GetItemCategoryById((int)id);
            if (obj == null)
            {
                return NotFound();
            }

            _itemCategoryRepository.DeleteItemCategory(obj);

            return RedirectToAction("Index");
        }
    }

}

