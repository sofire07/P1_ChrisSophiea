using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IHomeRepository
    {
        public IEnumerable<Store> GetAllStores();
        public ApplicationUser GetUserById(string userId);


    }
}
