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
    public class UserAnswerDal
    {
        private static UserAnswerDal ins;
        public static UserAnswerDal Ins => ins ?? (ins = new UserAnswerDal());

        public async Task<int> AddAsync(UserAnswer userAnswer)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.UserAnswer.AddAsync(userAnswer);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserAnswerDal.AddAsync", e);
            }
            return id;
        }

        public async Task<List<UserAnswer>> GetList(int logId)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var list = await dataContext.UserAnswer.Where(x => x.LogId == logId).ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserAnswerDal.GetList", e);
            }
            return null;
        }

        public async Task<UserAnswer> GetByIdAsync(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.UserAnswer.FindAsync(id);
                return info;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserAnswerDal.GetByIdAsync", e);
            }
            return null;
        }

        public async Task<bool> UpdateAsync(UserAnswer userAnswer)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.UserAnswer.Update(userAnswer);
                await dataContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserAnswerDal.AddAsync", e);
            }

            return flag;
        }
    }
}
