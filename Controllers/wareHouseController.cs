using inventory.Interfaces;
using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventory.Controllers
{
    /// <summary>
    /// Operations API
    /// Start 12-03-2021
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class wareHouseController : ControllerBase
    {
        private readonly iRepository _repository;
        ILogger<wareHouseController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public wareHouseController(ILogger<wareHouseController> logger,inventoryDBContext context, iRepository repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Get all warehouse
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareHouse>>> GetALLWareHouses()
        {
            _logger.LogInformation("Get all warehouse");
            var model = await _repository.SelectALL<WareHouse>();
            return model;
        }


    }
}
