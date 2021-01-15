using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class UserPurchaseVM
    {

        public IEnumerable<SelectListItem> UserSelectList { get; set; }
        public IEnumerable<Purchase> PurchaseList { get; set; }
        public IEnumerable<Store> StoreList { get; set; }
    }
}
