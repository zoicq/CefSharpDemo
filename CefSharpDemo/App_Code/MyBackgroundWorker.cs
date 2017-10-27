using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CefSharp;

namespace CefSharpDemo
{
    class MyBackgroundWorker
    {
        private BackgroundWorker bgw = new BackgroundWorker();

        //执行操作
        public void runDoWork(string methodName,IJavascriptCallback callback)
        {
            //注册完成事件处理程序
            bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;

            switch (methodName)
            {
                case "getInfo":
                    bgw.DoWork += GetInfo_DoWork;
                    bgw.RunWorkerAsync(callback);
                    break;

                case "getList":
                    bgw.DoWork += GetList_DoWork;
                    bgw.RunWorkerAsync();
                    break;
            }

        }

        //后台执行完成事件
        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                IJavascriptCallback cb = (IJavascriptCallback)e.Result;
                //执行回调
                cb.ExecuteAsync("123");
            }
            
        }

        //后台执行的逻辑
        private void GetInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            //获取参数
            IJavascriptCallback cb = (IJavascriptCallback)e.Argument;

            //在这里执行耗时的运算。
            System.Threading.Thread.Sleep(5000);

            //将需要的参数(回调函数对象)传入完成事件中
            e.Result = cb;
        }

        private void GetList_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
    }
}
