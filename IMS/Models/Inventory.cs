using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int ProductId { get; internal set; }
        public string Location { get; set; }
        
        public List<Product> Products { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Transaction> Transactions { get; set; }
        
    }
}
