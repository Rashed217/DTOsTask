using DTOsTask.DTO;
using DTOsTask.Service;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;

namespace DTOsTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductInputDTO productInputDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productOutputDTO = _productService.AddProduct(productInputDTO);

            return CreatedAtAction(nameof(GetProductById), new { id = productOutputDTO }, productOutputDTO);
        }

        [HttpGet]
        public IActionResult GetAllProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var products = _productService.GetAllProducts(pageNumber, pageSize);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var productOutputDTO = _productService.GetProductById(id);

            if (productOutputDTO == null)
            {
                return NotFound();
            }

            return Ok(productOutputDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductInputDTO productInputDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productOutputDTO = _productService.UpdateProduct(id, productInputDTO);

            if (productOutputDTO == null)
            {
                return NotFound();
            }

            return Ok(productOutputDTO);
        }
    }
}
