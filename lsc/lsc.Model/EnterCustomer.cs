using lsc.Model.Enume;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lsc.Model
{
    /// <summary>
    /// 企业客户
    /// </summary>
    [Serializable]
    public class EnterCustomer
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 企业客户名称
        /// </summary>
        [StringLength(126)]
        public string EnterName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string Abbreviation { get; set; }
        /// <summary>
        /// 企业客户类型
        /// </summary>
        public CustomerTypeEnum CustomerType { get; set; }
        /// <summary>
        /// 关系等级
        /// </summary>
        public RelationshipEnume Relationship { get; set; }
        /// <summary>
        /// 价值评估
        /// </summary>
        public ValueGradeEnume ValueGrade { get; set; }
        /// <summary>
        /// 客户来源
        /// </summary>
        public CustSource Source { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public PhaseEnume Phase { get; set; }
        /// <summary>
        /// 是否是热点客户
        /// </summary>
        public bool IsHeat { get; set; }
        /// <summary>
        /// 热度
        /// </summary>
        public DegreeOfHeatEnume DegreeOfHeat { get; set; }
        /// <summary>
        /// 热点分类
        /// </summary>
        public HeatTypeEnum HeatTYPE { get; set; }
        /// <summary>
        /// 热点说明
        /// </summary>
        [StringLength(256)]
        public string HeatMsg { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [StringLength(32)]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [StringLength(32)]
        public string City { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(32)]
        public string Telephone { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        public string Landline { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        [StringLength(32)]
        public string FaxNumber { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [StringLength(32)]
        public string ZipCode { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(32)]
        public string Email { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        [StringLength(126)]
        public string WebSit { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(256)]
        public string Address { get; set; }
        /// <summary>
        /// 开票资料
        /// </summary>
        [StringLength(1024)]
        public string InvoiceMsg { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
        [StringLength(1024)]
        public string CustAbstract { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string Rem { get; set; }
        /// <summary>
        /// 最后追踪时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public int CreateUserID { get; set; }
        /// <summary>
        /// 拥有者ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StateEnum State { get; set; }
    }
}
