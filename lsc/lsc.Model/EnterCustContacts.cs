using bnuxq.Model.Enume;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 联系人
    /// </summary>
    [Serializable]
    public class EnterCustContacts
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 企业客户ID
        /// </summary>
        public int EnterCustID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        public SexEnum Sex { get; set; }
        /// <summary>
        /// 负责业务
        /// </summary>
        public string Business { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Duties { get; set; }

        public string Telephone { get; set; }

        public string Landline { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChart { get; set; }

        public string Email { get; set; }

        public string QQ { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Rem { get; set; }
    }
}
