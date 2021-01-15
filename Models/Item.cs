using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Item
    {
        public Item()
        {
            Inventories = new HashSet<Inventory>();
            ItemPurchases = new HashSet<ItemPurchase>();
        }

        [Display(Name = "Item ID")]
        public int ItemId { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        public string ItemName { get; set; }

        [Display(Name = "Item Category")]
        [Required]
        public int ItemCategoryId { get; set; }
        [ForeignKey("ItemCategoryId")]
        public virtual ItemCategory ItemCategory { get; set; }

        [Display(Name = "Item Description")]
        [Required]
        public string ItemDescription { get; set; }

        [Display(Name = "Item Price")]
        [Range(1, 100), DataType(DataType.Currency)]
        [Required]
        public double ItemPrice { get; set; }

        [Display(Name = "Item Image")]
        public string ItemImage { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<ItemPurchase> ItemPurchases { get; set; }
    }
}
