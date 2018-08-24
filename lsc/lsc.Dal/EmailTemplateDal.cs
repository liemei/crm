using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsc.Common;
using lsc.Model;
using Microsoft.EntityFrameworkCore;

namespace lsc.Dal
{
    public class EmailTemplateDal
    {
        private static EmailTemplateDal ins;
        public static EmailTemplateDal Ins => ins ?? (ins = new EmailTemplateDal());

        public async Task<int> AddAsync(EmailTemplate emailTemplate)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.EmailTemplates.AddAsync(emailTemplate);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailTemplateDal.AddAsync", e);
            }
            return id;
        }

        public async Task<EmailTemplate> GetById(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.EmailTemplates.FindAsync(id);
                return entity;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailTemplateDal.GetById", e);
            }
            return null;
        }

        public EmailTemplate GetByIds(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var entity = dataContext.EmailTemplates.Find(id);
                return entity;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailTemplateDal.GetByIds", e);
            }
            return null;
        }

        public async Task<bool> DelAsync(EmailTemplate emailTemplate)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.EmailTemplates.Remove(emailTemplate);
                await dataContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailTemplateDal.DelAsync", e);
            }
            return flag;
        }

        public async Task<List<EmailTemplate>> GetList()
        {
            try
            {
                DataContext dataContext = new DataContext();
                var list = await dataContext.EmailTemplates.ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailTemplateDal.GetList", e);
            }
            return null;
        }
    }
}
