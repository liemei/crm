using bnuxq.Dal;
using bnuxq.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bnuxq.Bll
{
    /// <summary>
    /// 角色相关操作
    /// </summary>
    public class UserRoleBll
    {
        public async Task<int> AddRole(UserRole role)
        {
            return await UserRoleDal.Ins.AddRole(role);
        }

        public async Task<List<UserRole>> Get()
        {
            return await UserRoleDal.Ins.Get();
        }
        public async Task<UserRole> Get(int ID)
        {
            return await UserRoleDal.Ins.Get(ID);
        }
        public async Task<bool> UpdateRole(UserRole role)
        {
            return await UserRoleDal.Ins.UpdateRole(role);
        }
    }
}
