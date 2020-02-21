using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Abc.Facade.Quantity;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Abc.Soft.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Abc.Facade.Quantity.MeasureView> Measures{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MeasureView>().ToTable(nameof(Measures));
        }
    }
}
