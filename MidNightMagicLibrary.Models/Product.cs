using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidNightMagicLibrary.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        public string? Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        [Range(0, 10000)]
        public int? Quantity { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public string? ImageUrl { get; set; }
        [NotMapped]
        public int Count { get; set; }
    }
}
