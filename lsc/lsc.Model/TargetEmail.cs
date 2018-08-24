using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 目标邮箱
    /// </summary>
    [Serializable]
    public class TargetEmail
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(64)]
        public string Email { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
