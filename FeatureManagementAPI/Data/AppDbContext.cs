using FeatureManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FeatureManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Feature> Features { get; set; }
    }
}