using bnuxq.Dal;
using bnuxq.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bnuxq.Bll
{
    /// <summary>
    /// 客户状态改变日志相关操作
    /// </summary>
    public class EnterCustPhaseLogBll
    {
        public async Task<int> AddAsync(EnterCustPhaseLog log)
        {
            return await EnterCustPhaseLogDal.Ins.AddAsync(log);
        }

        public async Task<List<EnterCustPhaseLog>> ListAsync(int EnterCustomerID)
        {
            return await EnterCustPhaseLogDal.Ins.ListAsync(EnterCustomerID);
        }
        public async Task<List<EnterCustPhaseLog>> ListAsync(DateTime startTime, DateTime endTime, int UserID)
        {
            return await EnterCustPhaseLogDal.Ins.ListAsync(startTime,endTime,UserID);
        }
        public async Task<List<UserEnterReport>> GetAsync(DateTime startTime, DateTime endTime, int UserID)
        {
            return await EnterCustPhaseLogDal.Ins.GetAsync(startTime,endTime,UserID);
        }
        /// <summary>
        /// 统计一段时间内每个销售员每天的业绩
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<EnterTotalReportForDay>> GetReportAsync(DateTime startTime, DateTime endTime, int? userid = null)
        {
            return await EnterCustPhaseLogDal.Ins.GetReportAsync(startTime,endTime, userid);
        }
    }
}
