using Microsoft.EntityFrameworkCore;

namespace inventory.Models
{

    /// <summary>
    /// Definition of entities.
    /// Start 12-03-2021
    /// </summary>
    public class inventoryDBContext : DbContext
    {
        private readonly DbContextOptions _options;

     
        /// <summary>
        /// Constructor
        /// </summary>
        public inventoryDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<WareHouse> WareHouse { get; set; }
        public DbSet<Movement> Movement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
