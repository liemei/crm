using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Common;
using lsc.Model;
using Microsoft.EntityFrameworkCore;

namespace lsc.Dal
{
    public class SendEmailTaskDal
    {
        private static SendEmailTaskDal ins;
        public static SendEmailTaskDal Ins => ins ?? (ins = new SendEmailTaskDal());

        public async Task<int> AddAsync(SendEmailTask sendEmailTask)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.SendEmailTasks.AddAsync(sendEmailTask);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailTaskDal.AddAsync", e);
            }
            return id;
        }

        public async Task<SendEmailTask> GetById(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.SendEmailTasks.FindAsync(id);
                return info;
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailTaskDal.GetById", e);
            }
            return null;
        }

        public async Task<bool> DelAsync(SendEmailTask sendEmailTask)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.SendEmailTasks.Remove(sendEmailTask);
                await dataContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailTaskDal.DelAsync", e);
            }
            return flag;
        }

        public async Task<List<SendEmailTask>> GetList()
        {
            try
            {
                DataContext dataContext = new DataContext();
                var list = await dataContext.SendEmailTasks.ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                ClassLoger.Error("SendEmailTaskDal.GetList", e);
            }
            return null;
        }
    }
}
