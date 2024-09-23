using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2EF.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ICollection<Product> Products { get; set; }=null!;
    }
}
