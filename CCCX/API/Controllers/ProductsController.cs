using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
        }

        [Cached(600)] //cache for 10 minutes
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts( 
            [FromQuery] ProductSpecParams productParams) // gets products
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams); //creates specification for products
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams); //creates specification for products with filters???

            var totalItems = await _productsRepo.CountAsync(countSpec); //gets total items

            var products = await _productsRepo.ListAsync(spec); //gets products

            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products); //maps products to return dto

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data)); //returns products if successful
        }

        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //returns ok if successful
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] //returns not found if unsuccessful
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id); //creates specification for product with id

            var product = await _productsRepo.GetEntityWithSpec(spec); //gets product with specification

            if (product == null) return NotFound(new ApiResponse(404)); //if product is null return not found

            return _mapper.Map<ProductToReturnDto>(product); //returns product if successful
        }

        [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands() 
        {
            return Ok(await _productBrandRepo.ListAllAsync()); //returns brands if successful
        }

        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync()); //returns types if successful
        }
    }
}
