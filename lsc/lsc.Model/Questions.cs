using System;
using System.Collections.Generic;
using System.Text;
using bnuxq.Model.Enume;
using System.ComponentModel.DataAnnotations;

namespace bnuxq.Model
{
    /// <summary>
    /// 考题
    /// </summary>
    [Serializable]
    public class Questions
    {
        [Key]
        public int Id { get; set; }
        [StringLength(126)]
        public string Content { get; set; }
        /// <summary>
        /// 考题类型
        /// </summary>
        public QuestionsTypeEnum QuestionsType { get; set; }
    }
}
