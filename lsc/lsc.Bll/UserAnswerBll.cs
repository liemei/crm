using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Dal;
using lsc.Model;

namespace lsc.Bll
{
    public class UserAnswerBll
    {
        public async Task<int> AddAsync(UserAnswer userAnswer)
        {
            return await UserAnswerDal.Ins.AddAsync(userAnswer);
        }

        public async Task<List<UserAnswer>> GetList(int logId)
        {
            return await UserAnswerDal.Ins.GetList(logId);
        }

        public async Task<UserAnswer> GetByIdAsync(int id)
        {
            return await UserAnswerDal.Ins.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(UserAnswer userAnswer)
        {
            return await UserAnswerDal.Ins.UpdateAsync(userAnswer);
        }
    }
}
