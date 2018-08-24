using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Dal;
using lsc.Model;

namespace lsc.Bll
{
    /// <summary>
    /// 邮件发送任务
    /// </summary>
    public class SendEmailTaskBll
    {
        public async Task<int> AddAsync(SendEmailTask sendEmailTask)
        {
            return await SendEmailTaskDal.Ins.AddAsync(sendEmailTask);
        }

        public async Task<SendEmailTask> GetById(int id)
        {
            return await SendEmailTaskDal.Ins.GetById(id);
        }

        public async Task<bool> DelAsync(SendEmailTask sendEmailTask)
        {
            return await SendEmailTaskDal.Ins.DelAsync(sendEmailTask);
        }

        public async Task<List<SendEmailTask>> GetList()
        {
            return await SendEmailTaskDal.Ins.GetList();
        }
    }
}
