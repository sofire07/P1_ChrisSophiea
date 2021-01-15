using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    
    public class ItemPurchase
    {
        [Display(Name = "Item Purchase ID")]
        public int Id { get; set; }

        [Display(Name = "Item ID")]
        public int Item1ItemId { get; set; }

        [Display(Name = "Purchase ID")]
        public int PurchasesPurchaseId { get; set; }

        [Display(Name = "Item Quantity")]
        public int? ItemQty { get; set; }

        public virtual Item Item1Item { get; set; }
        public virtual Purchase PurchasesPurchase { get; set; }
    }
}
