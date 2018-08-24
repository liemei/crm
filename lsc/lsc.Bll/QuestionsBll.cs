using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bnuxq.Dal;
using bnuxq.Model;
using bnuxq.Model.Enume;

namespace bnuxq.Bll
{
    public class QuestionsBll
    {
        public async Task<int> AddAsync(Questions questions)
        {
            return await QuestionsDal.Ins.AddAsync(questions);
        }

        public async Task<bool> DelAsync(Questions questions)
        {
            return await QuestionsDal.Ins.DelAsync(questions);
        }

        public async Task<Tuple<List<Questions>, long>> GetList(int pageIndex, int pageSize)
        {
            return await QuestionsDal.Ins.GetList(pageIndex, pageSize);
        }
        /// <summary>
        /// 随机获取指定类型的题目num条
        /// </summary>
        /// <param name="questionsType"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public async Task<List<Questions>> GetList(QuestionsTypeEnum questionsType, int num)
        {
            return await QuestionsDal.Ins.GetList(questionsType, num);
        }

        public async Task<Questions> GetByIdAsync(int id)
        {
            return await QuestionsDal.Ins.GetByIdAsync(id);
        }
    }
}
