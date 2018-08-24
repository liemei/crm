using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lsc.Model
{
    [Serializable]
    public class UserAccount
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        [StringLength(32)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string Password { get; set; }
    }
}
