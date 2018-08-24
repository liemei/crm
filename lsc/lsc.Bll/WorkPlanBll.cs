using lsc.Dal;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lsc.Bll
{
    /// <summary>
    /// 工作计划相关操作
    /// </summary>
    public class WorkPlanBll
    {
        public async Task<int> AddAsync(WorkPlan workPlan)
        {
            return await WorkPlanDal.Ins.AddAsync(workPlan);
        }

        public async Task DelAsync(WorkPlan workPlan)
        {
            await WorkPlanDal.Ins.DelAsync(workPlan);
        }
        public async Task UpdateAsync(WorkPlan workPlan)
        {
            await WorkPlanDal.Ins.UpdateAsync(workPlan);
        }
        public async Task<WorkPlan> GetAsync(int id)
        {
            return await WorkPlanDal.Ins.GetAsync(id);
        }

        public async Task<Tuple<List<WorkPlan>, long>> TupleAsync(int userid, int pageIndex, int pageSize)
        {
            return await WorkPlanDal.Ins.TupleAsync(userid,pageIndex,pageSize);
        }
        /// <summary>
        /// 获取最近五天未完成的计划
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<WorkPlan>> ListAsync(int userid)
        {
            return await WorkPlanDal.Ins.ListAsync(userid);
        }
    }
}
