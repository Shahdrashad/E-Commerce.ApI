using AutoMapper;
using E_commerce.Core.Entities;
using E_commerce.Core.Repository;
using E_commerce.Core.Specification;
using E_Commerce.ApI.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ApI.Controllers
{
    
    public class ProductsController : APIBaseController
    {
      
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        #region without unitOfWork
        // private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<ProductType> _typeRepo;
        //private readonly IGenericRepository<ProductBrand> _brandRepo;
        #endregion
        public ProductsController(IMapper mapper,IUnitOfWork unitOfWork)
        {
           
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            #region without unitOfWork
            //_productRepo = productRepo;
            //_typeRepo = TypeRepo;
            //_brandRepo = BrandRepo;
            #endregion

        }



        // baseurl / api/product
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams Params)
        {
            var Spec =new ProductWithBrandAndTypeSpecification(Params);

            var product =await _unitOfWork.Repository<Product>().GetAllWithSpecificationAsync(Spec);
            var MappedProducts=_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(product);
            //OkObjectResult result = new OkObjectResult(product);
            return Ok(MappedProducts);
        }
      

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(id);
            var Product = await _unitOfWork.Repository<Product>().GetByIdWithSpecification(Spec);
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);
            //OkObjectResult result = new OkObjectResult(product);
           
            return Ok(MappedProduct);
        }

        //Get All Types

        [HttpGet("Types")]
        public async Task <ActionResult< IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }




    }
}
