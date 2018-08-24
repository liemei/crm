using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bnuxq.Model;
using bnuxq.Common;
using Microsoft.AspNetCore.Http;
using bnuxq.crm.Filter;
using Microsoft.AspNetCore.Mvc.Filters;
using bnuxq.Bll;

namespace bnuxq.crm.Controllers
{
    [UserLoginFilter]
    public class BaseController : Controller
    {
        private UserInfo _userinfo;
        /// <summary>
        /// 当前登录的用户
        /// </summary>
        public new UserInfo User
        {
            get
            {
                if (_userinfo == null)
                    _userinfo = JsonSerializerHelper.Deserialize<UserInfo>(HttpContext.Session.GetString("user"));
                return _userinfo;
            }
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains("user"))
            {

                ClassLoger.Fail("UserLoginFilter", "session过期");
                context.Result = new RedirectResult("/Account/Index");
                return;
            }
            base.OnActionExecuting(context);
        }
        public override  void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["user"] = User;
            UserRoleJurisdictionBll bll = new UserRoleJurisdictionBll();
            List<UserRoleJurisdiction> userrolejurlist = bll.GetListAsync(User.RoleID);
            ViewData["userrolejurlist"] = userrolejurlist;
            ModuleInfoBll moduleInfoBll = new ModuleInfoBll();
            List<ModuleInfo> modulelist = moduleInfoBll.GetList();
            ViewData["modulelist"] = modulelist;
            base.OnActionExecuted(context);
        }
    }
}