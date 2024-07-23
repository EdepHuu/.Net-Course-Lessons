using Microsoft.AspNetCore.Mvc;

namespace ProducstsApi.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private static readonly string[] Products = {
            "Product 1", "Product 2", "Product 3"
        };
        // localhost:5000/api/products => GET
        [HttpGet]
        public string[] GetProducts()
        {
            return Products;
        }

        // localhost:5000/api/products/1=> GET
        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return Products[id];
        }
    }
}