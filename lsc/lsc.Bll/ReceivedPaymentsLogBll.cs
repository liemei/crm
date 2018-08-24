using lsc.Dal;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lsc.Bll
{
    /// <summary>
    /// 回款记录相关操作
    /// </summary>
    public class ReceivedPaymentsLogBll
    {
        public async Task<int> AddAsync(ReceivedPaymentsLog log)
        {
            return await ReceivedPaymentsLogDal.Ins.AddAsync(log);
        }

        public async Task<List<ReceivedPaymentsLog>> GetListAsync(DateTime startTime, DateTime endTime, int userid)
        {
            return await ReceivedPaymentsLogDal.Ins.GetListAsync(startTime,endTime,userid);
        }

        public async Task<List<ReceivedPaymentsLog>> GetListAsync(int SalesProjectID)
        {
            return await ReceivedPaymentsLogDal.Ins.GetListAsync(SalesProjectID);
        }
    }
}
