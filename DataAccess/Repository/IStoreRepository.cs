using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IStoreRepository
    {

        public IEnumerable<Store> GetAllStores();
        public Store GetStoreByAddress(string address);

        public void CreateAddStore(Store store);

        public Store FindStoreById(int id);

        public Store GetStoreByAP(string address, string phone);

        public void UpdateStore(Store store);

        public void DeleteStore(Store store);

    }
}
