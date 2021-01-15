using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Inventory
    {
        [Display(Name = "Inventory ID")]
        public int InventoryId { get; set; }

        [Required]
        [Display(Name = "Store ID")]
        public int Store1Id { get; set; }

        [Required]
        [Display(Name = "Item ID")]
        public int Item1Id { get; set; }

        [Range(0,int.MaxValue)]
        [Required]
        [Display(Name = "Inventory Amount")]
        public int InventoryAmount { get; set; }

        public virtual Item Item1 { get; set; }
        public virtual Store Store1 { get; set; }
    }
}
