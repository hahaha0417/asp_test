


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
        public Dictionary<string, string> String_ = new Dictionary<string, string>();

        public Dictionary<string, List<string>> List_ = new Dictionary<string, List<string>>();

        public Dictionary<string, Lazy<Dictionary<string, string>>> Dictionary_ = new Dictionary<string, Lazy<Dictionary<string, string>>>();

        public Dictionary<string, List<KeyValuePair<string, string>>> Pair_ = new Dictionary<string, List<KeyValuePair<string, string>>>();

        
        public void Initial()
        {
            String_.Add("xxx", "xxx");

            List_.Add("xxx", new List<string>() { 
                "xxx",
                "yyy",
            });

            Dictionary_.Add("xxx", new Lazy<Dictionary<string, string>>(() => {
                return new Dictionary<string, string>()
                {
                    { "aaa", "111" },
                    { "bbb", "222" },
                };
            }));

            Dictionary_.Add("yyy", new Lazy<Dictionary<string, string>>(() => {
                return new Dictionary<string, string>()
                {
                    { "aaa", "111" },
                    { "bbb", "222" },
                };
            }));

            Pair_.Add("xxx", new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>( "aaa", "111" ),
                new KeyValuePair<string, string>( "bbb", "222" ),
            });

            
            {
                string xxx = (string)String_["xxx"].Clone();
            }

            {
                List<string> xxx = List_["xxx"].ToList();
            }

            //{
            //    List<string> xxx = Dictionary_["xxx"].Value.Values.ToList();
            //}

            {
                List<string> xxx = Dictionary_["yyy"].Value.Values.ToList();
            }

            {
                Dictionary<string, string> xxx = new Dictionary<string, string>(Dictionary_["xxx"].Value);
                xxx.Remove("aaa");
                int rrr = 0;
            }

            {
                List<KeyValuePair<string, string>> xxx = Pair_["xxx"].ToList();
                // 刪除 index = 1 的元素（第二個）
                xxx.RemoveAt(1);
            }
        }
    }
}