using AutoMapper;
using addCard_backend.Context;
using addCard_backend.Models;

namespace addCard_backend.Services
{
    public class CreditCardRepository : Repository<CreditCard>
    {
        public CreditCardRepository(CreditCardContext context, IMapper mapper) : base(context, mapper) { }
    }
}

