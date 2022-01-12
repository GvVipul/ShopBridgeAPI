

using System.ComponentModel.DataAnnotations;

namespace ShowBridge.Dtos.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SoldBy { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public int ModifiedUserID { get; set; }
    }
}
