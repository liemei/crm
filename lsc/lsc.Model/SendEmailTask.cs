using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 邮件发送任务
    /// </summary>
    [Serializable]
    public class SendEmailTask
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [MaxLength(256)]
        public string TaskName { get; set; }
        /// <summary>
        /// 邮件模板Id
        /// </summary>
        public int EmailTempId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
