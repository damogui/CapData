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
            #region 进行非抓取

            //            string regstr = @"(?i)(?<=<td.*?.*?>)[^<]+(?=</td>)"; //提取td的文字           
            //string regstr = @"<a\s+href=(?<url>.+?)>(?<content>.+?)</a>";   //提取链接的内容
                                                                            //            string regstr = @"<td.+?><a\s+href=(?<url>.+?)>(?<content>.+?)</a></td>";  //提取TD中链接的内容
                                                                            //            string regstr = @"<td.+?><span.+?>(?<content>.+?)</span></td>";  //提取TD中span的内容
                                                                            //            string regstr = @"<td.+?>(?<content>.+?)</td>";   //获取TD之间所有的内容
                                                                            //            string regstr = @"<td>(?<content>.+?)-<font color=#0000ff>推荐</font></td>"; //获取内容

           // string regstr = "<a\b[^>]+\bhref=\"/firm_([^ \"]*)\"[^>] *>([\\s\\S] *?)</a>";  //提取页面所有TD内容
          // string regReplace = @"(?i)[\<]td.*?[\>]";    //将所有<td......> 替换成<td>

            string jsonUrl3 = "http://www.qichacha.com/search?key=%E4%BA%91%E5%AD%A6%E6%97%B6%E4%BB%A3";


            string json3 = HttpHelper.RequestJSONFromUrl(jsonUrl3);


            Regex reg =
                new Regex(
                    @"(?is)<a[^>]*?href=(['""\s]?)(?<href>([^'""\s]*))\.html\1[^>]*?>");

            MatchCollection match = reg.Matches(json3);
            //            MatchCollection mc = reg.Matches(json3);
            Console.WriteLine(match[0].Groups["href"].Value);
            foreach (Match m in match)
            {
               // Console.WriteLine(m.Groups[0].ToString());
                Console.WriteLine(m.Groups["href"].Value + "");
                Console.WriteLine("------------------------------");
//                string s = Regex.Replace(m.Groups[0].ToString(), regReplace, "<td>", RegexOptions.IgnoreCase);
               // Console.WriteLine(s);

            } 
            #endregion

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
