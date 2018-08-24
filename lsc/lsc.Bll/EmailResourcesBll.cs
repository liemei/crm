using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bnuxq.Dal;
using bnuxq.Model;

namespace bnuxq.Bll
{
    /// <summary>
    /// 邮件资源管理
    /// </summary>
    public class EmailResourcesBll
    {
        public async Task<int> AddAsync(EmailResources emailResources)
        {
            return await EmailResourcesDal.Ins.AddAsync(emailResources);
        }

        public async Task<EmailResources> GetById(int id)
        {
            return await EmailResourcesDal.Ins.GetById(id);
        }

        public async Task<bool> DelAsync(EmailResources emailResources)
        {
            return await EmailResourcesDal.Ins.DelAsync(emailResources);
        }

        public async Task<List<EmailResources>> GetList()
        {
            return await EmailResourcesDal.Ins.GetList();
        }

        public List<EmailResources> GetLists()
        {
            return EmailResourcesDal.Ins.GetLists();
        }
    }
}
