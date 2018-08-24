using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lsc.Common;
using lsc.Bll;
using lsc.Model;
using lsc.crm.ViewModel;

namespace lsc.crm.Controllers
{
    public class ReportController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            DateTime startTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime();
            DateTime endTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime();
            if (Request.Method=="POST")
            {
                startTime = Request.Form["startTime"].TryToDateTime();
                endTime = Request.Form["endTime"].TryToDateTime();
            }
            ViewBag.startTime = startTime.ToString("yyyy-MM-dd");
            ViewBag.endTime = endTime.ToString("yyyy-MM-dd");
            UserBll userBll = new UserBll();
            List<UserInfo> userlist =await userBll.GetListAsync();

            SalesProjectBll salesProjectBll = new SalesProjectBll();
            // 成单量统计
            List<UserEnterReport> entercustomTotal =await salesProjectBll.GetAsync(startTime, endTime, 0);
            // 客户量统计
            EnterCustomerBll customerBll = new EnterCustomerBll();
            List<UserEnterReport> customerTotal = await customerBll.GetAsync(startTime, endTime, 0);

            //电话量统计
            EnterCustPhaseLogBll phlogbll = new EnterCustPhaseLogBll();
            List<UserEnterReport> phonetotal =await phlogbll.GetAsync(startTime,endTime,0);

            List<UserReportViewModel> reportlist = new List<UserReportViewModel>();
            // 应收账款
            var ReceoverPayList = await salesProjectBll.GetListAsync(0, startTime, endTime);

            ReceivedPaymentsLogBll receivedPaymentsLogBll = new ReceivedPaymentsLogBll();
            var ReceivedPaymentsLogList =await receivedPaymentsLogBll.GetListAsync(startTime,endTime,0);

            if (userlist!=null)
            {
                foreach (var user in userlist)
                {
                    if (User.UserName!="admin" && User.ID!= user.ID)
                    {
                        continue;
                    }
                    UserReportViewModel userReportView = new UserReportViewModel();
                    if (customerTotal!=null && customerTotal.Count>0)
                    {
                        var customer = customerTotal.FirstOrDefault(x=>x.UserID==user.ID);
                        if (customer!=null)
                        {
                            userReportView.CustomorTotal = customer.Total;
                        }
                    }

                    if (entercustomTotal!=null && entercustomTotal.Count>0)
                    {
                        var enter = entercustomTotal.FirstOrDefault(x => x.UserID == user.ID);
                        if (enter!=null)
                        {
                            userReportView.SalesProjectTotal = enter.Total;
                        }
                    }

                    if (phonetotal!=null && phonetotal.Count>0)
                    {
                        var phone = phonetotal.FirstOrDefault(x => x.UserID == user.ID);
                        if (phone!=null)
                        {
                            userReportView.PhoneTotal = phone.Total;
                        }
                    }
                    userReportView.UserID = user.ID;
                    userReportView.UserName = user.Name;
                    userReportView.TargetAmt = user.TargetAmt;
                    if (ReceoverPayList!=null && ReceoverPayList.Count>0)
                    {
                        var rlist = from rpay in ReceoverPayList
                                    where rpay.HeadID == user.ID
                                    select rpay.ProjectAmt;
                        if (rlist != null)
                            userReportView.ReceoverPay = rlist.Sum();
                    }

                    if (ReceivedPaymentsLogList!=null && ReceivedPaymentsLogList.Count>0)
                    {
                        var rplist = from rplog in ReceivedPaymentsLogList
                                     where rplog.UserID == user.ID
                                     select rplog.Amt;
                        if (rplist != null)
                            userReportView.HReceoverPay = rplist.Sum();
                    }
                    reportlist.Add(userReportView);
                }
            }
            ViewBag.root = "report";
            return View(reportlist);
        }
        /// <summary>
        /// 统计销售员一段时间内每天电话量
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PhoneReport()
        {
            DateTime startTime = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime();
            DateTime endTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime();
            if (Request.Method == "POST")
            {
                startTime = Request.Form["startTime"].TryToDateTime();
                endTime = Request.Form["endTime"].TryToDateTime();
            }
            ViewBag.startTime = startTime.ToString("yyyy-MM-dd");
            ViewBag.endTime = endTime.ToString("yyyy-MM-dd");

            EnterCustPhaseLogBll enterCustPhaseLogBll = new EnterCustPhaseLogBll();
           
            UserBll userBll = new UserBll();
            List<UserInfo> userlist = await userBll.GetListAsync();
            List<EnterTotalReportForDay> list = new List<EnterTotalReportForDay>();
            if (User.UserName == "admin")
            {
                list = await enterCustPhaseLogBll.GetReportAsync(startTime, endTime);
            }
            else
            {
                list = await enterCustPhaseLogBll.GetReportAsync(startTime, endTime,User.ID);
            }
            // 日期
            var datalist = from info in list
                           orderby info.Days
                           group info by info.Days into g
                           select g.Key;

            var useridlist = from info in list
                           group info by info.UserID into g
                           select g.Key;
            List<Serie> seriesList = new List<Serie>();
            List<string> userNames = new List<string>();
            if (useridlist!=null && useridlist.Count()>0)
            {
                List<Data> markdataList = new List<Data>();
                markdataList.Add(new Data { type = "max", name = "最大值" });
                markdataList.Add(new Data { type = "min", name = "最小值" });
                var markPoint = new Mark { data = markdataList };

                List<Data> markLineData = new List<Data>() { new Data { type = "average", name = "平均值" } };
                var markLine = new Mark { data = markLineData };
                foreach (var uid in useridlist)
                {
                    List<string> dList = new List<string>();
                    
                    foreach (string s in datalist)
                    {
                        string data = "0";
                        var info = from i in list
                            where i.Days == s && i.UserID == uid
                            select i;
                        if (info!=null && info.Count()>0)
                        {
                            data = info.FirstOrDefault().Total.TryToString();
                        }
                        dList.Add(data);
                    }
                    //var data = from info in list
                    //           where info.UserID == uid
                    //           orderby info.Days
                    //           select info.Total.ToString();
                    string username = string.Empty;
                    if (userlist!=null)
                    {
                        var user = userlist.FirstOrDefault(x => x.ID == uid);
                        if (user != null)
                            username = user.Name;
                    }
                    if (!userNames.Contains(username))
                        userNames.Add(username);

                    var series = new Serie
                    {
                        name = username,
                        type = "line",
                        data = dList.ToArray(),
                        markPoint = markPoint,
                        markLine = markLine
                    };
                    seriesList.Add(series);
                }
            }
            ViewBag.datalist = JsonSerializerHelper.Serialize(datalist.ToArray());
            ViewBag.seriesList = JsonSerializerHelper.Serialize(seriesList);
            ViewBag.userNames = JsonSerializerHelper.Serialize(userNames.ToArray());
            ViewBag.list = list.OrderBy(x => x.Days).ToList();
            ViewBag.userlist = userlist;
            return View();
        }
    }
}