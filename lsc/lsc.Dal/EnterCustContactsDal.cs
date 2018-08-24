using lsc.Common;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lsc.Dal
{
    /// <summary>
    /// 企业联系人相关操作
    /// </summary>
    public class EnterCustContactsDal
    {
        private static EnterCustContactsDal _Ins;
        public static EnterCustContactsDal Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new EnterCustContactsDal();
                return _Ins;
            }
        }

        public async Task<int> Add(EnterCustContacts ecc)
        {
            int id = 0;
            try
            {
                DataContext dataContext = new DataContext();
                var user = await dataContext.EnterCustContactss.AddAsync(ecc);
                await dataContext.SaveChangesAsync();
                id = user.Entity.ID;
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustContactsDal.Add",ex);
            }
            return id;
        }

        public async Task<bool> UpdateAsync(EnterCustContacts ecc)
        {
            bool flag = false;
            try
            {
                DataContext dataContext = new DataContext();
                dataContext.EnterCustContactss.Update(ecc);
                await dataContext.SaveChangesAsync();
                flag = true;
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustContactsDal.UpdateAsync",ex);
            }
            return flag;
        }

        public async Task<EnterCustContacts> GetAsync(int id)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var info =await dataContext.EnterCustContactss.FindAsync(id);
                return info;
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustContactsDal.GetAsync",ex);
            }
            return null;
        }
        /// <summary>
        /// 根据客户ID获取客户联系人信息
        /// </summary>
        /// <param name="EnterCustID"></param>
        /// <returns></returns>
        public async Task<List<EnterCustContacts>> GetListAsync(int EnterCustID)
        {
            try
            {
                List<EnterCustContacts> list = null;
                await Task.Run(()=> {
                    DataContext dataContext = new DataContext();
                    list = dataContext.EnterCustContactss.Where(x => x.EnterCustID == EnterCustID).OrderByDescending(x => x.ID).ToList();
                });
                return list;
            } catch (Exception ex)
            {
                ClassLoger.Error("EnterCustContactsDal.GetListAsync",ex);
            }
            return null;
        }

        public async Task<List<EnterCustContacts>> GetListAsync(string telphone,string qq)
        {
            try
            {
                DataContext dataContext = new DataContext();
                var list =await dataContext.EnterCustContactss.Where(x => x.Telephone == telphone || x.QQ==qq).OrderByDescending(x => x.ID).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                ClassLoger.Error("EnterCustContactsDal.GetListAsync", ex);
            }
            return null;
        }
    }
}
