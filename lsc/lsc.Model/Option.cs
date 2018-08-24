using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 选项
    /// </summary>
    [Serializable]
    public class Option
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 考题Id
        /// </summary>
        public int QuestionsId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(126)]
        public string Content { get; set; }
        /// <summary>
        /// 选项索引A/B
        /// </summary>
        [StringLength(2)]
        public string ItemIndex { get; set; }
        /// <summary>
        /// 是否是正确答案
        /// </summary>
        public bool IsOk { get; set; }
    }
}
