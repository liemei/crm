using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bnuxq.Bll;
using bnuxq.Model;
using bnuxq.Common;
using bnuxq.crm.ViewModel;

namespace bnuxq.crm.Controllers
{
    public class UsersController : BaseController
    {
        public IActionResult Index()
        {
            ViewBag.root = "account";
            return View();
        }

        public IActionResult ModuleList()
        {
            ModuleInfoBll bll = new ModuleInfoBll();
            var list =  bll.GetList();
            ViewBag.root = "account";
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddModule()
        {
            string name = Request.Form["name"].TryToString();
            ModuleInfo moduleInfo = new ModuleInfo();
            moduleInfo.Name = name;
            ModuleInfoBll bll = new ModuleInfoBll();
            int id = await bll.AddModuleInfo(moduleInfo);
            if (id > 0)
                return Json(new { code = 1, msg = "OK" });
            return Json(new { code = 0, msg = "保存失败" });
        }
        [HttpGet]
        public async Task<IActionResult> DelModel(int id)
        {
            ModuleInfoBll bll = new ModuleInfoBll();
            var info =await bll.Get(id);
            if (info!=null)
            {
                await bll.DelModel(info);
                return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "删除失败" });
        }

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RoleList()
        {
            UserRoleBll bll = new UserRoleBll();
            var list =await bll.Get();
            ViewBag.root = "account";
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole()
        {
            string name = Request.Form["name"].TryToString();
            UserRole userRole = new UserRole();
            userRole.RoleName = name;
            userRole.State = Model.Enume.StateEnum.Invalid;
            UserRoleBll bll = new UserRoleBll();
            int id = await bll.AddRole(userRole);
            if (id > 0)
                return Json(new { code = 1, msg = "OK" });
            return Json(new { code = 0, msg = "失败" });
        }
        [HttpGet]
        public async Task<IActionResult> DelRole(int id)
        {
            UserRoleBll bll = new UserRoleBll();
            var info = await bll.Get(id);
            if (info != null)
            {
                info.State = Model.Enume.StateEnum.Valid;
                bool falg = await bll.UpdateRole(info);
                if(falg)
                    return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "删除失败" });
        }

        /// <summary>
        /// 角色权限
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddRoleJuri(int roleid)
        {
            UserRoleJurisdictionBll bll = new UserRoleJurisdictionBll();
            var list =  bll.GetListAsync(roleid);
            ModuleInfoBll mbll = new ModuleInfoBll();
            List<ModuleInfo> mlist = mbll.GetList();
            ViewBag.mlist = mlist;

            UserRoleBll rbll = new UserRoleBll();
            UserRole userRole= await rbll.Get(roleid);
            ViewBag.userRole = userRole;
            ViewBag.roleid = roleid;
            ViewBag.root = "account";
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> SaveRoleJuri([FromForm]UserRoleJurisdiction userRoleJurisdiction)
        {
            UserRoleJurisdictionBll bll = new UserRoleJurisdictionBll();
            if (userRoleJurisdiction.ID <= 0)
            {
                int id = await bll.AddUserRoleJurisdiction(userRoleJurisdiction);
                if (id > 0)
                    return Json(new { code = 1, msg = "保存成功" });
                else
                    Json(new { code = 0, msg = "保存失败" });
            }
            else
            {
                bool flag = await bll.UpdateUserRoleJurisdiction(userRoleJurisdiction);
                if(flag)
                    return Json(new { code = 1, msg = "保存成功" });
                else
                    return Json(new { code = 0, msg = "保存失败" });
            }
            return Json(new { code = 0, msg = "保存失败" });
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public IActionResult UserList()
        {
            ViewBag.root = "account";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UserListData(int page, int limit)
        {
            UserBll userBll = new UserBll();
            List<UserViewModel> userlist = new List<UserViewModel>();
            var users =await userBll.GetList("", page, limit);
            if (users.Item1!=null && users.Item1.Count>0)
            {
                foreach (var user in users.Item1)
                {
                    UserViewModel vm = new UserViewModel();
                    vm.CreateTime = user.CreateTime.ToString("yyyy-MM-dd");
                    vm.ID = user.ID;
                    vm.Name = user.Name;
                    if (user.RoleID>0)
                    {
                        UserRoleBll rbll = new UserRoleBll();
                        var info =await rbll.Get(user.RoleID);
                        if (info != null)
                            vm.RoleName = info.RoleName;
                    }
                    vm.TelPhone = user.TelPhone;
                    vm.UserName = user.UserName;
                    userlist.Add(vm);
                }
            }
            return Json(new { code=0, msg="", count= users.Item2, data= userlist.ToArray() });
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddUser(int id=0)
        {
            UserRoleBll rbll = new UserRoleBll();
            var userRolelist =await rbll.Get();
            ViewBag.userRolelist = userRolelist;
            ViewBag.root = "account";
            UserInfo userInfo = new UserInfo();
            if (id>0)
            {
                UserBll bll = new UserBll();
                userInfo = await bll.GetByID(id);
            }
            ViewBag.userInfo = userInfo;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser()
        {
            UserInfo user = new UserInfo();
            user.UserName = Request.Form["UserName"].TryToString();
            string Password = Request.Form["Password"].TryToString();
            user.CreateTime = DateTime.Now;
            user.Name = Request.Form["Name"].TryToString();
            user.TelPhone = Request.Form["TelPhone"].TryToString();
            user.RoleID = Request.Form["RoleID"].TryToInt();
            user.ID = Request.Form["ID"].TryToInt();
            UserBll bll = new UserBll();
            if (user.ID > 0)
            {
                bool flag = await bll.Update(user, Password);
                if (flag)
                    return Json(new { code = 1, msg = "成功" });
            }
            else
            {
                int id = await bll.AddUser(user, Password);
                if (id > 0)
                    return Json(new { code = 1, msg = "成功" });
            }
            
            return Json(new { code = 0, msg = "失败" });
        }
        [HttpGet]
        public async Task<IActionResult> DelUser(int id)
        {
            UserBll bll = new UserBll();
            var user = await bll.GetByID(id);
            if (user!=null)
            {
                user.State = Model.Enume.StateEnum.Valid;
                bool flag =await bll.Update(user);
                if (flag)
                    return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "删除失败" });
        }

        public async Task<IActionResult> UserMessage()
        {
            UserRoleBll userRoleBll = new UserRoleBll();
            UserRole userRole =await userRoleBll.Get(User.RoleID);
            ViewBag.userRole = userRole;
            return View(User);
        }
    }
}