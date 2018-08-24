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
    public class SendEmailLogDal
    {
        private static SendEmailLogDal ins;
        public static SendEmailLogDal Ins => ins ?? (ins = new SendEmailLogDal());

        public async Task<int> AddAsync(SendEmailLog senbEmailLog)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.SendEmailLogs.AddAsync(senbEmailLog);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailLogDal.AddAsync", e);
            }
            return id;
        }

        public int Add(SendEmailLog senbEmailLog)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = dataContext.SendEmailLogs.Add(senbEmailLog);
                dataContext.SaveChanges();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailLogDal.AddAsync", e);
            }
            return id;
        }

        public async Task<SendEmailLog> GetById(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.SendEmailLogs.FindAsync(id);
                return info;
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailLogDal.GetById", e);
            }
            return null;
        }

        public int Update(SendEmailLog senbEmailLog)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = dataContext.SendEmailLogs.Update(senbEmailLog);
                dataContext.SaveChanges();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailLogDal.Update", e);
            }
            return id;
        }

        public Tuple<List<SendEmailLog>, long> GetByTaskId(int sendEmailTaskId,int pageIndex,int pageSize)
        {
            List<SendEmailLog> list = new List<SendEmailLog>();
            long count = 0;
            try
            {
                DataContext dataContext = new DataContext();
                list = dataContext.SendEmailLogs.Where(x => x.SendEmailTaskId == sendEmailTaskId)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                count = dataContext.SendEmailLogs.LongCount();
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailLogDal.GetByTaskId", e);
            }
            return Tuple.Create(list, count);
        }
        /// <summary>
        /// 分页获取未发送的邮件
        /// </summary>
        /// <param name="sendEmailTaskId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Tuple<List<SendEmailLog>, long> GetNoSendByTaskId(int sendEmailTaskId, int pageIndex, int pageSize)
        {
            List<SendEmailLog> list = new List<SendEmailLog>();
            long count = 0;
            try
            {
                DataContext dataContext = new DataContext();
                list = dataContext.SendEmailLogs.Where(x => x.SendEmailTaskId == sendEmailTaskId && x.IsSend == false)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                count = dataContext.SendEmailLogs.LongCount();
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailLogDal.GetByTaskId", e);
            }
            return Tuple.Create(list, count);
        }
    }
}
