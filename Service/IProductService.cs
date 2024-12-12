using DTOsTask.DTO;

namespace DTOsTask.Service
{
    public interface IProductService
    {
        ProductOutputDTO AddProduct(ProductInputDTO productInputDTO);
        List<ProductOutputDTO> GetAllProducts(int pageNumber, int pageSize);
        ProductOutputDTO GetProductById(int id);
        ProductOutputDTO UpdateProduct(int id, ProductInputDTO productInputDTO);
    }
}
