using addCard_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace addCard_backend.Context
{
    public class CreditCardContext : DbContext
    {
        public DbSet<CreditCard> CreditCard => Set<CreditCard>();

        public CreditCardContext(DbContextOptions<CreditCardContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
