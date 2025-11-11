using asp_base.Models.test;
using Microsoft.EntityFrameworkCore;

namespace asp_base.Models.test
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options) // 將 options 傳給 DbContext
        {
        }

        public DbSet<product> Products { get; set; }
        public DbSet<category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder model_builder)
        {
            //model_builder.Entity<product>()
            //    .HasOne(p => p.category)              // 一個 Product 有一個 Category
            //    .WithMany(c => c.products)            // 一個 Category 有多個 Products
            //    .HasForeignKey(p => p.category_id)     // 外鍵欄位
            //    .OnDelete(DeleteBehavior.Cascade);    // 刪除類別時，連帶刪產品（可選）

            base.OnModelCreating(model_builder);
        }
    }
}


