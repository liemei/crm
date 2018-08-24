using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Dal;
using lsc.Model;

namespace lsc.Bll
{
    public class UserQuestionsBll
    {
        public async Task<int> AddAsync(UserQuestions userQuestions)
        {
            return await UserQuestionsDal.Ins.AddAsync(userQuestions);
        }

        public async Task<List<UserQuestions>> GetList(int logId)
        {
            return await UserQuestionsDal.Ins.GetList(logId);
        }
    }
}
