using eshop.Models.Comments;
using eshop.Models.Database.Configuration;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public class EshopDBContext : IdentityDbContext<User, Role, int>
    {
        public EshopDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.ApplyConfigurations(modelBuilder);
            this.ApplyConfigurationsForAll(modelBuilder);

            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.Relational().TableName.Replace("AspNet", String.Empty);
            }

            int stringLength = 256;
            //modelBuilder.Entity<User>(entity => entity.Property(m => m.Id).HasMaxLength(stringLength));
            modelBuilder.Entity<User>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(stringLength));
            modelBuilder.Entity<User>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(stringLength));

            //modelBuilder.Entity<Role>(entity => entity.Property(m => m.Id).HasMaxLength(stringLength));
            modelBuilder.Entity<Role>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(stringLength));
            
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(stringLength));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(stringLength));
            //modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.UserId).HasMaxLength(stringLength));
            //modelBuilder.Entity<IdentityUserRole<int>>(entity => entity.Property(m => m.UserId).HasMaxLength(stringLength));

            //modelBuilder.Entity<IdentityUserRole<int>>(entity => entity.Property(m => m.RoleId).HasMaxLength(stringLength));

            //modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.UserId).HasMaxLength(stringLength));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(stringLength));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.Name).HasMaxLength(stringLength));

            //modelBuilder.Entity<IdentityUserClaim<int>>(entity => entity.Property(m => m.Id).HasMaxLength(stringLength));
            //modelBuilder.Entity<IdentityUserClaim<int>>(entity => entity.Property(m => m.UserId).HasMaxLength(stringLength));
            //modelBuilder.Entity<IdentityRoleClaim<int>>(entity => entity.Property(m => m.Id).HasMaxLength(stringLength));
            //modelBuilder.Entity<IdentityRoleClaim<int>>(entity => entity.Property(m => m.RoleId).HasMaxLength(stringLength));
        }

        protected virtual void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CarouselConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new MainCommentConfiguration());
            modelBuilder.ApplyConfiguration(new SubCommentConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
        protected void ApplyConfigurationsForAll(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
