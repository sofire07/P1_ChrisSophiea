using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;
using DataAccess.Repository;

namespace MyStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index(string searchString)
        {

            var users = _userRepository.GetAllUsers();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = _userRepository.SearchUsers(users, searchString);
            }

            return View(users);
        }

        public IActionResult ViewPurchases(string id)
        {
            //get logged in user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<Purchase> Purchases;

            if (id == null && User.IsInRole(WC.AdminRole))
            {
                Purchases = _userRepository.GetAllPurchases();
            }
            else if (id != null && User.IsInRole(WC.AdminRole))
            {
                Purchases = _userRepository.GetPurchasesById(id);
            }
            else
            {
                string userId = claim.Value;
                Purchases = _userRepository.GetLoggedInPurchases(userId);
            }

            Purchases = _userRepository.GetItemPurchaseByPurchase(Purchases);

            UserPurchaseVM userPurchaseVM = new UserPurchaseVM
            {
                StoreList = _userRepository.GetAllStores(),
                PurchaseList = Purchases
            };

            return View(userPurchaseVM);
        }

    }
}
