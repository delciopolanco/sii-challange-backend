using AutoMapper;
using POS_service_customers.Context;
using POS_service_customers.Models;

namespace POS_service_customers.Services
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(CustomerDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}

