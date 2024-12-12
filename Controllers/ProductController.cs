using DTOsTask.Service;
using Microsoft.AspNetCore.Mvc;

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

}
