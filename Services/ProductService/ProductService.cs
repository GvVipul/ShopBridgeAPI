using System.Collections.Generic;
using ShowBridge.Properties.Models;
using System.Linq;
using System.Threading.Tasks;
using ShowBridge.Dtos.Product;
using AutoMapper;
using ShowBridge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using ShowBridge.Services.CommonService;
using System.IO;

namespace ShowBridge.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly string _errorLogPath = Path.Combine(Environment.CurrentDirectory, @"Logs\\ErrorLog"+
            DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContext;

        public ProductService(IMapper mapper,DataContext context, IHttpContextAccessor httpContext){
            _mapper = mapper;
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                InventoryProduct product = _mapper.Map<InventoryProduct>(newProduct);
                product.createdDate = DateTime.Now;
                product.modifiedDate = DateTime.Now;
                await _context.ShowBridge_InventoryProduct.AddAsync(product);
                await _context.SaveChangesAsync();
                serviceResponse.Message = "Product details successfully saved";
                serviceResponse.ResponseCode = 200;
            }
            catch(Exception Ex)
            {
                ErrorLog.LogErrorToFile(Ex, "ProductController", "CreateProduct", _errorLogPath);
                serviceResponse.Success = false;
                serviceResponse.Message = Ex.Message;
                serviceResponse.ResponseCode = 500;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {                
                List<InventoryProduct> dbProduct = await _context.ShowBridge_InventoryProduct.ToListAsync();
                serviceResponse.Data = (dbProduct.Select(c => _mapper.Map<GetProductDto>(c))).ToList();
                serviceResponse.ResponseCode = 200;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogErrorToFile(Ex, "ProductController", "GetAllProduct", _errorLogPath);
                serviceResponse.Success = false;
                serviceResponse.Message = Ex.Message;
                serviceResponse.ResponseCode = 500;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<UpdateProductDto>> UpdateProduct(UpdateProductDto updateProduct)
        {
            ServiceResponse<UpdateProductDto> serviceResponse = new ServiceResponse<UpdateProductDto>();
            try
            {
                InventoryProduct updateProd = await _context.ShowBridge_InventoryProduct.FirstOrDefaultAsync(c => c.Id == updateProduct.Id);
                if(updateProd != null)
                {
                    updateProd.Name = updateProduct.Name;
                    updateProd.Description = updateProduct.Description;
                    updateProd.SoldBy = updateProduct.SoldBy;
                    updateProd.Category = updateProduct.Category;
                    updateProd.Brand = updateProduct.Brand;
                    updateProd.Price = updateProduct.Price;
                    updateProd.ModifiedUserID = updateProduct.ModifiedUserID;
                    updateProd.modifiedDate = DateTime.Now;

                    _context.ShowBridge_InventoryProduct.Update(updateProd);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<UpdateProductDto>(updateProd);
                }
                else
                {
                    serviceResponse.Message = "Product not found in Inventory";
                    serviceResponse.Success = false;
                }

            }

            catch (Exception Ex)
            {
                ErrorLog.LogErrorToFile(Ex, "ProductController", "UpdateProductDetails", _errorLogPath);
                serviceResponse.Success = false;
                serviceResponse.Message = Ex.Message;
                serviceResponse.ResponseCode = 500;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                InventoryProduct product = await _context.ShowBridge_InventoryProduct.FirstOrDefaultAsync(c => c.Id == id);
                if (product != null)
                {
                    _context.ShowBridge_InventoryProduct.Remove(product);
                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Product Successfully Deleted";
                    serviceResponse.ResponseCode = 200;
                }
                else
                {
                    serviceResponse.Message = "Product not found in Inventory";
                    serviceResponse.Success = false;
                }
            }
            catch (Exception Ex)
            {
                ErrorLog.LogErrorToFile(Ex, "ProductController", "RemoveProduct", _errorLogPath);
                serviceResponse.Success = false;
                serviceResponse.Message = Ex.Message;
                serviceResponse.ResponseCode = 500;
            }
            
            return serviceResponse;
        }
    }
}