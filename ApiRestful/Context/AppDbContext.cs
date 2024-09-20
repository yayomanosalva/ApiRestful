using Microsoft.EntityFrameworkCore;
using ApiRestful.Entities;

namespace ApiRestful.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //Server=localhost;Database=master;Trusted_Connection=True;

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entidad Product
            modelBuilder.Entity<Product>(entityPro =>
            {
                // nombre de la tabla
                entityPro.ToTable("Products");

                // clave primaria
                entityPro.HasKey(p => p.ProductId);

                // Propiedades de la tabla
                entityPro.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entityPro.Property(p => p.Description).HasMaxLength(500);
                entityPro.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");

                // Relación Product - Category (muchos a uno)
                entityPro.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId);
            });

            // Entidad Category
            modelBuilder.Entity<Category>(entityCat =>
            {
                // Nombre de la tabla
                entityCat.ToTable("Categorys");

                // Clave primaria
                entityCat.HasKey(c => c.CategoryId);

                // Propiedades de la tabla
                entityCat.Property(c => c.Name).IsRequired().HasMaxLength(50);

                // Relación Category - Products (uno a muchos)
                entityCat.HasMany(c => c.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId);
            });

            // Entidad User
            modelBuilder.Entity<User>(entityUs =>
            {
                // Nombre de la tabla
                entityUs.ToTable("Users");

                // Clave primaria
                entityUs.HasKey(u => u.UserId);

                // Propiedades de la tabla
                entityUs.Property(u => u.Username).IsRequired().HasMaxLength(100);
                entityUs.Property(u => u.Email).IsRequired().HasMaxLength(150);

                // Indice único para el email
                entityUs.HasIndex(u => u.Email).IsUnique();

                // Relación User - WishlistItem (uno a muchos)
                entityUs.HasMany(u => u.WishlistItems)
                        .WithOne(pd => pd.User)
                        .HasForeignKey(pd => pd.UserId);
            });

            // Entidad WishlistItem
            modelBuilder.Entity<WishlistItem>(entity =>
            {
                // Definir el nombre de la tabla
                entity.ToTable("WishlistItems");

                // Definir la clave primaria
                entity.HasKey(pd => pd.Id);

                // Relación WishlistItem - Usuario (muchos a uno)
                entity.HasOne(pd => pd.User)
                      .WithMany(u => u.WishlistItems)
                      .HasForeignKey(pd => pd.UserId);

                // Relación WishlistItem - Product (muchos a uno)
                entity.HasOne(pd => pd.Product)
                      .WithMany(p => p.WishlistItems)
                      .HasForeignKey(pd => pd.ProductId);
            });
        }

    }
}
