using bnuxq.Dal;
using bnuxq.Model;
using bnuxq.Model.Enume;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bnuxq.Bll
{
    public class EnterCustomerBll
    {
        public async Task<int> AddEnterCustomer(EnterCustomer enterCustomer)
        {
            return await EnterCustomerDal.Ins.AddEnterCustomer(enterCustomer);
        }

        public async Task<bool> UpdateEnterCustomerAsync(EnterCustomer enterCustomer)
        {
            return await EnterCustomerDal.Ins.UpdateEnterCustomerAsync(enterCustomer);
        }

        public async Task<EnterCustomer> GetAsync(int id)
        {
            return await EnterCustomerDal.Ins.GetAsync(id);
        }
        public async Task<EnterCustomer> GetAsync(string EnterName)
        {
            return await EnterCustomerDal.Ins.GetAsync(EnterName);
        }
        /// <summary>
        /// 获取企业客户信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userid">所属用户ID</param>
        /// <param name="EnterName">企业名称</param>
        /// <param name="CustomerType">客户类型</param>
        /// <param name="Relationship">关系等级</param>
        /// <param name="Phase">阶段</param>
        /// <param name="ValueGrade">价值评估</param>
        /// <param name="Source">客户来源</param>
        /// <param name="IsHeat">是否热点客户</param>
        /// <param name="DegreeOfHeat">热度</param>
        /// <param name="Province">省份</param>
        /// <param name="City">城市</param>
        /// <param name="UpdateTime">最近更新时间</param>
        /// <returns></returns>
        public async Task<Tuple<List<EnterCustomer>, long>> GetAsync(int pageIndex, int pageSize, int? userid = null, string EnterName = null, CustomerTypeEnum? CustomerType = null, RelationshipEnume? Relationship = null, PhaseEnume? Phase = null, ValueGradeEnume? ValueGrade = null, CustSource? Source = null, bool? IsHeat = null, DegreeOfHeatEnume? DegreeOfHeat = null, string Province = null, string City = null, DateTime? UpdateTime = null, bool? timeType = null, List<int> idList = null)
        {
            return await EnterCustomerDal.Ins.GetAsync(pageIndex,pageSize,userid,EnterName,CustomerType,Relationship,Phase,ValueGrade,Source,IsHeat,DegreeOfHeat,Province,City,UpdateTime, timeType, idList);
        }

        public async Task<Tuple<List<EnterCustomer>, long>> GetAllAsync(int pageIndex, int pageSize, int? userid = null,
            string EnterName = null, CustomerTypeEnum? CustomerType = null, RelationshipEnume? Relationship = null,
            PhaseEnume? Phase = null, ValueGradeEnume? ValueGrade = null, CustSource? Source = null,
            bool? IsHeat = null, DegreeOfHeatEnume? DegreeOfHeat = null, string Province = null, string City = null,
            DateTime? UpdateTime = null, bool? timeType = null, List<int> idList = null)
        {
            return await EnterCustomerDal.Ins.GetAllAsync(pageIndex, pageSize, userid, EnterName, CustomerType, Relationship, Phase, ValueGrade, Source, IsHeat, DegreeOfHeat, Province, City, UpdateTime, timeType, idList);
        }

        public async Task<List<EnterCustomer>> ListAsync(DateTime startTime, DateTime endTime, int userid)
        {
            return await EnterCustomerDal.Ins.ListAsync(startTime,endTime,userid);
        }
        public async Task<bool> DelAsync(int id)
        {
            bool flag = false;
            var info = await GetAsync(id);
            if (info != null)
            {
                info.State = StateEnum.Valid;
                flag = await UpdateEnterCustomerAsync(info);
            }
            return flag;
        }

        public async Task DelAsync(EnterCustomer enterCustomer)
        {
            await EnterCustomerDal.Ins.DelAsync(enterCustomer);
        }

        public async Task<bool> ExistsEnterNameAsync(int id, string EnterName)
        {
            return await EnterCustomerDal.Ins.ExistsEnterNameAsync(id,EnterName);
        }
        /// <summary>
        /// 统计录入用户数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<UserEnterReport>> GetAsync(DateTime startTime, DateTime endTime, int userid)
        {
            return await EnterCustomerDal.Ins.GetAsync(startTime, endTime, userid);
        }
    }
}
