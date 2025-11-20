using Microsoft.EntityFrameworkCore;
using ClaimsManagementApp.Models;

namespace ClaimsManagementApp.Data
{
    public class ClaimsDbContext : DbContext
    {
        public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options) : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }

        // public DbSet<HRDashboardViewModel> HRModels { get; set; }  // remove this if it's just a ViewModel

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<SupportingDocument> SupportingDocuments { get; set; }
    }
}
