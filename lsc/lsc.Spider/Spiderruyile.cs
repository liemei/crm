using AngleSharp.Html.Parser;
using lsc.Bll;
using lsc.Common;
using lsc.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace lsc.Spider
{
    public class Spiderruyile
    {
        private void SpiderList(string url,string price,string city)
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            int totalPage = 1;
            HttpUtils httpUtils = new HttpUtils();
            for (int j=1;j<6;j++)
            {
                for (int i = 1; i <= totalPage; i++)
                {
                    Thread.Sleep(2000);
                    string lurl = $"{url}&t={j}&p={i}";
                    string html = httpUtils.Get(lurl);
                    HtmlParser htmlParser = new HtmlParser();
                    var dom = htmlParser.ParseDocument(html);
                    if (dom != null)
                    {
                        var slist = dom.QuerySelectorAll("div.sk");
                        if (slist != null)
                        {
                            foreach (var s in slist)
                            {
                                EnterCustomer enterCustomer = new EnterCustomer();
                                var h4 = s.QuerySelector("h4");
                                if (h4 != null)
                                {
                                    enterCustomer.Abbreviation = h4.TextContent;
                                    enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.MiddleSchool;
                                    enterCustomer.EnterName = h4.TextContent;

                                    s.RemoveChild(h4);
                                }
                                var div = s.QuerySelector("div.kw");
                                if (div != null)
                                {
                                    s.RemoveChild(div);
                                }
                                string context = s.TextContent;
                                var param = context.Split("地址");
                                if (context.Contains("地址")&& context.Contains("电话") && param != null && param.Length>=2)
                                {
                                    string p1 = param[0];
                                    string p2 = param[1];//地址
                                    if (p1.Contains("邮编"))
                                    {
                                        var pr = p1.Split("邮编");
                                        if (pr != null && pr.Length >= 2)
                                        {
                                            enterCustomer.Landline = pr[0].Replace("电话：", "");
                                        }
                                    }
                                    else
                                    {
                                        enterCustomer.Landline = p1.Replace("电话：", "");
                                    }
                                }
                                enterCustomer.Province = price;
                                enterCustomer.City = city;
                                enterCustomer.Source = Model.Enume.CustSource.Other;
                                enterCustomer.State = Model.Enume.StateEnum.Invalid;
                                switch (j)
                                {
                                    case 1:
                                        enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.PrimarySchool;
                                        break;
                                    case 2:
                                        enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.PrimarySchool;
                                        break;
                                    case 3:
                                        enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.MiddleSchool;
                                        break;
                                    case 4:
                                        enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.Colleges;
                                        break;
                                    case 5:
                                        enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.Colleges;
                                        break;
                                }
                                if (!bll.ExistsEnterNameAsync(0, enterCustomer.EnterName).Result)
                                {
                                    bll.AddEnterCustomer(enterCustomer);
                                }
                                ClassLoger.Info("成功抓取学校:", enterCustomer.EnterName);
                                Console.WriteLine(enterCustomer.EnterName);
                            }
                        }
                        var fenyeDiv = dom.QuerySelector("div.fy");
                        if (fenyeDiv != null)
                        {
                            var zysspan = fenyeDiv.QuerySelector("span.zys");
                            if (zysspan != null)
                            {
                                totalPage = zysspan.TextContent.TryToInt(1);
                            }
                        }
                    }

                }
            }
            
        }

        public void SpiderTest()
        {
            string url = "https://www.ruyile.com/xuexiao/?a=1"; 
            HttpUtils httpUtils = new HttpUtils();
            string html = httpUtils.Get(url);
            HtmlParser htmlParser = new HtmlParser();
            var dom = htmlParser.ParseDocument(html);
            if (dom!=null)
            {
                var pdiv = dom.QuerySelector("div.dqlb");
                if (pdiv!=null)
                {
                    var qylbdiv = pdiv.QuerySelector("div.qylb");
                    if (qylbdiv!=null)
                    {
                        var alist = qylbdiv.QuerySelectorAll("a");
                        if (alist!=null)
                        {
                            foreach (var a in alist)
                            {
                                Thread.Sleep(1000);
                                var pri = a.TextContent;
                                var u = a.GetAttribute("href");
                                SpiderList(u,pri,"");
                            }
                        }
                    }
                }
            }
        }
    }
}
