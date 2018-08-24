using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 邮件模板
    /// </summary>
    [Serializable]
    public class EmailTemplate
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 邮件模板标题
        /// </summary>
        [MaxLength(256)]
        public string Title { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        [MaxLength(2048)]
        public string EmailContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
