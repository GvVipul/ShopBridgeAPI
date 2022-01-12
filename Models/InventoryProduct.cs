

using System;

namespace ShowBridge.Properties.Models
{
    public class InventoryProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SoldBy { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public int CreatedUserID { get; set; }
        public int ModifiedUserID { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}