using bnuxq.Dal;
using bnuxq.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bnuxq.Bll
{
    /// <summary>
    /// 模块相关操作
    /// </summary>
    public class ModuleInfoBll
    {
        public async Task<int> AddModuleInfo(ModuleInfo module)
        {
            return await ModuleInfoDal.Ins.AddModuleInfo(module);
        }

        public List<ModuleInfo> GetList()
        {
            return  ModuleInfoDal.Ins.GetList();
        }
        public async Task DelModel(ModuleInfo module)
        {
            await ModuleInfoDal.Ins.DelModel(module);
        }
        public async Task<ModuleInfo> Get(int id)
        {
            return await ModuleInfoDal.Ins.Get(id);
        }
    }
}
