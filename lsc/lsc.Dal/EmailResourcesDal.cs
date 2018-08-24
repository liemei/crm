using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bnuxq.Common;
using bnuxq.Model;
using Microsoft.EntityFrameworkCore;

namespace bnuxq.Dal
{
    public class EmailResourcesDal
    {
        private static EmailResourcesDal ins;
        public static EmailResourcesDal Ins => ins ?? (ins = new EmailResourcesDal());

        public async Task<int> AddAsync(EmailResources emailResources)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.EmailResourcess.AddAsync(emailResources);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailResourcesDal.AddAsync", e);
            }
            return id;
        }

        public async Task<EmailResources> GetById(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.EmailResourcess.FindAsync(id);
                return entity;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailResourcesDal.GetById", e);
            }
            return null;
        }

        public async Task<bool> DelAsync(EmailResources emailResources)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.EmailResourcess.Remove(emailResources);
                await dataContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailResourcesDal.DelAsync", e);
            }
            return flag;
        }

        public async Task<List<EmailResources>> GetList()
        {
            try
            {
                DataContext dataContext = new DataContext();
                var list = await dataContext.EmailResourcess.ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailResourcesDal.GetList", e);
            }
            return null;
        }

        public List<EmailResources> GetLists()
        {
            try
            {
                DataContext dataContext = new DataContext();
                var list = dataContext.EmailResourcess.ToList();
                return list;
            }
            catch (Exception e)
            {
                ClassLoger.Error("EmailResourcesDal.GetList", e);
            }
            return null;
        }
    }
}
