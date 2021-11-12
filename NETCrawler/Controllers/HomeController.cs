using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace NETCrawler.Controllers
{
    public class HomeController : Controller
    {
        //首页
        public ActionResult Index()
        {
            Session["result"] = "";
            return View();
        }
        #region
        ////选择目录对话框
        //public void SetPath()
        //{
        //    //错误内容: 在可以调用OLE之前，必须将当前线程设置为单线程单元（STA）模式，请确保您的Main函数带有STAThreadAttribute。
        //    Thread thread = new Thread(new ThreadStart(SavePathChoice));
        //    thread.SetApartmentState(ApartmentState.STA); //重点
        //    thread.Start();
        //}
        ////目录选择对话框
        //public void SavePathChoice()
        //{
        //    //创建目录选择对话框对象
        //    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        //    //提示信息
        //    folderBrowserDialog.Description = "请选择保存的文件夹!";
        //    //不显示新建文件夹按钮
        //    folderBrowserDialog.ShowNewFolderButton = false;
        //    //保存到当前页面
        //    folderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
        //    //运行目录选择对话框
        //    folderBrowserDialog.ShowDialog();
        //    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        Session["result"] = folderBrowserDialog.SelectedPath.ToList();
        //    }
        //}
        ////开始下载
        //public void DownloadPath(string key,string save)
        //{
        //    //验证保存路径是否存在与文件夹创建
        //    if (!Directory.Exists(save))
        //    {
        //        try
        //        {
        //            Directory.CreateDirectory(save);
        //        }
        //        catch
        //        {
        //            Response.Write("<script>alert('文件创建失败！');</script>");
        //            return;
        //        }
        //    }
        //    //验证文件的可写性
        //    try
        //    {
        //        using (FileStream fs = new FileStream(Path.Combine(save, "Test_File.txt"), FileMode.Create)) ;
        //        //删除文件，跨平台
        //        //File.Detete(Path.Combine(save, "Test_File.txt"));
        //    }
        //    catch
        //    {
        //        Response.Write("<script>alert('文件的可写性！');</script>");
        //        return;
        //    }
        //}
        #endregion
        private string DownloadsURL(string url, string pages)
        {
            int pagess = int.Parse(pages);
            string Html = null;
            string Urlall = "下载失败";
            int cs = 1;
            for (int i = 0; i < pagess; i++)
            {
                Stream stream = null;
                string Durl = url.Replace("[REPLACE]", (10 * i).ToString());
                try
                {
                    stream = Downloas.DownloadFile(Durl);
                }
                catch
                {

                    continue;
                }
                try
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        Html = sr.ReadToEnd();
                    }
                }
                catch
                {

                    continue;
                }
                JObject jobj = JObject.Parse(Html);
                JArray jarr = (JArray)jobj["data"];
                string PicUrl = null;
                string SavePath = null;
                for (int j = 0; j < jarr.Count; j++)
                {
                    try
                    {
                        PicUrl = jarr[j]["thumbURL"].ToString();
                        SavePath = Path.Combine("D:\\桌面\\img", UrlBianMa.ReName(PicUrl.Substring(PicUrl.LastIndexOf("/")+ 1)));
                        SavePath = SavePath.Replace(".jpg", ".webp");
                        string PicReferer = "hppt://image.baidu.com/";
                        //if (jarr[j].Contains("replaceUrl"))
                        //{
                        //    PicUrl = jarr[j]["replaceUrl"][0]["ObjUrl"].ToString();
                        //    SavePath = Path.Combine("D:\\桌面\\img", UrlBianMa.ReName(PicUrl.Substring(PicUrl.LastIndexOf("/") + 1)));
                        //    PicReferer = jarr[j]["replaceUrl"][0]["FromUrl"].ToString();

                        //}
                        if (Downloas.DownloadFile(PicUrl, SavePath, PicReferer))
                        {
                            if (cs == 1)
                            {
                                Urlall = "下载成功";
                                cs++;
                            }
                            Urlall += PicUrl + SavePath + "  ";
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return Urlall;
        }
        public string Downloadworld(string keyname, string zMax, string ICColor, string page)
        {
            //Stream stream = Downloas.DownloadFile(GetURL(keyname, zMax, ICColor));
            //using (StreamReader sr = new StreamReader(stream))
            //{
            //    return sr.ReadToEnd();
            //}
            return DownloadsURL(GetURL(keyname, zMax, ICColor), page);
            //return GetURL(keyname, zMax, ICColor);
        }
        //生成地址
        private string GetURL(string keyname, string zMax, string ICColor)
        {
            //关键字
            string queryWord, word;
            queryWord = UrlBianMa.UrlEncode(keyname);
            word = UrlBianMa.UrlEncode(keyname);
            //大小
            string z;
            z = zMax;
            //颜色
            string ic;
            ic = ICColor;
            string url = string.Format("https://image.baidu.com/search/acjson?tn=resultjson_com&logid=11225711020569559063&ipn=rj&ct=201326592&is=&fp=result&queryWord={0}&cl=2&lm=-1&ie=utf-8&oe=utf-8&adpicid=&st=-1&z={1}&ic={2}&hd=&latest=&copyright=&word={3}&s=&se=&tab=&width=&height=&face=0&istype=2&qc=&nc=1&fr=&expermode=&nojc=&pn=[REPLACE]&rn=10&gsm=1e&1628866309477=", queryWord, z, ic, word);
            return url;
        }
    }
}