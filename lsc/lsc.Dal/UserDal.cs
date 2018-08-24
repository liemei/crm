using lsc.Common;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lsc.Dal
{
    public class UserDal
    {
        private static UserDal _Ins;
        public static UserDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new UserDal();
                return _Ins;
            }
        }

        public async Task<int> AddUser(UserInfo user,string password)
        {
            int id = 0;
            try
            {
                DataContext db = new DataContext();
                var userinfo = await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                await RegistUserAccount(userinfo.Entity, password);
                id = userinfo.Entity.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("UserDal.AddUser",ex);
            }
            return id;
        }

        public async Task<bool> Update(UserInfo user,string password)
        {
            bool flag = false;
            try
            {
                var useracc = GetUserAccountByUserID(user.ID);
                DataContext db = new DataContext();
                db.Users.Update(user);
                if (useracc!=null)
                {
                    db.UserAccounts.Remove(useracc);
                }
                await db.SaveChangesAsync();
                await RegistUserAccount(user,password);
                flag = true;
            } catch (Exception ex)
            {
                ClassLoger.Error("UserDal.Update",ex);
            }
            return flag;
        }
        public async Task<bool> Update(UserInfo user)
        {
            bool flag = false;
            try
            {
                DataContext db = new DataContext();
                db.Users.Update(user);
                await db.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("UserDal.Update", ex);
            }
            return flag;
        }
        public async Task<UserInfo> GetByID(int id)
        {
            try
            {
                DataContext db = new DataContext();
                var userinfo = await db.Users.FindAsync(id);
                return userinfo;
            } catch (Exception ex)
            {
                ClassLoger.Error("UserDal.GetByID",ex);
            }
            return null;
        }

        public async Task<Tuple<List<UserInfo>, long>> GetList(string name,int pageIndex,int pageSize)
        {
            try
            {
                DataContext db = new DataContext();
                List<UserInfo> userlist = new List<UserInfo>();
                long count = 0;
                await Task.Run(()=> {
                    if (!string.IsNullOrEmpty(name))
                    {
                        var query = db.Users.Where(x =>x.State== Model.Enume.StateEnum.Invalid && x.Name.Contains(name));
                        userlist = query.OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        count = query.LongCount();
                    }
                    else
                    {
                        userlist = db.Users.Where(x=>x.State== Model.Enume.StateEnum.Invalid).OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        count = db.Users.Where(x => x.State == Model.Enume.StateEnum.Invalid).LongCount();
                    }
                    
                });
                return Tuple.Create(userlist, count);
            }
            catch (Exception ex)
            {
                ClassLoger.Error("UserDal.GetList",ex);
            }
            return Tuple.Create<List<UserInfo>, long>(new List<UserInfo>(),0);
        }

        public async Task<UserInfo> UserLogin(string username,string password)
        {
            try
            {
                password = ConvertPassword(password);
                UserInfo user = null;
                DataContext db = new DataContext();
                var list = db.UserAccounts.Where(x => x.UserName == username && x.Password == password).ToList();
                if (list != null && list.Count > 0)
                {
                    int userid = list.FirstOrDefault().UserID;
                    user = await GetByID(userid);
                }
                return user;
            } catch (Exception ex)
            {
                ClassLoger.Error("UserDal.UserLogin",ex);
            }
            return null;
        }

        private async Task RegistUserAccount(UserInfo user,string password)
        {
            try
            {
                UserAccount account = new UserAccount();
                account.UserName = user.UserName;
                account.Password = ConvertPassword(password);
                account.UserID = user.ID;
                DataContext db = new DataContext();
                await db.AddAsync(account);
                await db.SaveChangesAsync();
            } catch (Exception ex)
            {
                ClassLoger.Error("UserDll.RegistUserAccount",ex);
            }
        }

        private UserAccount GetUserAccountByUserID(int userid)
        {
            try
            {
                DataContext dataContext = new DataContext();
                return dataContext.UserAccounts.Where(x => x.UserID == userid).FirstOrDefault();
            } catch (Exception ex)
            {
                ClassLoger.Error("UserDal.GetUserAccountByUserID",ex);
            }
            return null;
        }
        string ConvertPassword(string password)
        {
            return password.MD5();
        }

        public async Task<List<UserInfo>> GetListAsync()
        {
            try
            {
                List<UserInfo> list = null;
                DataContext dataContext = new DataContext();
                await Task.Run(()=> {
                    list = dataContext.Users.Where(x => x.State == Model.Enume.StateEnum.Invalid).ToList();
                });
                return list;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("UserDal.GetListAsync",ex);
            }
            return null;
        }
    }
}
