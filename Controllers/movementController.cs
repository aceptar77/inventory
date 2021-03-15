using inventory.Components;
using inventory.Interfaces;
using inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace inventory.Controllers
{
    /// <summary>
    /// Operations API
	/// Start 12-03-2021
	/// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly ILogger<MovementController> _logger;
        private readonly iRepository _repository;
        private readonly movementComponents _movementComponents;
        /// <summary>
        /// Constructor
        /// </summary>
        public MovementController(ILogger<MovementController> logger, inventoryDBContext context, iRepository repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); 
            _movementComponents = new movementComponents(context);
            _movementComponents = _movementComponents ?? throw new ArgumentNullException(nameof(_movementComponents));
        }

        /// <summary>
        /// GetALLMovement from product
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movement>>> GetALLMovements()
        {
            _logger.LogInformation("GetALLMovement from product");
            var model = await _repository.SelectALL<Movement>();
            return model;
        }

        /// <summary>
        ///  Get MovementById
        /// </summary>
        /// <param name="movementId"></param>    
        [HttpGet("{movementId}")]
        public async Task<ActionResult<Movement>> GetMovementById(int movementId)
           {
            _logger.LogInformation("Find Movement");
            var model = await _repository.SelectById<Movement>(movementId);

            return model;
        }
    
        /// <summary>
        ///  Get balance of product
        /// </summary>
        /// <param name=" productId"></param>    
        [HttpGet("{productId}")]
        public ActionResult<int> GetBalanceById(int productId)
        {
            _logger.LogInformation("Get balance of product");
            return _movementComponents.BalanceProductMovement(productId);
        }

        /// <summary>
        /// Loading Products to Warehouse
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Movement/PostLoadingProductsWarehouse
        ///  {
        ///  "typeId": 1,
        ///  "wareHouseId": 1,
        ///  "productId": 1,
        ///  "cant": 1
        ///  }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A newly Movement</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        public async Task<ActionResult<Movement>> PostLoadingProductsWarehouse(Movement model)
        {
            _logger.LogInformation("Add product to warehouse: {movementId}", model.movementId);
            await _movementComponents.LoadingProductsWarehouse(model);
            if (model.movementId > 0)
            {
                return CreatedAtAction("GetMovementById", new { movementId = model.movementId }, model);
            }
            else
            {
                _logger.LogInformation("Exceeding the maximum capacity");
                return BadRequest("Exceeding the maximum capacity");
            }
        }

        /// <summary>
        /// Take product from warehouse
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Movement/PostTakeProductWareHouse
        ///  {
        ///  "typeId": 2,
        ///  "wareHouseId": 1,
        ///  "productId": 1,
        ///  "cant": 1,
        ///  }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A newly Movement</returns>
        /// <response code="201">Returns the newly product recall</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        public async Task<ActionResult<Movement>> PostTakeProductWareHouse(Movement model)
        {
            _logger.LogInformation("product recall to warehouse: {movementId}", model.movementId);
            await _movementComponents.TakeProductWareHouse(model);
            return CreatedAtAction("GetMovementById", new { movementId = model.movementId }, model);
        }

        /// <summary>
        ///  Moving product from warehouse another warehouse
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Movement>> PostMovingProductWareHouseAnother(List<Movement> listModel)
        {
            _logger.LogInformation("Moving product from warehouse another warehouse: {movementId}", listModel.Where(x => x.typeId == 1).FirstOrDefault().movementId);
            await _movementComponents.MoveProductWareHouseAnother(listModel);
            return CreatedAtAction("GetMovementById", new { movementId = listModel.Where(x => x.typeId == 1).FirstOrDefault().movementId }, listModel.Where(x => x.typeId == 1).FirstOrDefault());
        }

    }
}
