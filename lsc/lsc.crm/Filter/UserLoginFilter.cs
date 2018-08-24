using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using bnuxq.Common;

namespace bnuxq.crm.Filter
{
    public class UserLoginFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains("user"))
            {
                
                ClassLoger.Fail("UserLoginFilter","session过期");
                context.Result = new RedirectResult("/Account/Index");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
