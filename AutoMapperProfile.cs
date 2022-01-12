using AutoMapper;
using ShowBridge.Dtos.Product;
using ShowBridge.Properties.Models;

namespace ShowBridge
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<InventoryProduct, GetProductDto>();
            CreateMap<AddProductDto, InventoryProduct>();
            CreateMap<InventoryProduct, UpdateProductDto>();
        }
    }
}
