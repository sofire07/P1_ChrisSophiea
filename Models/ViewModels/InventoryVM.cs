using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class InventoryVM
    {
        public IEnumerable<Inventory> Inventories { get; set; }
        public IEnumerable<Store> Stores { get; set; }

    }
}
