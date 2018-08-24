using lsc.Dal;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lsc.Bll
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class UserRoleJurisdictionBll
    {
        public async Task<int> AddUserRoleJurisdiction(UserRoleJurisdiction urj)
        {
            return await UserRoleJurisdictionDal.Ins.AddUserRoleJurisdiction(urj);
        }
        public async Task<bool> UpdateUserRoleJurisdiction(UserRoleJurisdiction urj)
        {
            return await UserRoleJurisdictionDal.Ins.UpdateUserRoleJurisdiction(urj);
        }
        public List<UserRoleJurisdiction> GetListAsync(int UserRoleID)
        {
            return  UserRoleJurisdictionDal.Ins.GetListAsync(UserRoleID);
        }
    }
}
