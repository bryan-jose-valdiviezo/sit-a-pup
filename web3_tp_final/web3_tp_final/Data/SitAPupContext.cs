using Microsoft.EntityFrameworkCore;
using web3_tp_final.Models;

namespace web3_tp_final.Data
{
    public class SitAPupContext : DbContext
    {
        public SitAPupContext(DbContextOptions<SitAPupContext> options)
                    : base(options)
        {
        }

        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
