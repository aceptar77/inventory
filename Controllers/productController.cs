using inventory.Interfaces;
using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace inventory.Controllers
{
    /// <summary>
    ///  Operations API
    ///  Start 12-03-2021
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly iRepository _repository;
        ILogger<productController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>

        public productController(ILogger<productController> logger, inventoryDBContext context, iRepository repository)
        {
            _logger = logger;
             _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        ///  Find a product
        /// </summary>
        /// <param name="productId"></param>    
        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            _logger.LogInformation("Find a product");
            var model = await _repository.SelectById<Product>(productId);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

    }
}
