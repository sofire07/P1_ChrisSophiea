using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _db.Store;
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _db.ApplicationUser.FirstOrDefault(i => i.Id == userId);
        }
    }
}
