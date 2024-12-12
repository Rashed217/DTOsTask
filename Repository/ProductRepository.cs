using DTOsTask.DTO;
using DTOsTask.Model;
using Microsoft.EntityFrameworkCore;

namespace DTOsTask.Repository
{
    // Repository class that interacts with the database for CRUD operations related to products
    public class ProductRepository : IProductRepository
    {
        // Instance of ApplicationDbContext used to interact with the database
        private readonly ApplicationDbContext _context;

        // Constructor that injects the ApplicationDbContext instance into the repository
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to add a new product to the database
        public void AddProduct(Product product)
        {
            // Add the new product entity to the Products DbSet (in-memory representation of the Products table)
            _context.Products.Add(product);

            // Commit the changes to the database by saving them
            _context.SaveChanges();
        }

        // Method to retrieve all products from the database
        public List<Product> GetAllProducts()
        {
            // Retrieve and return all products as a list
            return _context.Products.ToList();
        }

        // Method to retrieve a product by its ID from the database
        public Product GetProductById(int id)
        {
            // Use the Find method to retrieve a product by its primary key (ID)
            return _context.Products.Find(id);
        }

        // Method to update an existing product in the database
        public void UpdateProduct(Product product)
        {
            // Mark the product entity as modified in the DbContext (this triggers an update when SaveChanges is called)
            _context.Products.Update(product);

            // Commit the changes to the database
            _context.SaveChanges();
        }

        // Method to delete a product from the database by its ID
        public void DeleteProduct(int id)
        {
            // Retrieve the product by its ID
            var product = GetProductById(id);

            // If the product exists, remove it from the DbSet (which also removes it from the database upon SaveChanges)
            if (product != null)
            {
                _context.Products.Remove(product);

                // Commit the changes to the database
                _context.SaveChanges();
            }
        }
    }
}
