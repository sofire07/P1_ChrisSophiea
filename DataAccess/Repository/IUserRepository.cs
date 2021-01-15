using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IUserRepository 
    {
        public IQueryable<ApplicationUser> GetAllUsers();
        public IQueryable<ApplicationUser> SearchUsers(IQueryable<ApplicationUser> users, string searchString);

        public IEnumerable<Purchase> GetAllPurchases();

        public IEnumerable<Purchase> GetLoggedInPurchases(string id);

        public IEnumerable<Purchase> GetPurchasesById(string id);

        public IEnumerable<Purchase> GetItemPurchaseByPurchase(IEnumerable<Purchase> purchases);

        public IEnumerable<Store> GetAllStores();
    }
}
