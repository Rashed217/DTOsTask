using DTOsTask.DTO;
using DTOsTask.Model;
using DTOsTask.Repository;

namespace DTOsTask.Service
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
    }

    public ProductOutputDto CreateProduct(ProductInputDto inputDto)
    {
        var product = new Product
        {
            Name = inputDto.Name,
            Price = inputDto.Price,
            Category = inputDto.Category,
            DateAdded = DateTime.Now
        };

        _repository.AddProduct(product);

        return new ProductOutputDto
        {
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            DateAdded = product.DateAdded
        };
    }
}
