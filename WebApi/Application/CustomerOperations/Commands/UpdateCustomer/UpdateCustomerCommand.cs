using System;
using WebApi.DBOperations;

namespace WebApi.Applications.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand
    {
        private readonly IMovieStoreDbContext _context;
        public UpdateCustomerModel Model { get; set; }
        public int CustomerId { get; set; }
        public UpdateCustomerCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var customer = _context.Customers.Find(CustomerId);
            if(customer is null)
                throw new InvalidOperationException("Customer not found");

            customer.Name = !string.IsNullOrWhiteSpace(Model.Name) ? Model.Name : customer.Name;
            customer.Surname = !string.IsNullOrWhiteSpace(Model.Surname) ? Model.Surname : customer.Surname;  
            customer.Email = !string.IsNullOrWhiteSpace(Model.Email) ? Model.Email : customer.Email;
            customer.Password = !string.IsNullOrWhiteSpace(Model.Password) ? Model.Password : customer.Password;
            customer.IsActive = Model.IsActive != default ? Model.IsActive : customer.IsActive;

            _context.SaveChanges();
        }
    }
    public class UpdateCustomerModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}