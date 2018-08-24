using lsc.Model.Enume;
using System;
using System.Collections.Generic;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 工作计划
    /// </summary>
    [Serializable]
    public class WorkPlan
    {
        public int ID { get; set; }
        /// <summary>
        /// 计划内容
        /// </summary>
        public string PlanContent { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int EnterCustID { get; set; }

        public DateTime CreateTime { get; set; }

        public int UserID { get; set; }

        public DateTime PlanTime { get; set; }

        public WorkPlanStateEnum WorkPlanState { get; set; }
    }
}
