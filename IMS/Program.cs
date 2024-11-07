using IMS.Controller;
using IMS.Data;
using IMS.Presentation;
using IMS.Repository;

namespace IMS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize the DbContext and repositories
            var context = new InventoryContext();
            var productRepository = new ProductRepository(context);
            var supplierRepository = new SupplierRepository(context);
            var transactionRepository = new TransactionRepository(context);

            // Initialize controllers with repositories
            var productController = new ProductController(productRepository);
            var supplierController = new SupplierController(supplierRepository);
            var transactionController = new TransactionController(transactionRepository, productRepository);

            // Initialize and display the presentation layer
            var presentationLayer = new PresentationLayer(productController, supplierController, transactionController);
            presentationLayer.DisplayMainMenu();
        }
    }
}
