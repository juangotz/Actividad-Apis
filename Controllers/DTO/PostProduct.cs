namespace Api.Controllers.DTO
{
    public class PostProduct
    {
        public string description { get; set; }
        public int stock { get; set; }
        public int price { get; set; }
        public int priceSell { get; set; }
        public int idUser { get; set; }
    }
}
