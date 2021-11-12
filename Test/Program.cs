using System;
using System.IO;
using System.Net;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            String URL = "https://img1.doubanio.com/view/subject/s/public/s33928057.jpg";
            //网页请求
            HttpWebRequest Myrq = (HttpWebRequest)WebRequest.Create(URL);//进行强制转换
            //连续性连接
            Myrq.KeepAlive = false;
            //超时值
            Myrq.Timeout = 30 * 1000;//30秒
            //请求方法
            Myrq.Method = "GET";
            //Accept的值
            Myrq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            //对有反爬的网页很关键
            //Myrq.Host = "book.douban.com";
            //Myrq.Referer = "https://book.douban.com/";
            //类似于ID
            Myrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 Safari/537.36 Edg/92.0.902.67";

            //服务器回应
            HttpWebResponse Myrp = (HttpWebResponse)Myrq.GetResponse();//进行强制转换
            //是否相应成功
            if (Myrp.StatusCode != HttpStatusCode.OK)
            {
                return;
            }
            //using(StreamReader sr=new StreamReader(Myrp.GetResponseStream()))
            //{
            //    //显示在控制台上
            //    Console.WriteLine(sr.ReadToEnd());
            //}
            //人工取代
            using(FileStream fs=new FileStream("1.jpg", FileMode.Create))
            {
                Myrp.GetResponseStream().CopyTo(fs);
            }
            //Console.ReadKey();
        }
    }
}
