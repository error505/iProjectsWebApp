using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iProjects.Models;

namespace iProjects.Controllers
{
    public class PostsController : ApiController
    {
        private ApplicationDbContext _ctx;

        public PostsController()
        {
            this._ctx = new ApplicationDbContext();
        }

        // GET api/<controller>
        public IList<Post> Get()
        {
            var allPosts = this._ctx.Posts.OrderByDescending(x => x.DatePosted).ToList();
            foreach (var post in allPosts)
            {
                var allCommentsOnThisPost = _ctx.Comments.Where(p => p.ParentPost.Id == post.Id).ToList();
                foreach (var comment in allCommentsOnThisPost)
                {
                    post.Comments.Add(comment);
                }
            }
            return allPosts;
        }
    }
}