using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Dal;
using lsc.Model;

namespace lsc.Bll
{
    public class SendEmailLogBll
    {
        public async Task<int> AddAsync(SendEmailLog senbEmailLog)
        {
            return await SendEmailLogDal.Ins.AddAsync(senbEmailLog);
        }

        public int Add(SendEmailLog senbEmailLog)
        {
            return SendEmailLogDal.Ins.Add(senbEmailLog);
        }

        public async Task<SendEmailLog> GetById(int id)
        {
            return await SendEmailLogDal.Ins.GetById(id);
        }

        public int Update(SendEmailLog senbEmailLog)
        {
            return SendEmailLogDal.Ins.Update(senbEmailLog);
        }

        public Tuple<List<SendEmailLog>, long> GetByTaskId(int sendEmailTaskId, int pageIndex, int pageSize)
        {
            return SendEmailLogDal.Ins.GetByTaskId(sendEmailTaskId, pageIndex, pageSize);
        }

        public Tuple<List<SendEmailLog>, long> GetNoSendByTaskId(int sendEmailTaskId, int pageIndex, int pageSize)
        {
            return SendEmailLogDal.Ins.GetNoSendByTaskId(sendEmailTaskId, pageIndex, pageSize);
        }
    }
}
