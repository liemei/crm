using lsc.Common;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace lsc.Dal
{
    /// <summary>
    /// 回款记录
    /// </summary>
    public class ReceivedPaymentsLogDal
    {
        private static ReceivedPaymentsLogDal _Ins;
        public static ReceivedPaymentsLogDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new ReceivedPaymentsLogDal();
                return _Ins;
            }
        }

        public async Task<int> AddAsync(ReceivedPaymentsLog log)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.ReceivedPaymentsLogs.AddAsync(log);
                await dataContext.SaveChangesAsync();
                id = info.Entity.ID;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("ReceivedPaymentsLogDal.AddAsync",ex);
            }
            return id;
        }

        public async Task<List<ReceivedPaymentsLog>> GetListAsync(DateTime startTime,DateTime endTime,int userid)
        {
            try
            {
                List<ReceivedPaymentsLog> list = new List<ReceivedPaymentsLog>();
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    var query = dataContext.ReceivedPaymentsLogs.Where(x => x.CreateTime > startTime && x.CreateTime < endTime);
                    if (userid > 0)
                        query = query.Where(x => x.UserID == userid);
                    list = query.OrderByDescending(x => x.ID).ToList();
                });
                return list;
            } catch (Exception ex)
            {
                ClassLoger.Error("ReceivedPaymentsLogDal.GetListAsync",ex);
            }
            return null;
        }

        public async Task<List<ReceivedPaymentsLog>> GetListAsync(int SalesProjectID)
        {

            try
            {
                List<ReceivedPaymentsLog> list = new List<ReceivedPaymentsLog>();
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    list = dataContext.ReceivedPaymentsLogs.Where(x => x.SalesProjectID == SalesProjectID).OrderByDescending(x => x.ID).ToList();
                });
                return list;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("ReceivedPaymentsLogDal.GetListAsync",ex);
            }
            return null;
        }
    }
}
