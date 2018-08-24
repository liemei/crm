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
    public class TargetEmailDal
    {
        private static TargetEmailDal ins;
        public static TargetEmailDal Ins => ins ?? (ins = new TargetEmailDal());

        public async Task<int> AddAsync(TargetEmail targetEmail)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.TargetEmails.AddAsync(targetEmail);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("TargetEmailDal.AddAsync", e);
            }
            return id;
        }

        public int Add(TargetEmail targetEmail)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = dataContext.TargetEmails.Add(targetEmail);
                dataContext.SaveChanges();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("TargetEmailDal.AddAsync", e);
            }
            return id;
        }

        public async Task<int> UpdateAsync(TargetEmail targetEmail)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var entity = dataContext.TargetEmails.Update(targetEmail);
                await dataContext.SaveChangesAsync();
                id = entity.Entity.Id;
            }
            catch (Exception e)
            {
                ClassLoger.Error("TargetEmailDal.AddAsync", e);
            }
            return id;
        }

        public async Task<TargetEmail> Get(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var entity = await dataContext.TargetEmails.FindAsync(id);
                return entity;
            }
            catch (Exception e)
            {
                ClassLoger.Error("TargetEmailDal.Get", e);
            }
            return null;
        }

        public async Task<Tuple<List<TargetEmail>, long>> GetList(int pageIndex,int pageSize)
        {
            List<TargetEmail> list = new List<TargetEmail>();
            long count = 0;
            try
            {
                DataContext dataContext = new DataContext();
                count = await dataContext.TargetEmails.LongCountAsync();
                list = await dataContext.TargetEmails.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception e)
            {
                ClassLoger.Error("TargetEmailDal.GetList", e);
            }
            return Tuple.Create(list, count);
        }

        public async Task<bool> DelAsync(TargetEmail targetEmail)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.TargetEmails.Remove(targetEmail);
                await dataContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception e)
            {
                ClassLoger.Error("TargetEmailDal.DelAsync", e);
            }
            return flag;
        }

        public async Task<bool> Exists(string email)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.TargetEmails.FirstOrDefaultAsync(x => x.Email == email);
                if (info!=null)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                ClassLoger.Error("TargetEmailDal.Exists", e);
            }
            return flag;
        }
    }
}
