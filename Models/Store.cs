using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Store
    {
        public Store()
        {
            Inventories = new HashSet<Inventory>();
            Purchases = new HashSet<Purchase>();
        }

        [Display(Name = "Store ID")]
        public int StoreId { get; set; }

        [Required]
        [Display(Name = "Store Address")]
        public string StoreAddress { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
