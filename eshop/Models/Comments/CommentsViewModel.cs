using eshop.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class CommentsViewModel
    {
        public int ProductId { get; set; }
        public int MainCommentId { get; set; }
        public string Message { get; set; } 
        public int UserId { get; set; }
        public bool Approved { get; set; }

        public IList<MainComment> MainComments { get; set; }
    }
}
