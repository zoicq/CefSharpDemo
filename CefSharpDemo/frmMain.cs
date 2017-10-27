using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Reflection;
using System.IO;

namespace CefSharpDemo
{
    public partial class frmMain : Form
    {
        public ChromiumWebBrowser browser;

        
        public frmMain()
        {
            InitializeComponent();
            LoadResource();
            InitBrowser();
        }
        /// <summary>
        /// 加载嵌入资源文件
        /// </summary>
        public void LoadResource()
        {
            string startPath = Application.StartupPath + @"\res\index.html";

            if (File.Exists(startPath))
            {
                File.Delete(startPath);
            }

            //读取嵌入资源文件
            Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("CefSharpDemo.res.index.html");
            byte[] bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();

            //保存嵌入资源文件
            FileStream fs = new FileStream(Application.StartupPath + @"\res\index.html", FileMode.CreateNew);
            fs.Write(bs, 0, bs.Length);
            fs.Close();
        }

        /// <summary>
        /// 初始化浏览器
        /// </summary>
        public void InitBrowser()
        {
            //初始化配置
            CefSettings cefsettings = new CefSettings();
            Cef.Initialize(cefsettings);

            //启动页面地址
            string startURL = Application.StartupPath + @"\res\index.html";

            //创建浏览器对象
            browser = new ChromiumWebBrowser(startURL);

            //注册JS调用winForm的方法对象
            browser.RegisterJsObject("winfromfn", new JSFunObj());

            //设置浏览器控件停靠方式
            browser.Dock = DockStyle.Fill;

            //将浏览器插入到窗体控件集合中
            this.Controls.Add(browser);

        }
       
        /// <summary>
        /// 前台JS调用的方法类
        /// </summary>
        public class JSFunObj
        {
            MyBackgroundWorker mybgw = new MyBackgroundWorker();
            /// <summary>
            /// 获取信息
            /// </summary>
            /// <param name="callback">回调函数</param>
            public void getInfo(IJavascriptCallback callback)
            {
                mybgw.runDoWork("getInfo", callback);

                //MessageBox.Show("我是webform框架弹出的");
                ////执行前台的回调函数
                //callback.ExecuteAsync("这是webform调用的");
            }
        }

    }


}
