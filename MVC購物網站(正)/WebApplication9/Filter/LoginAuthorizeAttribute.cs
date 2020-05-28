using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication9.Filter
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //判斷是否跳過授權過濾器
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            //判斷登入情況
            if (filterContext.HttpContext.Session["LoginName"] == null || filterContext.HttpContext.Session["LoginName"].ToString() == "")
            {
                //HttpContext.Current.Response.Write("認證不通過");
                //HttpContext.Current.Response.End();

                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
    
    
}