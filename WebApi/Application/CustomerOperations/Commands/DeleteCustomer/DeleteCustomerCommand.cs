using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int CustomerId { get; set; }
        public DeleteCustomerCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x=> x.Id==CustomerId);
            if(customer is null)
                throw new InvalidOperationException("Customer not found");
            if(!customer.IsActive)
                throw new InvalidOperationException("Customer is already inactive");

            customer.IsActive=false;
            _context.SaveChanges();
        }
    }
}