using lsc.Common;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace lsc.Dal
{
    public class WorkPlanDal
    {
        private static WorkPlanDal _Ins;
        public static WorkPlanDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new WorkPlanDal();
                return _Ins;
            }
        }

        public async Task<int> AddAsync(WorkPlan workPlan)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var info = await dataContext.WorkPlans.AddAsync(workPlan);
                await dataContext.SaveChangesAsync();
                id = info.Entity.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("WorkPlanDal.AddAsync",ex);
            }
            return id;
        }

        public async Task DelAsync(WorkPlan workPlan)
        {
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.WorkPlans.Remove(workPlan);
                await dataContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                ClassLoger.Error("WorkPlanDal.DelAsync",ex);
            }
        }

        public async Task UpdateAsync(WorkPlan workPlan)
        {
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.WorkPlans.Update(workPlan);
                await dataContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                ClassLoger.Error("WorkPlanDal.UpdateAsync",ex);
            }
        }

        public async Task<WorkPlan> GetAsync(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info =await dataContext.WorkPlans.FindAsync(id);
                return info;
            }
            catch(Exception ex)
            {
                ClassLoger.Error("WorkPlanDal.GetAsync",ex);
            }
            return null;
        }
        /// <summary>
        /// 获取最近五天的计划
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<WorkPlan>> ListAsync(int userid)
        {
            List<WorkPlan> list = null;
            try
            {
                DataContext dataContext = new DataContext();
                await Task.Run(()=> {
                    list = dataContext.WorkPlans.Where(x => x.UserID == userid && x.WorkPlanState == Model.Enume.WorkPlanStateEnum.NoFinish && x.PlanTime > DateTime.Now.AddDays(-5) && x.PlanTime < DateTime.Now.AddDays(1)).OrderByDescending(x=>x.ID).ToList();
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("WorkPlanDal.ListAsync",ex);
            }
            return list;
        }
        /// <summary>
        /// 分页获取工作计划
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Tuple<List<WorkPlan>, long>> TupleAsync(int userid,int pageIndex,int pageSize)
        {
            List<WorkPlan> list = null;
            long count = 0;
            try
            {
                DataContext dataContext = new DataContext();
                await Task.Run(() => 
                {
                    list = dataContext.WorkPlans.Where(x => x.UserID == userid).OrderBy(x => x.WorkPlanState).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    count = dataContext.WorkPlans.Where(x => x.UserID == userid).LongCount();
                });
            } catch (Exception ex)
            {
                ClassLoger.Error("WorkPlanDal.TupleAsync",ex);
            }
            return Tuple.Create(list,count);
        }
            
    }
}
