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
    public class TransactionController
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly ProductRepository _productRepository;

        public TransactionController(TransactionRepository transactionRepository, ProductRepository productRepository)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
        }

        public void AddTransaction(int productId, string type, int quantity)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }
            //line 32 to 38
            var inventory = _productRepository.GetInventoryByProductId(productId);
            if (inventory == null)
            {
                Console.WriteLine("Error:Inventory Not Found");
                return;
            }
            if (type == "Remove" && product.Quantity < quantity)
            {
                Console.WriteLine("Error: Insufficient stock.");
                return;
            }

            var transaction = new Transaction
            {
                ProductId = productId,
                Type = type,
                Quantity = quantity,
                Date = DateTime.Now,
                InventoryId = inventory.InventoryId,
            };

            if (type == "Add")
            {
                product.Quantity += quantity;
            }
            else if (type == "Remove")
            {
                product.Quantity -= quantity;
            }

            _transactionRepository.AddTransaction(transaction);
            _productRepository.UpdateProduct(productId, product.Name, product.Description, product.Price); // Update product quantity
            Console.WriteLine("Transaction completed successfully.");
        }

        public void ViewTransactionHistory(int productId)
        {
            try
            {
                var transactions = _transactionRepository.GetTransactionsByProductId(productId);
                //line73 to 78
                if (transactions.Count == 0)
                {
                    Console.WriteLine("No transactions found");
                    return;
                }
                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"ID: {transaction.TransactionId}, Type: {transaction.Type}, Quantity: {transaction.Quantity}, Date: {transaction.Date}");
                }
            }
            catch (TransactionNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ViewAllTransactions()
        {
            var transactions = _transactionRepository.GetAllTransactions();
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"ID: {transaction.TransactionId}, ProductID: {transaction.ProductId}, Type: {transaction.Type}, Quantity: {transaction.Quantity}, Date: {transaction.Date}");
            }
        }
    }
}
