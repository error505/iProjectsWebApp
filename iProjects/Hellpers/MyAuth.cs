//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using iProjects.Models;

//namespace iProjects.Hellpers
//{
//    public class MyAuthorizeAttribute : ApplicationUser
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();
//        protected override bool AuthorizeCore(HttpContextBase httpContext)
//        {
//            var authorized = base.AuthorizeCore(httpContext);
//            if (!authorized)
//            {
//                return false;
//            }

//            var rd = httpContext.Request.RequestContext.RouteData;

//            var id = rd.Values["id"];
//            var userName = httpContext.User.Identity.Name;

//            Project projects = db.Projects.Find(id);
//            ApplicationUser user = db.Users.Find(UserName);

//            return projects.UserId == user.Id;
//        }
//    }
//}