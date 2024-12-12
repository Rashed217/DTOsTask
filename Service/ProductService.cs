using DTOsTask.DTO;
using DTOsTask.Model;
using DTOsTask.Repository;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace DTOsTask.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductOutputDTO AddProduct(ProductInputDTO productInputDTO)
        {
            var product = new Product
            {
                Name = productInputDTO.Name,
                Price = productInputDTO.Price,
                Category = productInputDTO.Category,
                DateAdded = DateTime.Now
            };

            _productRepository.AddProduct(product);

            return new ProductOutputDTO
            {
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                DateAdded = product.DateAdded
            };
        }
        public List<ProductOutputDTO> GetAllProducts(int pageNumber, int pageSize)
        {
            var products = _productRepository.GetAllProducts();
            var paginatedProducts = products
                .OrderByDescending(p => p.DateAdded)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductOutputDTO
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.Category,
                    DateAdded = p.DateAdded
                })
                .ToList();

            return paginatedProducts;
        }

        public ProductOutputDTO GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
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
                return null;
            }
        }
    }
}
