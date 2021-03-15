using System;
using System.ComponentModel.DataAnnotations;

namespace inventory.Models
{
    /// <summary>
    /// Start 12-03-2021
    /// </summary>
    public class WareHouse
    {
        [Key]
        public int wareHouseId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int maxCapacity { get; set; }
        public DateTime createOn { get; set; } = System.DateTime.Now;
        public DateTime updateOn { get; set; }

        public virtual Movement Movement { get; set; }
    }
}