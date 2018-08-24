using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bnuxq.Model
{
    /// <summary>
    /// 地区
    /// </summary>
    [Serializable]
    public class DistrictInfo
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        [StringLength(64)]
        public string Name { get; set; }
        /// <summary>
        /// 上级名称
        /// </summary>
        public int Pid { get; set; }
    }
}
