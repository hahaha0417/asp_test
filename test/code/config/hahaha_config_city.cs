


using Emgu.CV.Aruco;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace asp_base
{
    public class hahaha_config
    {
        // ---------------------------------------------------------------
        // 固定的設定
        // ---------------------------------------------------------------
        public Dictionary<string, string> Config_String_ = new Dictionary<string, string>();

        public Dictionary<string, List<string>> Config_List_ = new Dictionary<string, List<string>>();

        public Dictionary<string, Lazy<Dictionary<string, string>>> Config_Dictionary_ = new Dictionary<string, Lazy<Dictionary<string, string>>>();

        public Dictionary<string, List<KeyValuePair<string, string>>> Config_Pair_ = new Dictionary<string, List<KeyValuePair<string, string>>>();

        
        public void Initial()
        {
            Config_String_.Add("xxx", "xxx");

            Config_List_.Add("xxx", new List<string>() { 
                "xxx",
                "yyy",
            });

            Config_Dictionary_.Add("xxx", new Lazy<Dictionary<string, string>>(() => {
                return new Dictionary<string, string>()
                {
                    { "aaa", "111" },
                    { "bbb", "222" },
                };
            }));

            Config_Dictionary_.Add("yyy", new Lazy<Dictionary<string, string>>(() => {
                return new Dictionary<string, string>()
                {
                    { "aaa", "111" },
                    { "bbb", "222" },
                };
            }));

            Config_Pair_.Add("xxx", new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>( "aaa", "111" ),
                new KeyValuePair<string, string>( "bbb", "222" ),
            });

            
            {
                string xxx = (string)Config_String_["xxx"].Clone();
            }

            {
                List<string> xxx = Config_List_["xxx"].ToList();
            }

            //{
            //    List<string> xxx = Config_Dictionary_["xxx"].Value.Values.ToList();
            //}

            {
                List<string> xxx = Config_Dictionary_["yyy"].Value.Values.ToList();
            }

            {
                Dictionary<string, string> xxx = new Dictionary<string, string>(Config_Dictionary_["xxx"].Value);
                xxx.Remove("aaa");
                int rrr = 0;
            }

            {
                List<KeyValuePair<string, string>> xxx = Config_Pair_["xxx"].ToList();
                // 刪除 index = 1 的元素（第二個）
                xxx.RemoveAt(1);
            }
        }
    }
}