using DTOsTask.DTO;
using DTOsTask.Model;
using DTOsTask.Repository;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace DTOsTask.Service
{
    // Service class responsible for business logic related to products
    public class ProductService : IProductService
    {
        // Injected repository that handles CRUD operations on the Product entity in the database
        private readonly IProductRepository _productRepository;

        // Constructor that initializes the ProductService with the injected repository
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Method to add a new product
        public ProductOutputDTO AddProduct(ProductInputDTO productInputDTO)
        {
            // Create a new Product entity from the ProductInputDTO data
            var product = new Product
            {
                Name = productInputDTO.Name,
                Price = productInputDTO.Price,
                Category = productInputDTO.Category,
                DateAdded = DateTime.Now  // Set the current date and time as the date the product was added
            };

            // Call the repository to add the product to the database
            _productRepository.AddProduct(product);

            // Return a ProductOutputDTO containing the newly added product's data
            return new ProductOutputDTO
            {
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                DateAdded = product.DateAdded
            };
        }

        // Method to get all products with pagination (page number and page size)
        public List<ProductOutputDTO> GetAllProducts(int pageNumber, int pageSize)
        {
            // Get all products from the repository
            var products = _productRepository.GetAllProducts();

            // Apply pagination: order by DateAdded, skip records for the current page, and take the specified number of products
            var paginatedProducts = products
                .OrderByDescending(p => p.DateAdded)  // Order products by the date added, in descending order
                .Skip((pageNumber - 1) * pageSize)  // Skip the number of products to get to the requested page
                .Take(pageSize)  // Take the specified number of products for the current page
                .Select(p => new ProductOutputDTO  // Transform each product into a ProductOutputDTO
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.Category,
                    DateAdded = p.DateAdded
                })
                .ToList();  // Execute the query and return the paginated result as a list

            return paginatedProducts;
        }

        // Method to get a specific product by its ID
        public ProductOutputDTO GetProductById(int id)
        {
            // Retrieve the product by ID using the repository
            var product = _productRepository.GetProductById(id);

            // If the product is found, return it as a ProductOutputDTO
            if (product != null)
            {
                return new ProductOutputDTO
                {
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    DateAdded = product.DateAdded
                };
            }
            else
            {
                // If no product is found, return null
                return null;
            }
        }

        // Method to update an existing product by its ID
        public ProductOutputDTO UpdateProduct(int id, ProductInputDTO productInputDTO)
        {
            // Retrieve the product by ID
            var product = _productRepository.GetProductById(id);

            // If the product exists, update its properties and save it using the repository
            if (product != null)
            {
                product.Name = productInputDTO.Name;
                product.Price = productInputDTO.Price;
                product.Category = productInputDTO.Category;

                // Update the product in the database
                _productRepository.UpdateProduct(product);

                // Return the updated product as a ProductOutputDTO
                return new ProductOutputDTO
                {
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    DateAdded = product.DateAdded
                };
            }
            else
            {
                // If no product is found, return null
                return null;
            }
        }

        // Method to delete a product by its ID
        public void DeleteProduct(int id)
        {
            // Call the repository to delete the product by its ID
            _productRepository.DeleteProduct(id);
        }
    }
}
