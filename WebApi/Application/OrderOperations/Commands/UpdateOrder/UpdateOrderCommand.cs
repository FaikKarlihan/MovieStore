using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommand
    {
        private readonly IMovieStoreDbContext _context;
        public UpdateOrderModel Model { get; set; }
        public int OrderId { get; set; }
        public UpdateOrderCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var order = _context.Orders.Find(OrderId);
            if(order is null)
                throw new InvalidOperationException("Order not found");

            order.Customer = !string.IsNullOrWhiteSpace(Model.Customer) ? GetCustomerFromDatabase() : order.Customer;
            order.Movie = !string.IsNullOrWhiteSpace(Model.Movie) ? GetMovieFromDatabase() : order.Movie;
            order.TotalPrice = Model.TotalPrice != default ? Model.TotalPrice : order.TotalPrice;
            order.OrderDate = Model.OrderDate != DateTime.Now ? Model.OrderDate : order.OrderDate;

            _context.SaveChanges();
            
        }
        private Customer GetCustomerFromDatabase()
        {
            var nameParts = Model.Customer.Split(' ');
            if (nameParts.Length < 2)
                throw new InvalidOperationException($"Invalid customer name format: {Model.Customer}. Please provide 'Name Surname'.");
                
            string firstName = nameParts[0];
            string lastName = string.Join(" ", nameParts.Skip(1));

            var customer = _context.Customers
                .SingleOrDefault(a => a.Name.ToLower() == firstName.ToLower() && a.Surname.ToLower() == lastName.ToLower());
            if (customer is null)
                throw new InvalidOperationException($"Customer '{firstName} {lastName}' not found. Please add the customer first.");

            return customer;
        }
        private Movie GetMovieFromDatabase()
        {
            var movie = _context.Movies.SingleOrDefault(x=> x.Name == Model.Movie);
            if(movie is null)
                throw new InvalidOperationException("Movie not found. Make sure you enter the movie name completely and correctly.");
            return movie;
        }
    }
    public class UpdateOrderModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }
    }
}