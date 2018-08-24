using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lsc.crm.ViewModel
{
    [Serializable]
    public class UserViewModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string TelPhone { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}
