using lsc.Dal;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lsc.Bll
{
    /// <summary>
    /// 销售项目状态变更日志
    /// </summary>
    public class SalesProjectStateLogBll
    {
        public async Task<int> AddAsync(SalesProjectStateLog salesProjectStateLog)
        {
            return await SalesProjectStateLogDal.Ins.AddAsync(salesProjectStateLog);
        }

        public async Task<List<SalesProjectStateLog>> GetListAsync(int SalesProjectID)
        {
            return await SalesProjectStateLogDal.Ins.GetListAsync(SalesProjectID);
        }
    }
}
