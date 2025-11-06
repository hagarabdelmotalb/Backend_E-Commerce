using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;
using Shared.ErrorModels;

namespace Presentation.Controllers
{
    public class ProductController(IServiceManager _serviceManager) : ApiController
    {
        //get all products
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationParameters parameters)
            => Ok(await _serviceManager.ProductService.GetAllProductsAsync(parameters));

        [ProducesResponseType(typeof(ProductResultDto),StatusCodes.Status200OK)]

        //get product by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));

        //get product brand
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());

        //get product types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());
    }
}
