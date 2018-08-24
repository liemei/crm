using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 用户答题记录
    /// </summary>
    [Serializable]
    public class UserAnswerLog
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 总分
        /// </summary>
        public double TotalScore { get; set; }
        /// <summary>
        /// 耗时
        /// </summary>
        public double Duration { get; set; }
    }
}
