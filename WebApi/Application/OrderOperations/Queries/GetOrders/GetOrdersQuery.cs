using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetOrdersQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GetOrdersViewModel> Handle()
        {
            var orders = _context.Orders
                .Include(x=> x.Customer)
                .Include(x=> x.Movie)
                .OrderBy(x=> x.Id).ToList();
            if(orders.Count<1)
                throw new InvalidOperationException("No orders found");

            List<GetOrdersViewModel> vm = _mapper.Map<List<GetOrdersViewModel>>(orders);

            return vm;
        }
    }
    public class GetOrdersViewModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }
    }
}