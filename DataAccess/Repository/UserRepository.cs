using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            return _db.Purchase.Include(i => i.Store1).Include(i => i.Customer1);
        }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return from u in _db.ApplicationUser select u;
        }

        public IEnumerable<Purchase> GetItemPurchaseByPurchase(IEnumerable<Purchase> purchases)
        {
            foreach (Purchase p in purchases)
            {
                p.ItemPurchase = _db.ItemPurchase.Include(i => i.Item1Item).Where(i => i.PurchasesPurchaseId == p.PurchaseId).ToList();
            }
            return purchases;
        }

        public IEnumerable<Purchase> GetLoggedInPurchases(string id)
        {
            return _db.Purchase.Include(i => i.Store1).Include(i => i.ItemPurchase).Include(i => i.Customer1).Where(x => x.Customer1Id == id);
        }

        public IEnumerable<Purchase> GetPurchasesById(string id)
        {
            return _db.Purchase.Include(i => i.Store1).Include(i => i.ItemPurchase).Include(i => i.Customer1).Where(x => x.Customer1Id == id);
        }

        public IQueryable<ApplicationUser> SearchUsers(IQueryable<ApplicationUser> users, string searchString)
        {
            return users.Where(s => s.FullName.Contains(searchString) || s.Email.Contains(searchString));
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _db.Store;
        }
    }
}
