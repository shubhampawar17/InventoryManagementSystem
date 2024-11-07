using IMS.Exceptions;
using IMS.Models;
using IMS.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Controller
{
    public class ProductController
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public void AddProduct(string name, string description, int quantity, double price, int inventoryId)
        {
            try
            {
                var product = new Product
                {
                    Name = name,
                    Description = description,
                    Quantity = quantity,
                    Price = price,
                    InventoryId = inventoryId
                };

                _productRepository.AddProduct(product);
                Console.WriteLine("Product added successfully.");
            }
            catch (DuplicateProductException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UpdateProduct(int productId, string newName, string newDescription , int newPrice)
        {
            try
            {
                _productRepository.UpdateProduct(productId, newName, newDescription , newPrice);
                Console.WriteLine("Product updated successfully.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (DuplicateProductException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                _productRepository.DeleteProduct(productId);
                Console.WriteLine("Product deleted successfully.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ViewProductDetails(int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product != null)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Description: {product.Description}, Quantity: {product.Quantity}, Price: {product.Price}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public void ViewAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price}");
            }
        }
    }
}
