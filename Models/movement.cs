using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory.Models
{
    /// <summary>
    /// Start 12-03-2021
    /// </summary>
    public class Movement
    {
        [Key]
        public int movementId { get; set; }
        public int typeId { get; set; }

        [ForeignKey("wareHouse")]
        public int wareHouseId { get; set; }
        [ForeignKey("product")]
        public int productId { get; set; }
        public int cant { get; set; }
        public DateTime createOn { get; set; } = System.DateTime.Now;
        public DateTime updateOn { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual WareHouse WareHouse { get; set; }

    }
}
