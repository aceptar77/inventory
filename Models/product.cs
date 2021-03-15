using System;
using System.ComponentModel.DataAnnotations;

namespace inventory.Models
{
    /// <summary>
    /// Start 12-03-2021
    /// </summary>
    public class Product
    {
        [Key]
        public int productId { get; set; }
        public string description { get; set; }
        public string sku { get; set; }
        public DateTime createOn { get; set; } = System.DateTime.Now;
        public DateTime updateOn { get; set; }
        public int maxCapacity { get; set; }
        public virtual Movement Movement { get; set; }
    }
}
