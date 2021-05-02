using eshop.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Comments
{
    [Table(nameof(MainComment))]
    public class MainComment : Entity
    {
        [Required]
        [StringLength(256), MinLength(2)]
        public string CommentMessage { get; set; }

        [Required]
        [ForeignKey(nameof(Identity.User))]
        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Required]
        public bool Approved { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
        public IList<SubComment> SubComments { get; set; }
    }
}
