using IMS.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Presentation
{
    public class PresentationLayer
    {
        private readonly ProductController _productController;
        private readonly SupplierController _supplierController;
        private readonly TransactionController _transactionController;

        public PresentationLayer(ProductController productController, SupplierController supplierController, TransactionController transactionController)
        {
            _productController = productController;
            _supplierController = supplierController;
            _transactionController = transactionController;
        }

        public void DisplayMainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("===== Inventory Management System =====");
                Console.WriteLine("1. Manage Products");
                Console.WriteLine("2. Manage Suppliers");
                Console.WriteLine("3. Manage Transactions");
                Console.WriteLine("4. Generate Inventory Report");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayProductMenu();
                        break;
                    case "2":
                        DisplaySupplierMenu();
                        break;
                    case "3":
                        DisplayTransactionMenu();
                        break;
                    case "4":
                        GenerateInventoryReport();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Exiting Inventory Management System.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void DisplayProductMenu()
        {
            Console.WriteLine("===== Product Management =====");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. View Product Details");
            Console.WriteLine("5. View All Products");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    UpdateProduct();
                    break;
                case "3":
                    DeleteProduct();
                    break;
                case "4":
                    ViewProductDetails();
                    break;
                case "5":
                    ViewAllProducts();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void AddProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter description: ");
            string description = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Enter price: ");
            double price = double.Parse(Console.ReadLine());
            Console.Write("Enter inventory ID: ");
            int inventoryId = int.Parse(Console.ReadLine());

            _productController.AddProduct(name, description, quantity, price, inventoryId);
        }

        private void UpdateProduct()
        {
            Console.Write("Enter product ID to update: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Enter new name: ");
            string newName = Console.ReadLine();
            Console.Write("Enter new description: ");
            string newDescription = Console.ReadLine();
            Console.Write("Enter new Price");
            int newPrice = int.Parse(Console.ReadLine());

            _productController.UpdateProduct(productId, newName, newDescription, newPrice);
        }

        private void DeleteProduct()
        {
            Console.Write("Enter product ID to delete: ");
            int productId = int.Parse(Console.ReadLine());
            _productController.DeleteProduct(productId);
        }

        private void ViewProductDetails()
        {
            Console.Write("Enter product ID to view details: ");
            int productId = int.Parse(Console.ReadLine());
            _productController.ViewProductDetails(productId);
        }

        private void ViewAllProducts()
        {
            _productController.ViewAllProducts();
        }

        private void DisplaySupplierMenu()
        {
            Console.WriteLine("===== Supplier Management =====");
            Console.WriteLine("1. Add Supplier");
            Console.WriteLine("2. Update Supplier");
            Console.WriteLine("3. Delete Supplier");
            Console.WriteLine("4. View Supplier Details");
            Console.WriteLine("5. View All Suppliers");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddSupplier();
                    break;
                case "2":
                    UpdateSupplier();
                    break;
                case "3":
                    DeleteSupplier();
                    break;
                case "4":
                    ViewSupplierDetails();
                    break;
                case "5":
                    ViewAllSuppliers();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void AddSupplier()
        {
            Console.Write("Enter supplier name: ");
            string name = Console.ReadLine();
            Console.Write("Enter contact information: ");
            string contactInfo = Console.ReadLine();
            Console.Write("Enter inventory ID: ");
            int inventoryId = int.Parse(Console.ReadLine());

            _supplierController.AddSupplier(name, contactInfo, inventoryId);
        }

        private void UpdateSupplier()
        {
            Console.Write("Enter supplier ID to update: ");
            int supplierId = int.Parse(Console.ReadLine());
            Console.Write("Enter new name: ");
            string newName = Console.ReadLine();
            Console.Write("Enter new contact information: ");
            string newContactInfo = Console.ReadLine();

            _supplierController.UpdateSupplier(supplierId, newName, newContactInfo);
        }

        private void DeleteSupplier()
        {
            Console.Write("Enter supplier ID to delete: ");
            int supplierId = int.Parse(Console.ReadLine());
            _supplierController.DeleteSupplier(supplierId);
        }

        private void ViewSupplierDetails()
        {
            Console.Write("Enter supplier ID to view details: ");
            int supplierId = int.Parse(Console.ReadLine());
            _supplierController.ViewSupplierDetails(supplierId);
        }

        private void ViewAllSuppliers()
        {
            _supplierController.ViewAllSuppliers();
        }

        private void DisplayTransactionMenu()
        {
            Console.WriteLine("===== Transaction Management =====");
            Console.WriteLine("1. Add Transaction");
            Console.WriteLine("2. View Transaction History");
            Console.WriteLine("3. View All Transactions");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddTransaction();
                    break;
                case "2":
                    ViewTransactionHistory();
                    break;
                case "3":
                    ViewAllTransactions();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void AddTransaction()
        {
            Console.Write("Enter product ID: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Enter transaction type (Add/Remove): ");
            string type = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            _transactionController.AddTransaction(productId, type, quantity);
        }

        private void ViewTransactionHistory()
        {
            Console.Write("Enter product ID to view transaction history: ");
            int productId = int.Parse(Console.ReadLine());
            _transactionController.ViewTransactionHistory(productId);
        }

        private void ViewAllTransactions()
        {
            _transactionController.ViewAllTransactions();
        }

        private void GenerateInventoryReport()
        {
            Console.WriteLine("===== Inventory Report =====");
            Console.WriteLine("Products in Inventory:");
            _productController.ViewAllProducts();
            Console.WriteLine("Suppliers:");
            _supplierController.ViewAllSuppliers();
            Console.WriteLine("Transaction History:");
            _transactionController.ViewAllTransactions();
        }
    }

}
