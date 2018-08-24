using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bnuxq.Common;
using bnuxq.Model;
using Microsoft.EntityFrameworkCore;

namespace bnuxq.Dal
{
    public class UserQuestionsDal
    {
        private static UserQuestionsDal ins;
        public static UserQuestionsDal Ins => ins ?? (ins = new UserQuestionsDal());

        public async Task<int> AddAsync(UserQuestions userQuestions)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.UserQuestions.AddAsync(userQuestions);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserQuestionsDal.AddAsync", e);
            }
            return id;
        }

        public async Task<List<UserQuestions>> GetList(int logId)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var list = await dataContext.UserQuestions.Where(x => x.LogId == logId).ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                ClassLoger.Error("UserQuestionsDal.GetList", e);
            }
            return null;
        }

    }
}
