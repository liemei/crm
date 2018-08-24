using System;
using System.Collections.Generic;
using System.Text;

namespace lsc.Model.Enume
{
    /// <summary>
    /// 项目状态
    /// </summary>
    public enum ProjectStateEnum
    {
        /// <summary>
        /// 进行中
        /// </summary>
        Ongoing=0,
        /// <summary>
        /// 成功
        /// </summary>
        Success =1,
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 2,
        /// <summary>
        /// 搁置
        /// </summary>
        Shelve = 3,
        /// <summary>
        /// 放弃
        /// </summary>
        Abandon = 4
    }
}
