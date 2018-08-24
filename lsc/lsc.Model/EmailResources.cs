using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 邮件资源
    /// </summary>
    [Serializable]
    public class EmailResources
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(64)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(11)]
        public string Password { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        [MaxLength(6)]
        public string Port { get; set; }
        /// <summary>
        /// 邮件服务器IP
        /// </summary>
        [MaxLength(64)]
        public string SenderServerIp { get; set; }
        /// <summary>
        /// 发送邮件的邮箱
        /// </summary>
        [MaxLength(64)]
        public string Email { get; set; }
    }
}
