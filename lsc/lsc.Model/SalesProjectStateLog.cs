using lsc.Model.Enume;
using System;
using System.Collections.Generic;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 销售项目状态变更日志
    /// </summary>
    [Serializable]
    public class SalesProjectStateLog
    {
        public int ID { get; set; }
        /// <summary>
        /// 销售项目ID
        /// </summary>
        public int SalesProjectID { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public ProjectStateEnum ProjectState { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Rem { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
