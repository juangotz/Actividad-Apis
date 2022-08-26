using APICODERHOUSE.Controllers.DTOs;
using APICODERHOUSE.Models;
using APICODERHOUSE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APICODERHOUSE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        [HttpGet]
        public static List<Sale> GetSales()
        {
            return SaleHandler.GetSale();
        }
        [HttpPost]
        public bool CreateSale(SaleProduct saleProduct)
        {
            try
            {
                return SaleHandler.CreateSale(new Sale
                {
                    id = saleProduct.id,
                    comment = saleProduct.comment
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
