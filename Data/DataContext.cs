using Microsoft.EntityFrameworkCore;
using ShowBridge.Properties.Models;
namespace ShowBridge.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        
        public DbSet<InventoryProduct> ShowBridge_InventoryProduct { get; set;}
    }
}