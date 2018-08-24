using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bnuxq.Model;
using System.Linq;
using bnuxq.Common;

namespace bnuxq.Dal
{
    public class UserRoleJurisdictionDal
    {
        private static UserRoleJurisdictionDal _Ins;
        public static UserRoleJurisdictionDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new UserRoleJurisdictionDal();
                return _Ins;
            }
        }

        public async Task<int> AddUserRoleJurisdiction(UserRoleJurisdiction urj)
        {
            int id = 0;
            try
            {
                DataContext db = new DataContext();
                var entity = await db.AddAsync(urj);
                await db.SaveChangesAsync();
                id = urj.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("UserRoleJurisdictionDal.AddUserRoleJurisdiction",ex);
            }
            return id;
        }

        public async Task<bool> UpdateUserRoleJurisdiction(UserRoleJurisdiction urj)
        {
            bool flag = false;
            try
            {
                DataContext db = new DataContext();
                db.Update(urj);
                await db.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("UserRoleJurisdictionDal.AddUserRoleJurisdiction", ex);
            }
            return flag;
        }

        /// <summary>
        /// 根据角色ID获取权限
        /// </summary>
        /// <param name="UserRoleID"></param>
        /// <returns></returns>
        public List<UserRoleJurisdiction> GetListAsync(int UserRoleID)
        {
            try
            {
                DataContext dataContext = new DataContext();
                List<UserRoleJurisdiction> list = null;
                list = dataContext.UserRoleJurisdictions.Where(x => x.UserRoleID == UserRoleID).ToList();
                return list;
            } catch (Exception ex)
            {
                ClassLoger.Error("UserRoleJurisdictionDal.GetListAsync",ex);
            }
            return null;
        } 
    }
}
