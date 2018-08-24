using bnuxq.Common;
using bnuxq.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace bnuxq.Dal
{
    /// <summary>
    /// 客户所处阶段更新日志相关操作
    /// </summary>
    public class EnterCustPhaseLogDal
    {
        private static EnterCustPhaseLogDal _Ins;
        public static EnterCustPhaseLogDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new EnterCustPhaseLogDal();
                return _Ins;
            }
        }

        public async Task<int> AddAsync(EnterCustPhaseLog log)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var info =await dataContext.EnterCustPhaseLogs.AddAsync(log);
                await dataContext.SaveChangesAsync();
                id = info.Entity.ID;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("EnterCustPhaseLogDal.AddAsync",ex);
            }
            return id;
        }

        public async Task<List<EnterCustPhaseLog>> ListAsync(int EnterCustomerID)
        {
            List<EnterCustPhaseLog> list = null;
            try
            {
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    list = dataContext.EnterCustPhaseLogs.Where(x => x.EnterCustomerID == EnterCustomerID).OrderByDescending(x=>x.ID).ToList() ;
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustPhaseLogDal.ListAsync",ex);
            }
            return list;
        }

        public async Task<List<EnterCustPhaseLog>> ListAsync(DateTime startTime, DateTime endTime, int UserID)
        {
            List<EnterCustPhaseLog> list = null;
            try
            {
                await Task.Run(() => {
                    DataContext dataContext = new DataContext();
                    list = dataContext.EnterCustPhaseLogs.Where(x => x.CreateTime > startTime && x.CreateTime < endTime && x.UserID == UserID).OrderByDescending(x=>x.ID).ToList();
                });
            }
            catch (Exception ex)
            {
                ClassLoger.Error("EnterCustPhaseLogDal.ListAsync", ex);
            }
            return list;
        }

        public async Task<List<UserEnterReport>> GetAsync(DateTime startTime,DateTime endTime,int UserID)
        {
            List<UserEnterReport> list = new List<UserEnterReport>();
            try
            {
                await Task.Run(() =>
                {
                    DataContext dataContext = new DataContext();
                    var query = dataContext.EnterCustPhaseLogs.Where(x=>x.CreateTime>startTime && x.CreateTime<endTime);
                    if (UserID > 0)
                        query = query.Where(x => x.UserID == UserID);
                    var report = from q in query.ToList()
                                 group q by q.UserID into g
                                 select new
                                 {
                                     UserID = g.Key,
                                     Total = g.Count()
                                 };
                    if (report!=null)
                    {
                        foreach (var r in report)
                        {
                            UserEnterReport uer = new UserEnterReport();
                            uer.Total = r.Total;
                            uer.UserID = r.UserID;
                            list.Add(uer);
                        }
                    }
                                 
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustPhaseLogDal.GetAsync",ex);
            }
            return list;
        }

        public async Task<List<EnterTotalReportForDay>> GetReportAsync(DateTime startTime,DateTime endTime,int ? userid = null)
        {
            List<EnterTotalReportForDay> reportList = new List<EnterTotalReportForDay>();
            try
            {
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    if (userid.HasValue)
                    {
                        var report = from u in dataContext.EnterCustPhaseLogs
                                     where u.CreateTime > startTime && u.CreateTime < endTime && u.UserID == userid
                                     group u by new { CreateTime = u.CreateTime.ToString("yyyy-MM-dd"), UserID = u.UserID } into g
                                     select new { g.Key.UserID, g.Key.CreateTime, Total = g.Count() };
                        if (report != null)
                        {
                            foreach (var r in report)
                            {
                                EnterTotalReportForDay enterTotal = new EnterTotalReportForDay();
                                enterTotal.Days = r.CreateTime;
                                enterTotal.Total = r.Total;
                                enterTotal.UserID = r.UserID;
                                reportList.Add(enterTotal);
                            }
                        }
                    }
                    else
                    {
                        var report = from u in dataContext.EnterCustPhaseLogs
                                     where u.CreateTime > startTime && u.CreateTime < endTime
                                     group u by new { CreateTime = u.CreateTime.ToString("yyyy-MM-dd"), UserID = u.UserID } into g
                                     select new { g.Key.UserID, g.Key.CreateTime, Total = g.Count() };
                        if (report != null)
                        {
                            foreach (var r in report)
                            {
                                EnterTotalReportForDay enterTotal = new EnterTotalReportForDay();
                                enterTotal.Days = r.CreateTime;
                                enterTotal.Total = r.Total;
                                enterTotal.UserID = r.UserID;
                                reportList.Add(enterTotal);
                            }
                        }
                    }
                    
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustPhaseLogDal.EnterCustPhaseLogDal",ex);
            }
            return reportList;
        }
    }
}
