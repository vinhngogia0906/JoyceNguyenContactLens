using BackendApplication.Schema.Types;
using Microsoft.EntityFrameworkCore;

namespace BackendApplication.Data
{
    public class ContactLensDbContext : DbContext
    {
        public ContactLensDbContext(DbContextOptions<ContactLensDbContext> options)
            : base(options)
        {
        }
        public DbSet<ContactLensType> ContactLenses { get; set; } = null!;
       
    }
}
