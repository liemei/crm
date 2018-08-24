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
    [Serializable]
    public class OptionDal
    {
        private static OptionDal ins;
        public static OptionDal Ins => ins ?? (ins = new OptionDal());

        public async Task<int> Addasync(Option option)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.Options.AddAsync(option);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("OptionDal.Addasync", e);
            }

            return id;
        }
        /// <summary>
        /// 根据ID获取选项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Option> GetByIdAsync(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info =await dataContext.Options.FindAsync(id);
                return info;
            }
            catch (Exception e)
            {
                ClassLoger.Error("OptionDal.GetByIdAsync");
            }
            return null;
        }
        public async Task<bool> DelAsync(Option option)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.Options.Remove(option);
                await dataContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception e)
            {
                ClassLoger.Error("OptionDal.DelAsync", e);
            }
            return flag;
        }
        /// <summary>
        /// 根据考题获取选项
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task<List<Option>> GetList(int questionId)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var list = await dataContext.Options.Where(x => x.QuestionsId == questionId).OrderBy(x => x.ItemIndex).ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                ClassLoger.Error("OptionDal.GetList", e);
            }
            return null;
        }

    }
}
