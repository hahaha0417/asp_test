using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace asp_base.Models.test
{
    public class category
    {
        [Key]
        public int id { get; set; }

        [Required] // 不可為 null
        [MaxLength(100)] // 最多 100 字元
        [DefaultValue("hahaha")]
        public string name { get; set; }

        [DefaultValue(false)]
        public bool is_active { get; set; } // 一般布林值

        public List<product> products { get; set; } // 一對多
    }


}