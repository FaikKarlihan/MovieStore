using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Orders
    {
        public static void AddOrders(this MovieStoreDbContext context)
        {
            var movie1 = context.Movies.SingleOrDefault(x => x.Id == 1);
            context.Orders.AddRange
            (
                new Order
                {
                    OrderDate = DateTime.Now,
                    TotalPrice = movie1.Price,
                    CustomerId = 1,
                    MovieId = movie1.Id 
                }
            );
        }
    }
}