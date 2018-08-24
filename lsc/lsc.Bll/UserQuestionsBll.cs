using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bnuxq.Dal;
using bnuxq.Model;

namespace bnuxq.Bll
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
