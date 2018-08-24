using lsc.Dal;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lsc.Bll
{
    public class DistrictInfoBll
    {
        public async Task<List<DistrictInfo>> GetAsync(int Pid)
        {
             return await DistrictInfoDal.Ins.GetAsync(Pid);
        }
    }
}
