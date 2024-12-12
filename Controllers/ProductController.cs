using DTOsTask.DTO;
using DTOsTask.Service;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;

namespace DTOsTask.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }
    }

    [HttpPost]
    public ActionResult<ProductOutputDto> AddProduct(ProductInputDto inputDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdProduct = _service.CreateProduct(inputDto);
        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Name }, createdProduct);
    }

}
