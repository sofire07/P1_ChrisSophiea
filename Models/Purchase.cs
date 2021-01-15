using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Purchase
    {
        public Purchase()
        {
            ItemPurchase = new HashSet<ItemPurchase>();
        }

        [Display(Name = "Purchase ID")]
        public int PurchaseId { get; set; }

        [Display(Name = "Customer ID")]
        public string Customer1Id { get; set; }

        [Display(Name = "Store ID")]
        public int Store1Id { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }

        [ForeignKey("Customer1Id")]
        public virtual ApplicationUser Customer1 { get; set; }
        public virtual Store Store1 { get; set; }

        public virtual ICollection<ItemPurchase> ItemPurchase { get; set; }

    }
}
