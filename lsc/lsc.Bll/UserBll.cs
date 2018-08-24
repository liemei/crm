using lsc.Dal;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lsc.Bll
{
    public class UserBll
    {
        public async Task<int> AddUser(UserInfo userInfo,string password)
        {
            return await UserDal.Ins.AddUser(userInfo, password);
        }

        public async Task<bool> Update(UserInfo user,string password)
        {
            return await UserDal.Ins.Update(user, password);
        }
        public async Task<bool> Update(UserInfo user)
        {
            return await UserDal.Ins.Update(user);
        }
        public async Task<UserInfo> GetByID(int id)
        {
            return await UserDal.Ins.GetByID(id);
        }
        public async Task<Tuple<List<UserInfo>, long>> GetList(string name, int pageIndex, int pageSize)
        {
            return await UserDal.Ins.GetList(name,pageIndex,pageSize);
        }
        public async Task<UserInfo> UserLogin(string username, string password)
        {
            return await UserDal.Ins.UserLogin(username, password);
        }

        public async Task<List<UserInfo>> GetListAsync()
        {
            return await UserDal.Ins.GetListAsync();
        }
    }
}
