using System;
using System.Collections.Generic;
using System.Text;

namespace bnuxq.Model.Enume
{
    /// <summary>
    /// 客户来源
    /// </summary>
    public enum CustSource
    {
        /// <summary>
        /// 客户来电
        /// </summary>
        CustTelephone=1,
        /// <summary>
        /// 主动挖掘
        /// </summary>
        Excavate=2,
        /// <summary>
        /// 网站咨询
        /// </summary>
        WebConsulting=3,
        /// <summary>
        /// 客户介绍
        /// </summary>
        Introduction=4,
        /// <summary>
        /// 招标
        /// </summary>
        Tender = 6,
        /// <summary>
        /// 展会
        /// </summary>
        Exhibition = 7,
        /// <summary>
        /// QQ群&微信群
        /// </summary>
        QQqun = 8,
        /// <summary>
        /// 其他来源
        /// </summary>
        Other = 5
    }
}
