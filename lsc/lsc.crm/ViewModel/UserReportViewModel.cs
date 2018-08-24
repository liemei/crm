using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnuxq.crm.ViewModel
{
    public class UserReportViewModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }
        /// <summary>
        /// 客户量统计
        /// </summary>
        public int CustomorTotal { get; set; }
        /// <summary>
        /// 成单量统计
        /// </summary>
        public int SalesProjectTotal { get; set; }
        /// <summary>
        /// 电话量统计
        /// </summary>
        public int PhoneTotal { get; set; }
        /// <summary>
        /// 应收账款
        /// </summary>
        public double ReceoverPay { get; set; }
        /// <summary>
        /// 已收账款
        /// </summary>
        public double HReceoverPay { get; set; }
        /// <summary>
        /// 月目标账款
        /// </summary>
        public double TargetAmt { get; set; }
    }
}
