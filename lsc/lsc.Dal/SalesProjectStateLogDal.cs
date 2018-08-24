using lsc.Common;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace lsc.Dal
{
    public class SalesProjectStateLogDal
    {
        private static SalesProjectStateLogDal _Ins;
        public static SalesProjectStateLogDal Ins
        {
            get
            {
                if (_Ins==null)
                {
                    _Ins = new SalesProjectStateLogDal();
                }
                return _Ins;
            }
        }

        public async Task<int> AddAsync(SalesProjectStateLog salesProjectStateLog)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.SalesProjectStateLogs.AddAsync(salesProjectStateLog);
                await dataContext.SaveChangesAsync();
                return info.Entity.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectStateLogDal.AddAsync",ex);
            }
            return id;
        }

        public async Task<List<SalesProjectStateLog>> GetListAsync(int SalesProjectID)
        {
            try
            {
                List<SalesProjectStateLog> list = null;
                await Task.Run(() =>
                {
                    DataContext dataContext = new DataContext();
                    list = dataContext.SalesProjectStateLogs.Where(x => x.SalesProjectID == SalesProjectID).OrderByDescending(x=>x.ID).ToList();
                });
                return list;
            } catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectStateLogDal.GetListAsync",ex);
            }
            return null;
        }
    }
}
