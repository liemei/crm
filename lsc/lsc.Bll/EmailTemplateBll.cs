using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Dal;
using lsc.Model;

namespace lsc.Bll
{
    public class EmailTemplateBll
    {
        public async Task<int> AddAsync(EmailTemplate emailTemplate)
        {
            return await EmailTemplateDal.Ins.AddAsync(emailTemplate);
        }

        public async Task<EmailTemplate> GetById(int id)
        {
            return await EmailTemplateDal.Ins.GetById(id);
        }

        public EmailTemplate GetByIds(int id)
        {
            return EmailTemplateDal.Ins.GetByIds(id);
        }

        public async Task<bool> DelAsync(EmailTemplate emailTemplate)
        {
            return await EmailTemplateDal.Ins.DelAsync(emailTemplate);
        }

        public async Task<List<EmailTemplate>> GetList()
        {
            return await EmailTemplateDal.Ins.GetList();
        }
    }
}
