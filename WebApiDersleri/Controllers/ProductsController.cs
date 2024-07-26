using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http; // HTTP istek ve yanıtlarını yönetmek için gerekli kütüphane.
using Microsoft.AspNetCore.Mvc; // ASP.NET Core MVC için gerekli kütüphane.
using Microsoft.EntityFrameworkCore; // Entity Framework Core ORM için gerekli kütüphane.
using WebApiDersleri.DTO;
using WebApiDersleri.Models; // Projeye özgü modelleri içeren kütüphane.

namespace WebApiDersleri.Controllers // Controllers namespace'i içinde çalıştığımızı belirtir.
{
	// localhost:5000/api/products
	[ApiController] // Bu attribute, bu sınıfın bir API controller olduğunu belirtir.
	[Route("api/[controller]")] // Route attribute'u, bu controller'ın temel yolunu ayarlar. 
								// [controller] kısmı, controller'ın adı ile otomatik olarak değiştirilir (bu durumda "Products").
	public class ProductsController : ControllerBase 
	// ProductsController sınıfı, API controller özelliklerini kazanmak için ControllerBase sınıfından türetilir.
	{
		private readonly ProductsContext _context; // Veritabanı işlemleri için ProductsContext nesnesi.

		// Constructor (Yapıcı Metod)
		public ProductsController(ProductsContext context)
		{
			_context = context; // Dependency Injection ile gelen context'i private değişkene atar.
		}

		// localhost:5000/api/products => GET
		[HttpGet] // HTTP GET isteklerini işleyen metod.
		public async Task<IActionResult> GetProducts()
		{
			var products = await _context.Products
										 .Where(i => i.IsActive)
										 .Select(p => ProductToDTO(p))
										 .ToListAsync(); // Tüm ürünleri veritabanından asenkron olarak getirir.

			return Ok(products); // Ürün listesi döndürülür.
		}

		// localhost:5000/api/products/1 => GET
		[HttpGet("{id}")] // HTTP GET isteklerini belirli bir id ile işleyen metod.
		[Authorize]
		public async Task<IActionResult> GetProduct(int? id)
		{
			if (id == null) // ID boşsa, Not Found (bulunamadı) döndürülür.
			{
				return NotFound();
			}

			// ID'ye göre ürünü veritabanından bul ve DTO'ya dönüştür.
			var product = await _context.Products
										.Where(p => p.ProductId == id)
										.Select(p => ProductToDTO(p))
										.FirstOrDefaultAsync();

			if (product == null) // Eğer ürün bulunamazsa, Not Found döndürülür.
			{
				return NotFound();
			}

			return Ok(product); // Ürün bulunursa, OK (200) döndürülür ve ürün bilgisi gönderilir.
		}

		[HttpPost] // HTTP POST isteklerini işleyen metod.
		public async Task<IActionResult> CreateProduct(Product entity)
		{
			_context.Products.Add(entity); // Yeni ürünü veritabanına ekler.
			await _context.SaveChangesAsync(); // Değişiklikleri kaydeder.

			return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity); // Yeni oluşturulan ürünü döndürür.
		}

		[HttpPut("{id}")] // HTTP PUT isteklerini belirli bir id ile işleyen metod.
		public async Task<IActionResult> UpdateProduct(int id, Product entity)
		{
			if (id != entity.ProductId) // Güncellemek istenen ürünün ID'si uyuşmuyorsa, BadRequest döndürülür.
			{
				return BadRequest();
			}

			var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id); // Belirtilen ID'ye sahip ürünü bulur.

			if (product == null) // Ürün bulunamazsa, Not Found döndürülür.
			{
				return NotFound();
			}

			// Ürün bilgilerini günceller.
			product.ProductName = entity.ProductName;
			product.Price = entity.Price;
			product.IsActive = entity.IsActive;

			try
			{
				await _context.SaveChangesAsync(); // Değişiklikleri kaydeder.
			}
			catch (Exception) // Hata oluşursa, Not Found döndürülür.
			{
				return NotFound();
			}

			return NoContent(); // Başarılı olursa, içerik yok anlamına gelen NoContent döndürülür.
		}

		[HttpDelete("{id}")] // HTTP DELETE isteklerini belirli bir id ile işleyen metod.
		public async Task<IActionResult> DeleteProduct(int? id)
		{
			if (id == null) // ID boşsa, Not Found döndürülür.
			{
				return NotFound();
			}

			var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id); // Belirtilen ID'ye sahip ürünü bulur.

			if (product == null) // Ürün bulunamazsa, Not Found döndürülür.
			{
				return NotFound();
			}

			_context.Products.Remove(product); // Ürünü veritabanından siler.

			try
			{
				await _context.SaveChangesAsync(); // Değişiklikleri kaydeder.
			}
			catch (Exception) // Hata oluşursa, Not Found döndürülür.
			{
				return NotFound();
			}

			return NoContent(); // Başarılı olursa, içerik yok anlamına gelen NoContent döndürülür.
		}

		private static ProductDTO ProductToDTO(Product p)
		{
			return new ProductDTO
			{
				ProductId = p.ProductId,
				ProductName = p.ProductName,
				Price = p.Price
			};
		}
	}
}
