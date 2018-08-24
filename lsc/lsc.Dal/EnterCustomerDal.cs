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
    /// <summary>
    /// 企业信息
    /// </summary>
    public class EnterCustomerDal
    {
        private static EnterCustomerDal _Ins;
        public static EnterCustomerDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new EnterCustomerDal();
                return _Ins;
            }
        }
        public async Task<int> AddEnterCustomer(EnterCustomer enterCustomer)
        {
            int id = 0;
            try
            {
                DataContext db = new DataContext();
                var entity =await db.EnterCustomers.AddAsync(enterCustomer);
                await db.SaveChangesAsync();
                id = entity.Entity.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.AddEnterCustomer",ex);
            }
            return id;
        }

        public async Task<bool> UpdateEnterCustomerAsync(EnterCustomer enterCustomer)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.Update(enterCustomer);
                await dataContext.SaveChangesAsync();
                flag = true;
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.UpdateEnterCustomerAsync",ex);
            }
            return flag;
        }

        public async Task<EnterCustomer> GetAsync(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.EnterCustomers.FindAsync(id);
                return info;
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.GetAsync",ex);
            }
            return null;
        }
        public async Task<EnterCustomer> GetAsync(string EnterName)
        {
            try
            {
                EnterCustomer info = null;
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    var list = dataContext.EnterCustomers.Where(x => x.EnterName == EnterName);
                    if (list != null && list.Count() > 0)
                        info = list.First();
                });
                return info;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.GetAsync", ex);
            }
            return null;
        }
        public async Task<bool> ExistsEnterNameAsync(int id,string EnterName)
        {
            bool flag = false;
            try
            {
                await Task.Run(() =>
                {
                    DataContext dataContext = new DataContext();
                    flag = dataContext.EnterCustomers.Where(x => x.EnterName == EnterName && x.ID != id).LongCount() > 0;
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.ExistsEnterNameAsync",ex);
            }
            return flag;
        }
        /// <summary>
        /// 获取企业客户信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userid">所属用户ID</param>
        /// <param name="EnterName">企业名称</param>
        /// <param name="CustomerType">客户类型</param>
        /// <param name="Relationship">关系等级</param>
        /// <param name="Phase">阶段</param>
        /// <param name="ValueGrade">价值评估</param>
        /// <param name="Source">客户来源</param>
        /// <param name="IsHeat">是否热点客户</param>
        /// <param name="DegreeOfHeat">热度</param>
        /// <param name="Province">省份</param>
        /// <param name="City">城市</param>
        /// <param name="UpdateTime">最近更新时间</param>
        /// <returns></returns>
        public async Task<Tuple<List<EnterCustomer>, long>> GetAsync(int pageIndex, int pageSize,int ? userid = null,string EnterName = null, CustomerTypeEnum ? CustomerType=null, RelationshipEnume ? Relationship = null, PhaseEnume ? Phase= null, ValueGradeEnume ? ValueGrade =null, CustSource ? Source=null, bool ? IsHeat =null, DegreeOfHeatEnume? DegreeOfHeat=null, string Province=null, string City=null, DateTime ? UpdateTime=null,bool ? timeType=null, List<int> idList=null)
        {
            List<EnterCustomer> list = new List<EnterCustomer>();
            long count = 0;
            try
            {
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    var result = dataContext.EnterCustomers.Where(x=>x.State== StateEnum.Invalid);
                    if (userid.HasValue)
                        result = result.Where(x=>x.UserID==userid);
                    if (!EnterName.IsNull())
                        result = result.Where(x => x.EnterName.Contains(EnterName));
                    if (CustomerType.HasValue)
                        result = result.Where(x => x.CustomerType == CustomerType);
                    if (Phase.HasValue)
                        result = result.Where(x => x.Phase == Phase);
                    if (IsHeat.HasValue)
                        result = result.Where(x => x.IsHeat == IsHeat);
                    if (DegreeOfHeat.HasValue)
                        result = result.Where(x => x.DegreeOfHeat == DegreeOfHeat);
                    if (!Province.IsNull())
                        result = result.Where(x => x.Province == Province);
                    if (!City.IsNull())
                        result = result.Where(x => x.City == City);
                    if (timeType.HasValue && timeType == true)
                    {
                        if (UpdateTime.HasValue)
                            result = result.Where(x => x.UpdateTime > UpdateTime);
                    }
                    else
                    {
                        if (UpdateTime.HasValue)
                            result = result.Where(x => x.UpdateTime < UpdateTime);
                    }
                    if (Relationship.HasValue)
                        result = result.Where(x => x.Relationship == Relationship);
                    if (ValueGrade.HasValue)
                        result = result.Where(x => x.ValueGrade == ValueGrade);
                    if (Source.HasValue)
                        result = result.Where(x => x.Source == Source);
                    if (idList != null && idList.Count > 0)
                        result = result.Where(x => idList.Contains(x.ID));

                    list = result.OrderByDescending(x => x.ID).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
                    count = result.LongCount();
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.GetAsync",ex);
            }
            return Tuple.Create(list, count);
        }

        /// <summary>
        /// 获取企业客户信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userid">所属用户ID</param>
        /// <param name="EnterName">企业名称</param>
        /// <param name="CustomerType">客户类型</param>
        /// <param name="Relationship">关系等级</param>
        /// <param name="Phase">阶段</param>
        /// <param name="ValueGrade">价值评估</param>
        /// <param name="Source">客户来源</param>
        /// <param name="IsHeat">是否热点客户</param>
        /// <param name="DegreeOfHeat">热度</param>
        /// <param name="Province">省份</param>
        /// <param name="City">城市</param>
        /// <param name="UpdateTime">最近更新时间</param>
        /// <returns></returns>
        public async Task<Tuple<List<EnterCustomer>, long>> GetAllAsync(int pageIndex, int pageSize, int? userid = null, string EnterName = null, CustomerTypeEnum? CustomerType = null, RelationshipEnume? Relationship = null, PhaseEnume? Phase = null, ValueGradeEnume? ValueGrade = null, CustSource? Source = null, bool? IsHeat = null, DegreeOfHeatEnume? DegreeOfHeat = null, string Province = null, string City = null, DateTime? UpdateTime = null, bool? timeType = null, List<int> idList = null)
        {
            List<EnterCustomer> list = new List<EnterCustomer>();
            long count = 0;
            try
            {
                await Task.Run(() => {
                    DataContext dataContext = new DataContext();
                    var result = dataContext.EnterCustomers.Where(x => x.State == StateEnum.Invalid);
                    if (userid.HasValue)
                        result = result.Where(x => x.UserID == userid);
                    if (!EnterName.IsNull())
                        result = result.Where(x => x.EnterName.Contains(EnterName));
                    if (CustomerType.HasValue)
                        result = result.Where(x => x.CustomerType == CustomerType);
                    if (Phase.HasValue)
                        result = result.Where(x => x.Phase == Phase);
                    if (IsHeat.HasValue)
                        result = result.Where(x => x.IsHeat == IsHeat);
                    if (DegreeOfHeat.HasValue)
                        result = result.Where(x => x.DegreeOfHeat == DegreeOfHeat);
                    if (!Province.IsNull())
                        result = result.Where(x => x.Province == Province);
                    if (!City.IsNull())
                        result = result.Where(x => x.City == City);
                    if (timeType.HasValue && timeType == true)
                    {
                        if (UpdateTime.HasValue)
                            result = result.Where(x => x.UpdateTime > UpdateTime);
                    }
                    else
                    {
                        if (UpdateTime.HasValue)
                            result = result.Where(x => x.UpdateTime < UpdateTime);
                    }
                    if (Relationship.HasValue)
                        result = result.Where(x => x.Relationship == Relationship);
                    if (ValueGrade.HasValue)
                        result = result.Where(x => x.ValueGrade == ValueGrade);
                    if (Source.HasValue)
                        result = result.Where(x => x.Source == Source);
                    if (idList != null && idList.Count > 0)
                        result = result.Where(x => idList.Contains(x.ID));

                    list = result.OrderByDescending(x => x.UpdateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    count = result.LongCount();
                });
            }
            catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.GetAsync", ex);
            }
            return Tuple.Create(list, count);
        }

        public async Task<List<EnterCustomer>> ListAsync(DateTime startTime, DateTime endTime, int userid)
        {
            List<EnterCustomer> list = new List<EnterCustomer>();
            try
            {
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    list = dataContext.EnterCustomers.Where(x => x.UserID == userid && x.CreateTime > startTime && x.CreateTime < endTime).ToList();
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.ListAsync",ex);
            }
            return list;
        }
        
        /// <summary>
        /// 统计用户录入客户数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<UserEnterReport>> GetAsync(DateTime startTime,DateTime endTime,int userid)
        {
            try
            {
                List<UserEnterReport> list = new List<UserEnterReport>();
                await Task.Run(() => {
                    DataContext dataContext = new DataContext();
                    var query = dataContext.EnterCustomers.Where(x => x.CreateTime > startTime && x.CreateTime < endTime);
                    if (userid>0)
                    {
                        query = query.Where(x => x.UserID == userid);
                    }
                    var report = from en in query.ToList()
                                 group en by en.UserID into g
                                 select new {
                                     UserID = g.Key,
                                     Total = g.Count()
                                 };
                    if (report!=null)
                    {
                        foreach (var r in report)
                        {
                            UserEnterReport enterReport = new UserEnterReport();
                            enterReport.Total = r.Total;
                            enterReport.UserID = r.UserID;
                            list.Add(enterReport);
                        }
                    }
                });
                return list;
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.GetAsync",ex);
            }
            return null;
        }
        /// <summary>
        /// 按照时间和用户ID分组统计用户信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userid"></param>
        /// <param name="type">最小时间单元：1.天；2.周；3.月</param>
        /// <returns></returns>
        public async Task<List<EnterTotalReportForDay>> GetReportForDayAsync(DateTime startTime, DateTime endTime, int userid,int type)
        {
            List<EnterTotalReportForDay> list = new List<EnterTotalReportForDay>();
            return list;
        }

        public async Task DelAsync(EnterCustomer enterCustomer)
        {
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.Remove(enterCustomer);
                await dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ClassLoger.Error("EnterCustomerDal.DelAsync", ex);
            }
        }
    }
}
