using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 用户答题题目
    /// </summary>
    [Serializable]
    public class UserQuestions
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 答题记录Id
        /// </summary>
        public int LogId { get; set; }
        /// <summary>
        ///题目Id
        /// </summary>
        public int QuestionsId { get; set; }
        /// <summary>
        /// 题目索引
        /// </summary>
        public int QIndex { get; set; }
    }
}
