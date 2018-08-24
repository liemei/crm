using bnuxq.Common;
using bnuxq.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace bnuxq.Dal
{
    /// <summary>
    /// 模块相关操作
    /// </summary>
    public class ModuleInfoDal
    {
        private static ModuleInfoDal _Ins;
        public static ModuleInfoDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new ModuleInfoDal();
                return _Ins;
            }
        }

        public async Task<int> AddModuleInfo(ModuleInfo module)
        {
            int id = 0;
            try
            {
                DataContext db = new DataContext();
                var moduleinfo = await db.ModuleInfos.AddAsync(module);
                await db.SaveChangesAsync();
                id = moduleinfo.Entity.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("ModuleInfoDal.AddModuleInfo",ex);
            }
            return id;
        }

        public List<ModuleInfo> GetList()
        {
            try
            {
                List<ModuleInfo> list = null;
                DataContext db = new DataContext();
                list = db.ModuleInfos.ToList();
                return list;
            } catch (Exception ex)
            {
                ClassLoger.Error("ModuleInfoDal.GetList",ex);
            }
            return null;
        }

        public async Task DelModel(ModuleInfo module)
        {
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.ModuleInfos.Remove(module);
                await dataContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                ClassLoger.Error("ModuleInfoDal.DelModel",ex);
            }
        }

        public async Task<ModuleInfo> Get(int id)
        {
            try
            {
                DataContext db = new DataContext();
                var info = await db.ModuleInfos.FindAsync(id);
                return info;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("ModuleInfoDal.GetList", ex);
            }
            return null;
        }
    }
}
