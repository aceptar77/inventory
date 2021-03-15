using inventory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace inventory.Data
{
    /// <summary>
    ///  Create dummy data
    ///  Start 12-03-2021
    /// </summary>
    public class dataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new inventoryDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<inventoryDBContext>>()))
            {
                if (context.Product.Any())
                {
                    return;   // Data was already seeded
                }
                context.Product.AddRange(
               new Product
               {
                   productId = 1,
                   description = "MEASURING AMPLIFIER ME50 S6",
                   sku = "30049099",
                   maxCapacity = 1000

               },
               new Product
               {
                   productId = 2,
                   description = "SHAFT SEAL",
                   sku = "84841001"
               },
               new Product
               {
                   productId = 3,
                   description = "RWJ_17021 (REFERENCE STANDARD)",
                   maxCapacity = 1000,
                   sku = "29329999"
               },
               new Product
               {
                   productId = 4,
                   description = "MEASURING AMPLIFIER ME50 S6",
                   sku = "96041089",
                   maxCapacity = 1000
               },
               new Product
               {
                   productId = 5,
                   description = "TUB RETIN-A CR 0.05% C/40G MX-CENCA-VE",
                   sku = "76121001",
                   maxCapacity = 1000
               },
               new Product
               {
                   productId = 6,
                   description = "CYLINDRICAL PIN FOR PM 2000, AISI 304, 12 x 25 mm",
                   sku = "48191001",
                   maxCapacity = 1000
               }
               );

                context.WareHouse.AddRange(
                    new WareHouse
                    {
                        wareHouseId = 1,
                        description = "Baltimore",
                        maxCapacity = 10000
                    },
                   new WareHouse
                   {
                       wareHouseId = 2,
                       description = "Seattle",
                       maxCapacity = 10000
                   },
                   new WareHouse
                   {
                       wareHouseId = 3,
                       description = "Minneapolis",
                       maxCapacity = 10000
                   },
                   new WareHouse
                   {
                       wareHouseId = 4,
                       description = "Washington",
                       maxCapacity = 10000
                   },
                   new WareHouse
                   {
                       wareHouseId = 5,
                       description = "Boston",
                       maxCapacity = 10000
                   }
                    );

                context.Movement.AddRange(
                 new Movement
                 {
                     movementId = 1,
                     typeId = 1,
                     productId = 1,
                     cant = 50

                 },
                 new Movement
                 {
                     movementId = 2,
                     typeId = 1,
                     productId = 1,
                     cant = 23
                 },
                 new Movement
                 {
                     movementId = 3,
                     typeId = 2,
                     productId = 5,
                     cant = 11
                 },
                 new Movement
                 {
                     movementId = 4,
                     typeId = 1,
                     productId = 6,
                     cant = 80
                 },

                 new Movement
                 {
                     movementId = 5,
                     typeId = 2,
                     productId = 1,
                     cant = 3
                 },
                 new Movement
                 {
                     movementId = 6,
                     typeId = 2,
                     productId = 3,
                     cant = 12
                 },
                 new Movement
                 {
                     movementId = 7,
                     typeId = 2,
                     productId = 4,
                     cant = 15
                 },
                 new Movement
                 {
                     movementId = 8,
                     typeId = 1,
                     productId = 3,
                     cant = 200
                 },
                  new Movement
                  {
                      movementId = 9,
                      typeId = 2,
                      productId = 6,
                      cant = 10
                  },
                  new Movement
                  {
                      movementId = 10,
                      typeId = 1,
                      productId = 1,
                      cant = 60
                  }
            );
                context.SaveChanges();
            }
        }
    }
}
