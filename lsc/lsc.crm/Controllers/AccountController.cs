using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lsc.Common;
using lsc.Bll;
using lsc.Model;
using Microsoft.AspNetCore.Http;

namespace lsc.crm.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            string username = Request.Form["username"].TryToString();
            string password = Request.Form["password"].TryToString();
            UserBll bll = new UserBll();
            UserInfo user = await bll.UserLogin(username,password);
            if (user!=null)
            {

                HttpContext.Session.SetString("user",JsonSerializerHelper.Serialize(user));
                return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "登录失败" });
        }

        public IActionResult LoginOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 打开邮件回调
        /// </summary>
        /// <param name="logid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> OpenEmailCallBack(int logid)
        {
            SendEmailLogBll bll = new SendEmailLogBll();
            var info = await bll.GetById(logid);
            if (info != null)
            {
                info.IsRead = true;
            }
            bll.Update(info);
            return Json(new { code = 1, msg = "OK" });
        }
    }
}