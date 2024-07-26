using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; // EF Core işlevlerini kullanmak için gerekli kütüphane
using System.Runtime.CompilerServices;

namespace WebApiDersleri.Models // Web API dersleri için model tanımlamalarının olduğu namespace
{
	// ProductsContext sınıfı, veritabanı işlemlerini yönetir ve DbContext sınıfından türetilmiştir.
	public class ProductsContext : IdentityDbContext<AppUsers ,AppRoles, int>
	{
		// Constructor (Yapıcı Metod), DbContextOptions parametresini alır ve base sınıfa gönderir.
		public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
		{
		}

		// Veritabanı modellerinin yapılandırıldığı metod.
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Temel OnModelCreating metodunu çağırır.
			base.OnModelCreating(modelBuilder);

			// Ürünlerin veritabanına başlangıç verisi olarak eklenmesini sağlar. Veri Tabanındaki değişiklikler kodu etkilemez!!!!!
			modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, ProductName = "Iphone 11", Price = 10000, IsActive = true });
			modelBuilder.Entity<Product>().HasData(new Product { ProductId = 2, ProductName = "Iphone 12", Price = 20000, IsActive = true });
			modelBuilder.Entity<Product>().HasData(new Product { ProductId = 3, ProductName = "Iphone 13", Price = 30000, IsActive = true });
			modelBuilder.Entity<Product>().HasData(new Product { ProductId = 4, ProductName = "Iphone 14", Price = 40000, IsActive = true });
			modelBuilder.Entity<Product>().HasData(new Product { ProductId = 5, ProductName = "Iphone 15", Price = 50000, IsActive = true });
		}

		// Products adında bir DbSet tanımlanır, bu DbSet Product varlıklarını temsil eder ve CRUD işlemleri için kullanılır.
		public DbSet<Product> Products { get; set; }
	}
}