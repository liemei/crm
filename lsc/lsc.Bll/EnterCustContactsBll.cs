using lsc.Dal;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lsc.Bll
{
    /// <summary>
    /// 客户联系人相关操作
    /// </summary>
    public class EnterCustContactsBll
    {
        public async Task<int> Add(EnterCustContacts ecc)
        {
            return await EnterCustContactsDal.Ins.Add(ecc);
        }

        public async Task<bool> UpdateAsync(EnterCustContacts ecc)
        {
            return await EnterCustContactsDal.Ins.UpdateAsync(ecc);
        }

        public async Task<EnterCustContacts> GetAsync(int id)
        {
            return await EnterCustContactsDal.Ins.GetAsync(id);
        }
        /// <summary>
        /// 根据客户ID获取客户联系人信息
        /// </summary>
        /// <param name="EnterCustID"></param>
        /// <returns></returns>
        public async Task<List<EnterCustContacts>> GetListAsync(int EnterCustID)
        {
            return await EnterCustContactsDal.Ins.GetListAsync(EnterCustID);
        }

        public async Task<List<EnterCustContacts>> GetListAsync(string telphone, string qq)
        {
            return await EnterCustContactsDal.Ins.GetListAsync(telphone, qq);
        }
    }
}
