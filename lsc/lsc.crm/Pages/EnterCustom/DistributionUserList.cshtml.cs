using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bnuxq.Bll;
using bnuxq.Model;

namespace bnuxq.crm.Pages.EnterCustom
{
    public class DistributionUserListModel : PageModel
    {
        public List<UserInfo> UserList { get; set; }
        public List<UserRole> UserRoleList { get; set; }

        public int ID { get; set; }
        public async Task OnGet(int id)
        {
            UserBll userBll = new UserBll();
            UserList = await userBll.GetListAsync();
            UserRoleBll rbll = new UserRoleBll();
            UserRoleList =await rbll.Get();
            ID = id;
        }
    }
}