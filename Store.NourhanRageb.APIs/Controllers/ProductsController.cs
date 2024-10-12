using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.NourhanRageb.APIs.Error;
using Store.NourhanRageb.Domain.Dtos.Products;
using Store.NourhanRageb.Domain.Services.Contract;
using Store.NourhanRageb.Domain.Specifications.Products;
using Microsoft.AspNetCore.Http;
using Store.NourhanRageb.Domain.Dtos;
using Store.NourhanRageb.Domain.Entities;
using Store.NourhanRageb.Domain.Helper;
using Store.NourhanRageb.Domain.Repositories.Contract;

namespace Store.NourhanRageb.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet] // Get / BaseUrl/Products?sort

        // sort --> (Name , priceAsc , PriceDesc)
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec) // endpoint
        {
            var result = await _productServices.GetAllProductsAsync(productSpec);
            return Ok(result);
        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")] // Get / BaseUrl/Products/brands
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands() // endpoint
        {
            var result = await _productServices.GetAllBrandsAsync();
            return Ok(result);
        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")] // Get / BaseUrl/Products/types
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes() // endpoint
        {
            var result = await _productServices.GetAllTypesAsync();
            return Ok(result);
        }

        [ProducesResponseType(typeof(TypeBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("id")] // Get / BaseUrl/Products/id
        public async Task<IActionResult> GetProductById(int? id) // endpoint
        {
            if (id is null)
            {
                return BadRequest(new ApiErrorResponse(400));
            }
            var result = await _productServices.GetProudctById(id.Value);

            if (result is null)
            {
                return NotFound(new ApiErrorResponse(404, $"The Product With Id : {id} not Found at Data Base :("));
            }

            return Ok(result);
        }
    }
}
