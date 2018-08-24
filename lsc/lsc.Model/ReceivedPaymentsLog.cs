using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace bnuxq.Model
{
    /// <summary>
    /// 销售项目回款记录
    /// </summary>
    [Serializable]
    public class ReceivedPaymentsLog
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 销售项目ID
        /// </summary>
        public int SalesProjectID { get; set; }
        /// <summary>
        /// 回款金额
        /// </summary>
        public double Amt { get; set; }

        public int UserID { get; set; }

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Rem { get; set; }
    }
}
