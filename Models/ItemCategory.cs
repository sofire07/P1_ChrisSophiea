using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ItemCategory
    {
        [Key]
        [Display(Name = "Category ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }
}
