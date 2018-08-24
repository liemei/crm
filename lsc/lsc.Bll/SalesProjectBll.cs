using bnuxq.Dal;
using bnuxq.Model;
using bnuxq.Model.Enume;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bnuxq.Bll
{
    /// <summary>
    /// 销售项目相关操作
    /// </summary>
    public class SalesProjectBll
    {
        public async Task<int> AddAsync(SalesProject salesProject)
        {
            return await SalesProjectDal.Ins.AddAsync(salesProject);
        }

        public async Task<bool> UpdateAsync(SalesProject salesProject)
        {
            return await SalesProjectDal.Ins.UpdateAsync(salesProject);
        }

        public async Task<SalesProject> GetAsync(int id)
        {
            return await SalesProjectDal.Ins.GetAsync(id);
        }
        /// <summary>
        /// 综合查询销售项目
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="CreateUserID">创建者用户ID</param>
        /// <param name="title">销售项目标题</param>
        /// <param name="EnterCustomerID">客户ID</param>
        /// <param name="ProjectState"></param>
        /// <param name="ProjectType"></param>
        /// <param name="ProjectStartTime"></param>
        /// <param name="ProjectEndTime"></param>
        /// <returns></returns>
        public async Task<Tuple<List<SalesProject>, long>> GetTupleAsync(int pageIndex, int pageSize, int? CreateUserID = null, string title = null, int? EnterCustomerID = null, ProjectStateEnum? ProjectState = null, ProjectTypeEnum? ProjectType = null, DateTime? ProjectStartTime = null, DateTime? ProjectEndTime = null)
        {
            return await SalesProjectDal.Ins.GetTupleAsync(pageIndex,pageSize,CreateUserID,title,EnterCustomerID,ProjectState,ProjectType,ProjectStartTime,ProjectEndTime);
        }
        /// <summary>
        /// 统计一段时间内的成单量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<UserEnterReport>> GetAsync(DateTime startTime, DateTime endTime, int userid)
        {
            return await SalesProjectDal.Ins.GetAsync(startTime,endTime,userid);
        }
        /// <summary>
        /// 根据用户ID获取本月应该到款的项目
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<SalesProject>> GetListAsync(int userid, DateTime startTime, DateTime endTime)
        {
            return await SalesProjectDal.Ins.GetListAsync(userid,startTime,endTime);
        }
        /// <summary>
        /// 根据客户ID获取客户的项目
        /// </summary>
        /// <param name="EnterCustomerID"></param>
        /// <returns></returns>
        public async Task<List<SalesProject>> GetListAsync(int EnterCustomerID)
        {
            return await SalesProjectDal.Ins.GetListAsync(EnterCustomerID);
        }
    }
}
