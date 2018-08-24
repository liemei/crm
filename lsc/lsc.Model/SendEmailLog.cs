using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 邮件发送日志
    /// </summary>
    [Serializable]
    public class SendEmailLog
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 邮件发送任务
        /// </summary>
        public int SendEmailTaskId { get; set; }
        /// <summary>
        /// 邮件模板Id
        /// </summary>
        public int EmailTempId { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        [MaxLength(64)]
        public string Email { get; set; }
        /// <summary>
        /// 目标姓名
        /// </summary>
        [MaxLength(32)]
        public string Name { get; set; }
        /// <summary>
        /// 是否已发送
        /// </summary>
        public bool IsSend { get; set; }
        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool IsSendOk { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }
    }
}
