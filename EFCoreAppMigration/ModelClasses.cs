using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreAppMigration
{
    public class Category
    {
        [Key] // primary identity key
        public int CategoryRowId { get; set; }
        [Required(ErrorMessage ="Category Id is must")]
        [StringLength(20)]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is must")]
        [StringLength(200)]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Base Price is must")]
        public int BasePrice { get; set; }

        public ICollection<Product> Products { get; set; } // expected one-to-many relationship
    }

    public class Product
    {
        [Key] // primary identity key
        public int ProductRowId { get; set; }
        [Required(ErrorMessage = "Product Id is must")]
        [StringLength(20)]
        public string ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is must")]
        [StringLength(200)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Manufacturer is must")]
        [StringLength(100)]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "Description is must")]
        [StringLength(400)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is must")]
        public int Price { get; set; }
        [ForeignKey("CategoryRowId")]
        public int CategoryRowId { get; set; } // expected foreign key
        public Category Category { get; set; } // expcted referencial integrity
    }
}
