using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using DataAccess.Repository;

namespace MyStore.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(IItemRepository itemRepository, IWebHostEnvironment webHostEnvironment)
        {
            _itemRepository = itemRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Item> objList = _itemRepository.GetAllItems();
            return View(objList);
        }

        //GET - Upsert
        public IActionResult Upsert(int? id)
        {
            ItemVM productVM = new ItemVM()
            {
                Item = new Item(),
                CategorySelectList = _itemRepository.GetAllItemCategories().Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Item = _itemRepository.FindItemById((int)id);
                if (productVM.Item == null) { return NotFound(); }
            }
            return View(productVM);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ItemVM productVM)
        {
            Item item = productVM.Item;
            var existingItem = _itemRepository.GetItemByName(item.ItemName);

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Item.ItemId == 0 && existingItem == null)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Item.ItemImage = fileName + extension;
                    _itemRepository.CreateAddItem(item);

                }
                else
                {
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        if (existingItem.ItemImage != null)
                        {
                            var oldFile = Path.Combine(upload, existingItem.ItemImage);
                            if (System.IO.File.Exists(oldFile))
                            {
                                System.IO.File.Delete(oldFile);
                            }
                        }
                        

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productVM.Item.ItemImage = fileName + extension;

                    }
                    else
                    {
                        productVM.Item.ItemImage = existingItem.ItemImage;
                    }
                    _itemRepository.UpdateItem(item);

                }
                return RedirectToAction("Index");

            }
            productVM.CategorySelectList = _itemRepository.GetAllItems().Select(i => new SelectListItem
            {
                Text = i.ItemName,
                Value = i.ItemId.ToString()
            });
        
            return View(productVM);
        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Item product = _itemRepository.GetItemById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _itemRepository.FindItemById((int)id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;


            var oldFile = Path.Combine(upload, obj.ItemImage);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _itemRepository.DeleteItem(obj);


            return RedirectToAction("Index");
        }


    }
}
