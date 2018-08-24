using bnuxq.Common;
using bnuxq.Model;
using bnuxq.Model.Enume;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace bnuxq.Dal
{
    public class SalesProjectDal
    {
        private static SalesProjectDal _Ins;
        public static SalesProjectDal Ins
        {
            get
            {
                if (_Ins == null)
                {
                    _Ins = new SalesProjectDal();
                }
                return _Ins;
            }
        }

        public async Task<int> AddAsync(SalesProject salesProject)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.AddAsync(salesProject);
                await dataContext.SaveChangesAsync();
                id = info.Entity.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectDal.AddAsync", ex);
            }
            return id;
        }

        public async Task<bool> UpdateAsync(SalesProject salesProject)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.SalesProjects.Update(salesProject);
                await dataContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectDal.UpdateAsync", ex);
            }
            return flag;
        }

        public async Task<SalesProject> GetAsync(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.SalesProjects.FindAsync(id);
                return info;
            } catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectDal.GetAsync", ex);
            }
            return null;
        }

        public async Task<Tuple<List<SalesProject>, long>> GetTupleAsync(int pageIndex, int pageSize, int? CreateUserID = null, string title = null, int? EnterCustomerID = null, ProjectStateEnum? ProjectState = null, ProjectTypeEnum? ProjectType = null, DateTime? ProjectStartTime = null, DateTime? ProjectEndTime = null)
        {
            List<SalesProject> list = null;
            long count = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var query = dataContext.SalesProjects.AsQueryable();
                if (CreateUserID.HasValue)
                {
                    query = query.Where(x => x.CreateUserID == CreateUserID);
                }
                if (!title.IsNull())
                {
                    query = query.Where(x => x.Title == title);
                }
                if (EnterCustomerID.HasValue)
                {
                    query = query.Where(x => x.EnterCustomerID == EnterCustomerID);
                }
                if (ProjectState.HasValue)
                {
                    query = query.Where(x => x.ProjectState == ProjectState);
                }
                if (ProjectType.HasValue)
                {
                    query = query.Where(x => x.ProjectType == ProjectType);
                }
                if (ProjectStartTime.HasValue)
                {
                    query = query.Where(x => x.ProjectTime > ProjectStartTime);
                }
                if (ProjectEndTime.HasValue)
                {
                    query = query.Where(x => x.ProjectTime < ProjectEndTime);
                }
                await Task.Run(() =>
                {
                    list = query.OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    count = query.LongCount();
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectDal.GetTupleAsync", ex);
            }
            return Tuple.Create(list, count);
        }

        public async Task<List<UserEnterReport>> GetAsync(DateTime startTime, DateTime endTime, int userid)
        {
            List<UserEnterReport> list = new List<UserEnterReport>();
            try
            {
                await Task.Run(() => {
                    DataContext dataContext = new DataContext();
                    var query = dataContext.SalesProjects.Where(x => x.ProjectTime > startTime && x.ProjectTime < endTime);
                    if (userid > 0)
                        query = query.Where(x => x.HeadID == userid);
                    var report = from q in query.ToList()
                                 group q by q.HeadID into g
                                 select new
                                 {
                                     UserID = g.Key,
                                     Total = g.Count()
                                 };
                    if (report != null)
                    {
                        foreach (var r in report)
                        {
                            UserEnterReport userEnterReport = new UserEnterReport();
                            userEnterReport.UserID = r.UserID;
                            userEnterReport.Total = r.Total;
                            list.Add(userEnterReport);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectDal.GetAsync", ex);
            }
            return list;
        }
        /// <summary>
        /// 获取本月应收账款的项目
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<SalesProject>> GetListAsync(int userid, DateTime startTime, DateTime endTime)
        {
            List<SalesProject> list = new List<SalesProject>();
            try
            {
                await Task.Run(() => {
                    DataContext dataContext = new DataContext();
                    var query = dataContext.SalesProjects.Where(x => x.ReceoverPayTime > startTime && x.ReceoverPayTime < endTime);
                    if (userid > 0)
                    {
                        query = query.Where(x => x.HeadID == userid);
                    }
                    list = query.OrderByDescending(x => x.ID).ToList();
                });

            } catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectDal.GetListAsync", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取客户下的销售项目
        /// </summary>
        /// <param name="EnterCustomerID"></param>
        /// <returns></returns>
        public async Task<List<SalesProject>> GetListAsync(int EnterCustomerID)
        {
            List<SalesProject> list = new List<SalesProject>();
            try
            {
                DataContext dataContext = new DataContext();
                await Task.Run(()=> {
                    list = dataContext.SalesProjects.Where(x => x.EnterCustomerID == EnterCustomerID).ToList();
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("SalesProjectDal.GetListAsync",ex);
            }
            return list;
        }
    }
}
