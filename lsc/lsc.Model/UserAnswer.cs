using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 用户答案
    /// </summary>
    [Serializable]
    public class UserAnswer
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 用户答题记录Id
        /// </summary>
        public int LogId { get; set; }
        /// <summary>
        /// 题目编号
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 选项编号Id
        /// </summary>
        public int OptionId { get; set; }
        /// <summary>
        /// 是否正确
        /// </summary>
        public bool IsOk { get; set; }
        /// <summary>
        /// 回答的内容，如果是问答题使用此字段
        /// </summary>
        [StringLength(500)]
        public string Content { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public double Score { get; set; }
    }
}
