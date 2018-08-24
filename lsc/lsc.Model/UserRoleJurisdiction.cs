using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [Serializable]
    public class UserRoleJurisdiction
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int UserRoleID { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public int ModuleID { get; set; }
        /// <summary>
        /// 是否有查询权限
        /// </summary>
        public bool IsQuery { get; set; }
        /// <summary>
        /// 是否有添加权限
        /// </summary>
        public bool IsAdd { get; set; }
        /// <summary>
        /// 是否有编辑权限
        /// </summary>
        public bool IsEdit { get; set; }
        /// <summary>
        /// 是否有删除权限
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 分配
        /// </summary>
        public bool IsAssignment { get; set; }
    }
}
