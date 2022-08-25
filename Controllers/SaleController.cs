using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Controllers.DTO;
using Api.Repository;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        [HttpPost]
        public static bool CreateSale(SaleProduct saleProduct)
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