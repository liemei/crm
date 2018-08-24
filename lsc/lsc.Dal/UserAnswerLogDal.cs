using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lsc.Common;
using lsc.Model;
using Microsoft.EntityFrameworkCore;

namespace lsc.Dal
{
    public class UserAnswerLogDal
    {
        private static UserAnswerLogDal ins;
        public static UserAnswerLogDal Ins => ins ?? (ins = new UserAnswerLogDal());

        public async Task<int> AddAsync(UserAnswerLog userAnswerLog)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.UserAnswerLog.AddAsync(userAnswerLog);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserAnswerLogDal.AddAsync", e);
            }
            return id;
        }

        public async Task<int> UpdateAsync(UserAnswerLog userAnswerLog)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = dataContext.UserAnswerLog.Update(userAnswerLog);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserAnswerLogDal.UpdateAsync", e);
            }
            return id;
        }

        public async Task<UserAnswerLog> GetById(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.UserAnswerLog.FindAsync(id);
                return info;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserAnswerLogDal.GetById", e);
            }
            return null;
        }

        public async Task<Tuple<List<UserAnswerLog>, long>> GetList(int userId, int pageIndex, int pageSize)
        {
            List<UserAnswerLog> list = new List<UserAnswerLog>();
            long count = 0;
            try
            {
                DataContext dataContext = new DataContext();
                if (userId==0)
                {
                    count = await dataContext.UserAnswerLog.LongCountAsync();
                    list = await dataContext.UserAnswerLog
                        .OrderByDescending(x => x.Id)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }
                else
                {
                    count = await dataContext.UserAnswerLog.Where(x => x.UserId == userId).LongCountAsync();
                    list = await dataContext.UserAnswerLog
                        .Where(x => x.UserId == userId)
                        .OrderByDescending(x => x.Id)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }
                
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserAnswerLogDal.GetList", e);
            }
            return Tuple.Create(list, count);
        }
    }
}
