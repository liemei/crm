using bnuxq.Model.Enume;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    public class UserRole
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(32)]
        public string RoleName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StateEnum State { get; set; }
    }
}
