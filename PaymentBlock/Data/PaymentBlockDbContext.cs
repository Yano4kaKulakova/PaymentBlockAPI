using Microsoft.EntityFrameworkCore;
using PaymentBlockAPI.Models;

namespace PaymentBlockAPI.Data
{
    public class PaymentBlockDbContext : DbContext
    {
        public PaymentBlockDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Block> Blocks { get; set; }
    }
}
