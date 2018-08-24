using lsc.Model.Enume;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 客户阶段更新日志
    /// </summary>
    [Serializable]
    public class EnterCustPhaseLog
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        public string UserName { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int EnterCustomerID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public PhaseEnume Phase { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Rem { get; set; }
    }
}
