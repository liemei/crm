using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Dal;
using lsc.Model;

namespace lsc.Bll
{
    public class OptionBll
    {
        public async Task<int> Addasync(Option option)
        {
            return await OptionDal.Ins.Addasync(option);
        }

        public async Task<bool> DelAsync(Option option)
        {
            return await OptionDal.Ins.DelAsync(option);
        }
        /// <summary>
        /// 根据考题获取对应的选项
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task<List<Option>> GetList(int questionId)
        {
            return await OptionDal.Ins.GetList(questionId);
        }

        public async Task<Option> GetByIdAsync(int id)
        {
            return await OptionDal.Ins.GetByIdAsync(id);
        }
    }
}
