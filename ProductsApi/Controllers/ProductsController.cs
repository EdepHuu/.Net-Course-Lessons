using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDersleri.Models;

namespace WebApiDersleri.Controllers 
{
	// localhost:5000/api/products
	[ApiController] // Bu attribute, bu sınıfın bir API controller olduğunu belirtir.
	[Route("api/[controller]")] // Route attribute'u, bu controller'ın temel yolunu ayarlar. [controller] kısmı, controller'ın adı ile otomatik olarak değiştirilir (bu durumda "Products").
	public class ProductsController : ControllerBase
    // ProductsController sınıfı, API controller özelliklerini kazanmak için ControllerBase sınıfından türetilir.
	{
		public static List<Product>? _products; // Ürünleri tutmak için statik bir liste tanımlıyoruz.
		
		// Constructor (Yapıcı Metod)
		public ProductsController()
		{
			// Ürünleri başlangıçta oluşturup listeye ekliyoruz.
			_products = new List<Product>
			{
				new Product { ProductId = 1, ProductName = "Iphone 12", Price = 20000, IsActive = true },
				new Product { ProductId = 2, ProductName = "Iphone 13", Price = 30000, IsActive = true },
				new Product { ProductId = 3, ProductName = "Iphone 14", Price = 40000, IsActive = true },
				new Product { ProductId = 4, ProductName = "Iphone 15", Price = 50000, IsActive = true }
			};
		}

		// localhost:5000/api/products => GET
		[HttpGet] // HTTP GET isteklerini işleyen metod.
		public IActionResult GetProducts()
		{
			if (_products == null) // Ürün listesi boşsa, kötü istek (Bad Request) döndürülür.
			{
				return BadRequest();
			}

			return Ok(_products); // Ürün listesi döndürülür.
		}

		// localhost:5000/api/products/1 => GET
		[HttpGet("{id}")] // HTTP GET isteklerini belirli bir id ile işleyen metod.
		public IActionResult GetProduct(int? id) 
		{
			if (id == null) // ID boşsa, Not Found (bulunamadı) döndürülür.
			{
				return NotFound();
			}

			// ID'ye göre ilk eşleşen ürünü bul.
			var p = _products?.FirstOrDefault(i => i.ProductId == id);

			if (p == null) // Eğer ürün bulunamazsa, Not Found döndürülür.
			{
				return NotFound();
			}

			return Ok(p); // Ürün bulunursa, OK (200) döndürülür ve ürün bilgisi gönderilir.
		}
	}
}
