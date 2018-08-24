using bnuxq.Common;
using bnuxq.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace bnuxq.Dal
{
    public class DataContext: DbContext
    {
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<UserInfo> Users { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public DbSet<ModuleInfo> ModuleInfos { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public DbSet<UserRoleJurisdiction> UserRoleJurisdictions { get; set; }
        /// <summary>
        /// 用户账户
        /// </summary>
        public DbSet<UserAccount> UserAccounts { get; set; }
        /// <summary>
        /// 企业客户
        /// </summary>
        public DbSet<EnterCustomer> EnterCustomers { get; set; }
        /// <summary>
        /// 企业客户联系人
        /// </summary>
        public DbSet<EnterCustContacts> EnterCustContactss { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public DbSet<DistrictInfo> Districtinfos { get; set; }
        /// <summary>
        /// 客户所处阶段更新日志
        /// </summary>
        public DbSet<EnterCustPhaseLog> EnterCustPhaseLogs { get; set; }
        /// <summary>
        /// 销售项目
        /// </summary>
        public DbSet<SalesProject> SalesProjects { get; set; }
        /// <summary>
        /// 销售项目状态变更日志
        /// </summary>
        public DbSet<SalesProjectStateLog> SalesProjectStateLogs { get; set; }
        /// <summary>
        /// 工作计划
        /// </summary>
        public DbSet<WorkPlan> WorkPlans { get; set; }
        /// <summary>
        /// 销售项目回款记录
        /// </summary>
        public DbSet<ReceivedPaymentsLog> ReceivedPaymentsLogs { get; set; }
        /// <summary>
        /// 考题
        /// </summary>
        public DbSet<Questions> QuestionsDbSet { get; set; }
        /// <summary>
        /// 选项
        /// </summary>
        public DbSet<Option> Options { get; set; }
        /// <summary>
        /// 用户答题记录
        /// </summary>
        public DbSet<UserAnswerLog> UserAnswerLog { get; set; }
        /// <summary>
        /// 用户答题的题目
        /// </summary>
        public DbSet<UserQuestions> UserQuestions { get; set; }
        /// <summary>
        /// 用户答案
        /// </summary>
        public DbSet<UserAnswer> UserAnswer { get; set; }
        /// <summary>
        /// 邮件资源
        /// </summary>
        public DbSet<EmailResources> EmailResourcess { get; set; }
        /// <summary>
        /// 目标邮箱
        /// </summary>
        public DbSet<TargetEmail> TargetEmails { get; set; }
        /// <summary>
        /// 邮箱模板
        /// </summary>
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        /// <summary>
        /// 邮件发送任务
        /// </summary>
        public DbSet<SendEmailTask> SendEmailTasks { get; set; }
        /// <summary>
        /// 邮件发送日志
        /// </summary>
        public DbSet<SendEmailLog> SendEmailLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(SystemSet.MysqlConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
