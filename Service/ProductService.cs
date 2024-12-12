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
}
