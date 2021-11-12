using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NETCrawler
{
    public class UrlBianMa
    {
        //关键字url编码
        public static string UrlEncode(string Key)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(Key);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString());
        }
        //重命名函数
        public static string ReName(string FileAddress)
        {
            FileAddress = FileAddress.Replace(":", "_");
            FileAddress = FileAddress.Replace("*", "_");
            FileAddress = FileAddress.Replace("?", "_");
            FileAddress = FileAddress.Replace("\\", "_");
            FileAddress = FileAddress.Replace("/", "_");
            FileAddress = FileAddress.Replace("\"", "_");
            FileAddress = FileAddress.Replace("<", "_");
            FileAddress = FileAddress.Replace("|", "_");
            FileAddress = FileAddress.Replace(">", "_");
            FileAddress = FileAddress.Replace(" ", "_");
            return FileAddress;
        }
    }
}