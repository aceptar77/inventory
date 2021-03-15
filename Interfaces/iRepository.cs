using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventory.Interfaces
{
    /// <summary>
    /// Interface pattern repository 
    /// Start 12-03-2021
    /// </summary>
    public interface iRepository
    {
        Task<T> SelectById<T>(int id) where T : class;
        Task<List<T>> SelectALL<T>() where T : class;
    }
}
