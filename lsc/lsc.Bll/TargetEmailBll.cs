using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bnuxq.Dal;
using bnuxq.Model;

namespace bnuxq.Bll
{
    public class TargetEmailBll
    {
        public async Task<int> AddAsync(TargetEmail targetEmail)
        {
            return await TargetEmailDal.Ins.AddAsync(targetEmail);
        }

        public int Add(TargetEmail targetEmail)
        {
            return TargetEmailDal.Ins.Add(targetEmail);
        }

        public async Task<int> UpdateAsync(TargetEmail targetEmail)
        {
            return await TargetEmailDal.Ins.UpdateAsync(targetEmail);
        }

        public async Task<TargetEmail> Get(int id)
        {
            return await TargetEmailDal.Ins.Get(id);
        }

        public async Task<Tuple<List<TargetEmail>, long>> GetList(int pageIndex, int pageSize)
        {
            return await TargetEmailDal.Ins.GetList(pageIndex, pageSize);
        }

        public async Task<bool> DelAsync(TargetEmail targetEmail)
        {
            return await TargetEmailDal.Ins.DelAsync(targetEmail);
        }

        public async Task<bool> Exists(string email)
        {
            return await TargetEmailDal.Ins.Exists(email);
        }
    }
}
