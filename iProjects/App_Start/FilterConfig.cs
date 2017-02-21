using System.Web;
using System.Web.Mvc;
using iProjects.Toast;

namespace iProjects
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MessagesActionFilter());
        }
    }
}
