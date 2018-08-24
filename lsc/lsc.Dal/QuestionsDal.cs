using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lsc.Common;
using lsc.Model;
using lsc.Model.Enume;
using Microsoft.EntityFrameworkCore;

namespace lsc.Dal
{
    public class QuestionsDal
    {
        private static QuestionsDal _ins;
        public static QuestionsDal Ins => _ins ?? (_ins = new QuestionsDal());

        public async Task<int> AddAsync(Questions questions)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.QuestionsDbSet.AddAsync(questions);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("QuestionsDal.add", e);
            }
            return id;
        }

        public async Task<Questions> GetByIdAsync(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.QuestionsDbSet.FindAsync(id);
                return info;
            }
            catch (Exception e)
            {
                ClassLoger.Error("QuestionsDal.GetByIdAsync", e);
            }
            return null;
        }
        public async Task<bool> DelAsync(Questions questions)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.QuestionsDbSet.Remove(questions);
                await dataContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception e)
            {
                ClassLoger.Error("QuestionsDal.DelAsync", e);
            }
            return flag;
        }
        /// <summary>
        /// 分页获取题库
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Tuple<List<Questions>, long>> GetList(int pageIndex, int pageSize)
        {
            List<Questions> list = null;
            long count = 0;
            try
            {
                DataContext dataContext = new DataContext();
                count = await dataContext.QuestionsDbSet.LongCountAsync();
                list = await dataContext.QuestionsDbSet
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                ClassLoger.Error("QuestionsDal.GetList", e);
            }
            return Tuple.Create(list, count);
        }

        /// <summary>
        /// 随机获取指定类型的题目num条
        /// </summary>
        /// <param name="questionsType"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public async Task<List<Questions>> GetList(QuestionsTypeEnum questionsType, int num)
        {
            try
            {
                List<int> idList = new List<int>();
                DataContext dataContext = new DataContext();
                var list = await dataContext.QuestionsDbSet.Where(x => x.QuestionsType == questionsType).Select(x => x.Id).ToListAsync();
                if (list != null)
                {
                    for (int i = 0; i < num; i++)
                    {
                        int times = num;
                    A:
                        Random random = new Random();
                        var rindex = random.Next(0, list.Count - 1);
                        var id = list[rindex];
                        if (idList.Contains(id))
                        {
                            if (times > 0)
                            {
                                times--;
                                goto A;
                            }
                        }
                        else
                        {
                            idList.Add(id);
                        }
                    }

                    var qlist = await dataContext.QuestionsDbSet.Where(x => idList.Contains(x.Id)).ToListAsync();
                    return qlist;
                }

            }
            catch (Exception e)
            {
                ClassLoger.Error("QuestionsDal.GetList", e);
            }
            return null;
        }
    }
}
