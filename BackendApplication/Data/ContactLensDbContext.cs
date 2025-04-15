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
        public DbSet<ContactLensType> ContactLenses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.DeliveryAddress);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<User>()
                .OwnsMany(u => u.Addresses);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.ContactLensType)
                .WithMany(cl => cl.OrderItems)
                .HasForeignKey(oi => oi.ContactLensTypeId);

        }
    }
}
