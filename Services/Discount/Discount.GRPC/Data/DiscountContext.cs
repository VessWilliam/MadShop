using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupons> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupons>().HasData(
             new
             {
                 Id = 1,
                 ProductName = "IPhone X",
                 Description = "IPhone Discount",
                 Amount = 150
             },
             new
             {
                 Id = 2,
                 ProductName = "Samsung S10",
                 Description = "Samsung Discount",
                 Amount = 100
             }
          );

    }

}
