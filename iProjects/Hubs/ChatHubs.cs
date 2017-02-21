using System;
using System.Linq;
using iProjects.Models;
using Microsoft.AspNet.SignalR;

namespace iProjects.Hubs
{
    public class BoardHub : Hub
    {
        public void WritePost(string username, string message)
        {
            var ctx = new ApplicationDbContext();
            var post = new Post { Message = message, Username = username, DatePosted = DateTime.Now };
            ctx.Posts.Add(post);
            ctx.SaveChanges();

            Clients.All.receivedNewPost(post.Id, post.Username, post.Message, post.DatePosted);
        }

        public void AddComment(int postId, string comment, string username)
        {
            var ctx = new ApplicationDbContext();
            var post = ctx.Posts.FirstOrDefault(p => p.Id == postId);

            if (post != null)
            {
                var newComment = new Comment { ParentPost = post, Message = comment, Username = username, DatePosted = DateTime.Now };
                ctx.Comments.Add(newComment);
                ctx.SaveChanges();

                Clients.All.receivedNewComment(newComment.ParentPost.Id, newComment.Id, newComment.Message, newComment.Username, newComment.DatePosted);
            }
        }
    }
}