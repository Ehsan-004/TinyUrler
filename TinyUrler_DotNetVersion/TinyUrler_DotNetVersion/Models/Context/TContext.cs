using Microsoft.EntityFrameworkCore;

namespace TinyUrler_DotNetVersion.Models.Context
{
    public class TContext : DbContext
    {
        public TContext(DbContextOptions<TContext> options) : base(options)
        {

        }
        public DbSet<Link> Links { get; set; }
    }
}