using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ShowBridge.Services.ProductService;
using System.Threading.Tasks;
using ShowBridge.Dtos.Product;
using ShowBridge.Attribute;
using Microsoft.AspNetCore.Authorization;

namespace ShowBridge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [TypeFilter(typeof(APIPermission))]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductService _prodService;
        public ProductController(IProductService prodService)
        {
            _prodService = prodService;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(await _prodService.GetAllProducts());
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> AddProduct(AddProductDto newProduct)
        {
            return Ok(await _prodService.AddProduct(newProduct));
        }

        [HttpPut("UpdateProductDetails")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto newProduct)
        {
            return Ok(await _prodService.UpdateProduct(newProduct));
        }

        [HttpDelete("RemoveProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = await _prodService.DeleteProduct(id);
            return Ok(serviceResponse);
        }
    }
}