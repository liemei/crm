using System;
using System.Collections.Generic;
using System.Text;
using lsc.Model;
using lsc.Common;
using System.Threading.Tasks;
using System.Linq;

namespace lsc.Dal
{
    /// <summary>
    /// 角色相关操作
    /// </summary>
    public class UserRoleDal
    {
        private static UserRoleDal _Ins;
        public static UserRoleDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new UserRoleDal();
                return _Ins;
            }
        }

        public async Task<int> AddRole(UserRole role)
        {
            int id = 0;
            try
            {
                DataContext db = new DataContext();
                var entity =await db.UserRoles.AddAsync(role);
                await db.SaveChangesAsync();
                id = entity.Entity.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("UserRoleDal.AddRole",ex);
            }
            return id;
        }

        public async Task<bool> UpdateRole(UserRole role)
        {
            bool flag = false;
            try
            {
                DataContext db = new DataContext();
                db.UserRoles.Update(role);
                await db.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("UserRoleDal.UpdateRole", ex);
            }
            return flag;
        }

        public async Task<List<UserRole>> Get()
        {
            List<UserRole> list = null;
            try
            {
                await Task.Run(()=> {
                    DataContext db = new DataContext();
                    list = db.UserRoles.Where(x=>x.State == Model.Enume.StateEnum.Invalid).ToList();
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("UserRoleDal.Get",ex);
            }
            return list;
        }

        public async Task<UserRole> Get(int ID)
        {
            try
            {
                DataContext db = new DataContext();
                var userrole = await db.UserRoles.FindAsync(ID);
                return userrole;
            } catch (Exception ex)
            {
                ClassLoger.Error("UserRoleDal.Get",ex);
            }
            return null;
        }
    }
}
