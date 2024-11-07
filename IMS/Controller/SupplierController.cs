using IMS.Exceptions;
using IMS.Models;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Controller
{
    public class SupplierController
    {
        private readonly SupplierRepository _supplierRepository;

        public SupplierController(SupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public void AddSupplier(string name, string contactInfo, int inventoryId)
        {
            try
            {
                var supplier = new Supplier
                {
                    Name = name,
                    ContactInformation = contactInfo,
                    InventoryId = inventoryId
                };

                _supplierRepository.AddSupplier(supplier);
                Console.WriteLine("Supplier added successfully.");
            }
            catch (DuplicateSupplierException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UpdateSupplier(int supplierId, string newName, string newContactInfo)
        {
            try
            {
                _supplierRepository.UpdateSupplier(supplierId, newName, newContactInfo);
                Console.WriteLine("Supplier updated successfully.");
            }
            catch (SupplierNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (DuplicateSupplierException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeleteSupplier(int supplierId)
        {
            try
            {
                _supplierRepository.DeleteSupplier(supplierId);
                Console.WriteLine("Supplier deleted successfully.");
            }
            catch (SupplierNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ViewSupplierDetails(int supplierId)
        {
            var supplier = _supplierRepository.GetSupplierById(supplierId);
            if (supplier != null)
            {
                Console.WriteLine($"ID: {supplier.SupplierId}, Name: {supplier.Name}, Contact Info: {supplier.ContactInformation}");
            }
            else
            {
                Console.WriteLine("Supplier not found.");
            }
        }

        public void ViewAllSuppliers()
        {
            var suppliers = _supplierRepository.GetAllSuppliers();
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.SupplierId}, Name: {supplier.Name}, Contact Info: {supplier.ContactInformation}");
            }
        }

    }
}
