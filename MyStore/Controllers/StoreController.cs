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
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Store> objList = _storeRepository.GetAllStores();
            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Store obj)
        {
            var existingStore = _storeRepository.GetStoreByAddress(obj.StoreAddress);
            if (ModelState.IsValid && existingStore == null)
            {
                _storeRepository.CreateAddStore(obj);
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
            var obj = _storeRepository.FindStoreById((int)id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Store obj)
        {
            string address = obj.StoreAddress;
            string phone = obj.PhoneNumber;
            var existingStore = _storeRepository.GetStoreByAP(address, phone);
            if (ModelState.IsValid && existingStore == null)
            {
                _storeRepository.UpdateStore(obj);
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
            var obj = _storeRepository.FindStoreById((int)id);
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
            var obj = _storeRepository.FindStoreById((int)id);
            if (obj == null)
            {
                return NotFound();
            }

            _storeRepository.DeleteStore(obj);


            return RedirectToAction("Index");
        }
    }
}
