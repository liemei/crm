using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Dal;
using lsc.Model;

namespace lsc.Bll
{
    public class UserAnswerLogBll
    {
        public async Task<int> AddAsync(UserAnswerLog userAnswerLog)
        {
            return await UserAnswerLogDal.Ins.AddAsync(userAnswerLog);
        }

        public async Task<UserAnswerLog> GetById(int id)
        {
            return await UserAnswerLogDal.Ins.GetById(id);
        }

        public async Task<int> UpdateAsync(UserAnswerLog userAnswerLog)
        {
            return await UserAnswerLogDal.Ins.UpdateAsync(userAnswerLog);
        }
        public async Task<Tuple<List<UserAnswerLog>, long>> GetList(int userId, int pageIndex, int pageSize)
        {
            return await UserAnswerLogDal.Ins.GetList(userId, pageIndex, pageSize);
        }
    }
}
