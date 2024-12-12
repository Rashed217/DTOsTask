using DTOsTask.Model;

namespace DTOsTask.Repository
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void UpdateProduct(Product product);
    }
}