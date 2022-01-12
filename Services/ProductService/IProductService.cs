
using System.Collections.Generic;
using System.Threading.Tasks;
using ShowBridge.Dtos.Product;
namespace ShowBridge.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>>  GetAllProducts();
        Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct);
        Task<ServiceResponse<UpdateProductDto>> UpdateProduct(UpdateProductDto updateProduct);
        Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id);
    }
}