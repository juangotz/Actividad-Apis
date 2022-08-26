using APICODERHOUSE.Controllers.DTOs;
using APICODERHOUSE.Models;
using APICODERHOUSE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APICODERHOUSE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public static List<Product> GetProductos()
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
    }
}
