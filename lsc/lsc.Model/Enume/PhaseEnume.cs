using System;
using System.Collections.Generic;
using System.Text;

namespace bnuxq.Model.Enume
{
    /// <summary>
    /// 阶段
    /// </summary>
    public enum PhaseEnume
    {
        /// <summary>
        /// 售前跟踪
        /// </summary>
        Pre_sale=1,
        /// <summary>
        /// 需求确认
        /// </summary>
        Demand_Confirmation=2,
        /// <summary>
        /// 售中跟单
        /// </summary>
        In_Sales=3,
        /// <summary>
        /// 签约洽谈
        /// </summary>
        Sign_Contract=4,
        /// <summary>
        /// 售后
        /// </summary>
        After_Sale=5,
        /// <summary>
        /// 订单失效
        /// </summary>
        Invalid = 6,
        /// <summary>
        /// 搁置
        /// </summary>
        Shelve = 7,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 8
    }
}
