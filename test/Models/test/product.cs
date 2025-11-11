using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace asp_base.Models.test
{
    
    public class product
    {
        [Key]
        public int id { get; set; }

        [Required] // 不可為 null
        [MaxLength(100)] // 最多 100 字元
        [DefaultValue("hahaha")]
        public string name { get; set; }

        [Column(TypeName = "decimal(10,2)")] // 精度與小數位數
        [DefaultValue(99.99)]
        public decimal price { get; set; }

        [Index(nameof(category_id))] // 為 Name 建立索引
        //[Index(nameof(CategoryId), nameof(Name))] // 複合索引
        // 外鍵欄位
        [ForeignKey("category")]  // 指向下方的 Category 導覽屬性
        [Range(0, 99999999)] // 限制最大 8 位數

        public int category_id { get; set; } // 外鍵

        public category category { get; set; } // 導覽屬性
    }


}