using IMS.Data;
using IMS.Exceptions;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Repository
{
    public class ProductRepository
    {
        private readonly InventoryContext _context;

        public ProductRepository(InventoryContext context)
        {
            _context = context;
        }
       
        // Add a new product if no duplicate name exists
        public void AddProduct(Product product)
        {
            if (_context.Products.Any(p => p.Name == product.Name))
            {
                throw new DuplicateProductException("Product with this name already exists.");
            }
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Update an existing product's details (excluding quantity) with duplicate name check
        public void UpdateProduct(int productId, string newName, string newDescription, double newPrice)
        {
            var product = GetProductById(productId);

            if (product == null)
            {
                throw new ProductNotFoundException("Product not found.");
            }

            if (_context.Products.Any(p => p.Name == newName && p.ProductId != productId))
            {
                throw new DuplicateProductException("Product with this name already exists.");
            }

            product.Name = newName;
            product.Description = newDescription;
            product.Price = newPrice;

            _context.SaveChanges();
        }

        // Delete an existing product by ID
        public void DeleteProduct(int productId)
        {
            var product = GetProductById(productId);

            if (product == null)
            {
                throw new ProductNotFoundException("Product not found.");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        // Retrieve product details by ID
        public Product GetProductById(int productId)
        {
            //return _context.Products.Find(productId); //change line 72
            return _context.Products.FirstOrDefault(p=>p.ProductId == productId); 
        }
        //line 74 to 79
        //Fetch inventory by product ID
        public Inventory GetInventoryByProductId(int productId)
        {
            return _context.Inventories.FirstOrDefault(i => i.ProductId == productId);
        }
        // Retrieve product details by name
        public Product GetProductByName(string productName)
        {
            return _context.Products.FirstOrDefault(p => p.Name == productName);
        }

        // Retrieve all products
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        // Increase product quantity and record a transaction
        public void AddStock(int productId, int quantity, int inventoryId)
        {
            var product = GetProductById(productId);

            if (product == null)
            {
                throw new ProductNotFoundException("Product not found.");
            }

            product.Quantity += quantity;

            // Add transaction record
            var transaction = new Transaction
            {
                ProductId = productId,
                Quantity = quantity,
                Type = "Add Stock",
                Date = DateTime.Now,
                InventoryId = inventoryId
            };
            _context.Transactions.Add(transaction);

            _context.SaveChanges();
        }

        // Decrease product quantity and record a transaction
        public void RemoveStock(int productId, int quantity, int inventoryId)
        {
            var product = GetProductById(productId);

            if (product == null)
            {
                throw new ProductNotFoundException("Product not found.");
            }

            if (product.Quantity < quantity)
            {
                throw new InsufficientStockException("Insufficient stock to remove the requested quantity.");
            }

            product.Quantity -= quantity;

            // Add transaction record
            var transaction = new Transaction
            {
                ProductId = productId,
                Quantity = quantity,
                Type = "Remove Stock",
                Date = DateTime.Now,
                InventoryId = inventoryId
            };
            _context.Transactions.Add(transaction);

            _context.SaveChanges();
        }
    }

}
