using DTOsTask.DTO;

namespace DTOsTask.Service
{
    public interface IProductService
    {
        List<ProductOutputDTO> GetAllProducts(int pageNumber, int pageSize);
        ProductOutputDTO GetProductById(int id);
        ProductOutputDTO AddProduct(ProductInputDTO productInputDTO);
    }
}
