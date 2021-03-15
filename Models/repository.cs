using inventory.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventory.Models
{
    /// <summary>
    /// Start 12-03-2021
    /// </summary>
    public class Repository<TDbContext> : iRepository where TDbContext : DbContext
    {
        protected TDbContext dbContext;

        /// <summary>
        ///  Constructor
        /// </summary>
        public Repository(TDbContext context)
        {
            dbContext = context;
        }

        /// <summary>
        ///  Find by id
        /// </summary>
        /// <param name="id"></param>   
        public async Task<T> SelectById<T>(int id) where T : class
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Get all 
        /// </summary>
        public async Task<List<T>> SelectALL<T>() where T : class
        {
            return await this.dbContext.Set<T>().ToListAsync();
        }
 
        public async Task<List<T>> SelectALLMovement<T>() where T : class
        {
            return await this.dbContext.Set<T>().ToListAsync();
        }

    }
}

