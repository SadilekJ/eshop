using eshop.Models.Comments;
using eshop.Models.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table(nameof(Product))]
    public class Product : Entity
    {
        [Required]
        public double Price { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [NotMapped]
        [FileContentType("image")]
        public IFormFile Image { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageSrc { get; set; }
        [Required]
        [StringLength(25)]
        public string ImageAlt { get; set; }
        
        public string DetailOfProduct { get; set; }

        public IList<MainComment> MainComments { get; set; }
    }
}
