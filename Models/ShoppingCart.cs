using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int ProductQty { get; set; }
    }
}
