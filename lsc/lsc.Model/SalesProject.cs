using lsc.Model.Enume;
using System;
using System.Collections.Generic;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 销售项目
    /// </summary>
    [Serializable]
    public class SalesProject
    {
        public int ID { get; set; }
        /// <summary>
        /// 项目标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int EnterCustomerID { get; set; }
        /// <summary>
        /// 录入人员ID
        /// </summary>
        public int CreateUserID { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 项目概要
        /// </summary>
        public string ProjectAbstract { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public ProjectStateEnum ProjectState { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public ProjectTypeEnum ProjectType { get; set; }
        /// <summary>
        /// 项目负责人ID
        /// </summary>
        public int HeadID { get; set; }
        /// <summary>
        /// 立项时间
        /// </summary>
        public DateTime ProjectTime { get; set; }
        /// <summary>
        /// 项目金额
        /// </summary>
        public double ProjectAmt { get; set; }
        /// <summary>
        /// 回款金额
        /// </summary>
        public double ReceoverPay { get; set; }
        /// <summary>
        /// 预计到款时间
        /// </summary>
        public DateTime ReceoverPayTime { get; set; }
    }
}
