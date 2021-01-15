using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateAddStore(Store store)
        {
            _db.Store.Add(store);
            _db.SaveChanges();
        }

        public Store FindStoreById(int id)
        {
            return _db.Store.Find(id);
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _db.Store;
        }

        public Store GetStoreByAddress(string address)
        {
            return _db.Store.AsNoTracking().FirstOrDefault(i => i.StoreAddress == address);
        }

        public Store GetStoreByAP(string address, string phone)
        {
            return _db.Store.AsNoTracking().FirstOrDefault(i => i.StoreAddress == address && i.PhoneNumber == phone);
        }

        public void DeleteStore(Store store)
        {
            _db.Store.Remove(store);
            _db.SaveChanges();
        }

        public void UpdateStore(Store store)
        {
            _db.Store.Update(store);
            _db.SaveChanges();
        }
    }
}
