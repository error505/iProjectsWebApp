using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iProjects.Models
{
    
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public DateTime DatePosted { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public DateTime DatePosted { get; set; }
        public virtual Post ParentPost { get; set; }
    }
}