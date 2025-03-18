using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int OrderId { get; set; }
        public GetOrderDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GetOrderDetailViewModel Handle()
        {
            var order = _context.Orders
                .Include(x=> x.Movie)
                .Include(x=> x.Customer)
                .SingleOrDefault(x=> x.Id == OrderId);
            
            if(order is null)
                throw new InvalidOperationException("Order not found");

            GetOrderDetailViewModel vm = _mapper.Map<GetOrderDetailViewModel>(order);

            return vm;
        }
    }
    public class GetOrderDetailViewModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }
    }
}