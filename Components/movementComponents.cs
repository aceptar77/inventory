using inventory.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory.Components
{
    public class movementComponents
    {
        /// <summary>
        /// Inject the DBContext into the components...
        /// Start 12-03-2021
        /// </summary>
        private readonly inventoryDBContext _context;

        public movementComponents(inventoryDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Loading products to warehouse
        /// Start 12-03-2021
        /// </summary>
        public async Task<Movement> LoadingProductsWarehouse(Movement movement)
        {
            int wareHouseMaximumQuantity = _context.WareHouse.Where(x => x.wareHouseId == movement.wareHouseId).FirstOrDefault().maxCapacity;
            int productMaximumQuantity = _context.Product.Where(x => x.productId == movement.productId).FirstOrDefault().maxCapacity;
            int inputMovement = (from x in _context.Movement.Where(x => x.typeId == 1 && x.productId == movement.productId && x.wareHouseId == movement.wareHouseId).ToList() select x.cant).Sum();
            int outMovement = (from x in _context.Movement.Where(x => x.typeId == 2 && x.productId == movement.productId && x.wareHouseId == movement.wareHouseId).ToList() select x.cant).Sum();
            int balance = inputMovement - outMovement;
            if (movement.cant < productMaximumQuantity && balance < wareHouseMaximumQuantity)
            {
                _context.Add(movement);
                await _context.SaveChangesAsync();
            }
            else
            {
                movement.movementId = -1;
            }

            return movement;
        }

        /// <summary>
        /// Get balance by product
        /// Start 12-03-2021
        /// </summary>
            public int BalanceProductMovement(int productId)
            {
                int inputMovement = (from x in _context.Movement.Where(x => x.typeId == 1 && x.productId == productId).ToList() select x.cant).Sum();
                int outMovement = (from x in _context.Movement.Where(x => x.typeId == 2 && x.productId == productId).ToList() select x.cant).Sum();
                return inputMovement - outMovement;
            }

            /// <summary>
            /// Take product from warehouse
            /// Start 12-03-2021
            /// </summary>
            public async Task<Movement> TakeProductWareHouse(Movement movement)
            {
                _context.Movement.Add(movement);
                await _context.SaveChangesAsync();
                return movement;
            }


            /// <summary>
            /// MoveProductWareHouseAnother
            /// Start 12-03-2021
            /// </summary>
            public async Task<List<Movement>> MoveProductWareHouseAnother(List<Movement> listMovement)
            {
                await TakeProductWareHouse(listMovement.Where(x => x.typeId == 1).FirstOrDefault());
                await LoadingProductsWarehouse(listMovement.Where(x => x.typeId == 2).FirstOrDefault());
                return listMovement;
            }
        }
    }
