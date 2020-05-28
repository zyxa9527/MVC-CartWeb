using System.Web;
using System.Web.Mvc;
using WebApplication9.Filter;

namespace WebApplication9
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //新增全域性授權過濾器
            //filters.Add(new LoginAuthorizeAttribute());
        }
    }
}
