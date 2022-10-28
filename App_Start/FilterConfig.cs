using System.Web;
using System.Web.Mvc;

namespace MVC_CRUD_ADO_OAUTH
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
