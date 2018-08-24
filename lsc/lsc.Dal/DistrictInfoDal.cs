using lsc.Common;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace lsc.Dal
{
    public class DistrictInfoDal
    {
        private static DistrictInfoDal _Ins;
        public static DistrictInfoDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new DistrictInfoDal();
                return _Ins;
            }
        }

        public async Task<List<DistrictInfo>> GetAsync(int Pid)
        {
            List<DistrictInfo> list = null;
            try
            {
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    list = dataContext.Districtinfos.Where(x => x.Pid == Pid).ToList();
                });
            }
            catch (Exception ex)
            {
                ClassLoger.Error("DistrictInfoDal.GetAsync", ex);
            }
            return list;
        }


    }
}
