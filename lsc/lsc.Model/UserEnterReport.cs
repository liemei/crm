using System;
using System.Collections.Generic;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 用户数量相关统计
    /// </summary>
    public class UserEnterReport
    {
        public int UserID { get; set; }
        public int Total { get; set; }
    }

    public class EnterTotalReportForDay
    {
        public string Days { get; set; }

        public int UserID { get; set; }

        public int Total { get; set; }
    }

}
