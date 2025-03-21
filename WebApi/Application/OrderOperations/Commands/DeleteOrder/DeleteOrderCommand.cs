using System;
using WebApi.DBOperations;

namespace WebApi.Applications.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int OrderId { get; set; }
        public DeleteOrderCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var order = _context.Orders.Find(OrderId);
            if(order is null)
                throw new InvalidOperationException("Order not found");

            _context.Orders.Remove(order);
            _context.SaveChanges();    
        }
    }
}