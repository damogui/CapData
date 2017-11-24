using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Http;
using Common.Model;
using Newtonsoft.Json;

namespace CapData
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.WriteLine(DeUnicode("\u817e\u8baf\u79d1\u6280\uff08\u6df1\u5733\uff09\u6709\u9650\u516c\u53f8"));
            string jsonUrl = "https://www.tianyancha.com/search/suggest.json?key=%E4%BA%91%E5%AD%A6";

            string jsonUrl2 = "http://www.qichacha.com/tax_view?keyno=181e23a3c35a6fc18450f03cc13bb03b&ajaxflag=1";


            string json = HttpHelper.RequestJSONFromUrl(jsonUrl);
            NamaePara<List<ComName>> para = JsonConvert.DeserializeObject<NamaePara<List<ComName>>>(json);
          

            string json2 = HttpHelper.RequestJSONFromUrl(jsonUrl2);
            BasePara<Credit> para2 = JsonConvert.DeserializeObject<BasePara<Credit>>(json2);
            Console.ReadKey();


        }


        /// <summary>
        /// Unicode编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EnUnicode(string str)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    strResult.Append("\\u");
                    strResult.Append(((int)str[i]).ToString("x"));
                }
            }
            return strResult.ToString();
        }

        /// <summary>
        /// Unicode解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeUnicode(string str)
        {
            //最直接的方法Regex.Unescape(str);
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            return reg.Replace(str, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        }
    }
}
