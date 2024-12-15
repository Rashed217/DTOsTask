using DTOsTask.DTO;
using DTOsTask.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DTOsTask.Controllers
{
    
    [Authorize] // This attribute ensures that only authenticated users can access the controller or action method. 
                // If a user is not authenticated, the framework will return a 401 Unauthorized response.
    [ApiController] // Marks the class as an API controller and defines the route pattern for the controller's actions
    [Route("api/[controller]")]  // The route will be prefixed with "api/Products" based on the controller name "Products"
    public class ProductsController : ControllerBase
    {
        // Injected service for handling product-related business logic
        private readonly IProductService _productService;

        // Constructor to initialize the ProductsController with a service to interact with product data
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Action to add a new product
        [HttpPost]  // Maps HTTP POST requests to this method
        public IActionResult AddProduct([FromBody] ProductInputDTO productInputDTO)
        {
            // Check if the input data is valid, as per model validation rules
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return a 400 Bad Request if the model state is invalid
            }

            // Call the service to add the product and return the output DTO (data transfer object)
            var productOutputDTO = _productService.AddProduct(productInputDTO);

            // Return a 201 Created response, including the location of the newly created product
            // Uses CreatedAtAction to provide the URL for the "GetProductById" action
            return CreatedAtAction(nameof(GetProductById), new { id = productOutputDTO }, productOutputDTO);
        }

        // Action to retrieve all products with optional pagination parameters
        [HttpGet]  // Maps HTTP GET requests to this method
        public IActionResult GetAllProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // Call the service to get products with the specified pagination settings
            var products = _productService.GetAllProducts(pageNumber, pageSize);

            // Return a 200 OK response with the list of products
            return Ok(products);
        }

        // Action to retrieve a specific product by its ID
        [HttpGet("{id}")]  // Maps HTTP GET requests with an ID parameter to this method
        public IActionResult GetProductById(int id)
        {
            // Call the service to get a product by its ID
            var productOutputDTO = _productService.GetProductById(id);

            // If no product is found, return a 404 Not Found response
            if (productOutputDTO == null)
            {
                return NotFound();
            }

            // Return a 200 OK response with the product data
            return Ok(productOutputDTO);
        }

        // Action to update an existing product by its ID
        [HttpPut("{id}")]  // Maps HTTP PUT requests with an ID parameter to this method
        public IActionResult UpdateProduct(int id, [FromBody] ProductInputDTO productInputDTO)
        {
            // Check if the input data is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return a 400 Bad Request if the model state is invalid
            }

            // Call the service to update the product and return the output DTO
            var productOutputDTO = _productService.UpdateProduct(id, productInputDTO);

            // If the product with the specified ID does not exist, return a 404 Not Found response
            if (productOutputDTO == null)
            {
                return NotFound();
            }

            // Return a 200 OK response with the updated product data
            return Ok(productOutputDTO);
        }

        // Action to delete a product by its ID
        [HttpDelete("{id}")]  // Maps HTTP DELETE requests with an ID parameter to this method
        public IActionResult DeleteProduct(int id)
        {
            // Call the service to delete the product
            _productService.DeleteProduct(id);

            // Return a 204 No Content response indicating that the operation was successful but there is no content to return
            return NoContent();
        }
    }
}
