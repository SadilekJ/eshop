using eshop.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Comments
{
    [Table(nameof(SubComment))]
    public class SubComment : Entity
    {
        [Required]
        [StringLength(256), MinLength(2)]
        public string CommentMessage { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required]
        public bool Approved { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(MainComment))]
        public int MainCommentId { get; set; }

        public MainComment MainComment { get; set; }
    }
}
