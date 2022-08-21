using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Model;
using Api.Controllers.DTO;
using Api.Repository;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public List<Product> GetProductos()
        {
            return ProductHandler.GetProductos();
        }

        [HttpDelete]
        public bool DeleteProduct([FromBody] int id)
        {
            try
            {
                return ProductHandler.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpPut]
        public bool UpdateDescription([FromBody] PutProduct putProduct)
        {
            try
            {
                return ProductHandler.UpdateDescription(new Product()
                {
                    id = putProduct.id,
                    description = putProduct.description
                });            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpPost]
        public bool CreateProduct([FromBody] PostProduct postProduct)
        {
            try
            {
                return ProductHandler.CreateProduct(new Product()
                {
                    description = postProduct.description,
                    price = postProduct.price,
                    stock = postProduct.stock,
                    priceSell = postProduct.priceSell,

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpPost]
        [HttpPut]
        public static bool LoadSale(SaleProduct saleProduct)
        {
            try
            {
                return ProductHandler.CreateSoldProduct(saleProduct.comment) & ProductHandler.UpdateStock(new Product
                {
                    id = saleProduct.id
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}