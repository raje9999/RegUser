using Microsoft.EntityFrameworkCore;
using RegUser.Model;
using System.Collections.Generic;

namespace RegUser.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AccountDetails> UserDetails { get; set; }
    }
}
