using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lsc.Common;
using lsc.Model.Enume;
using lsc.Bll;
using lsc.Model;
using lsc.crm.ViewModel;

namespace lsc.crm.Controllers
{
    /// <summary>
    /// 企业客户信息
    /// </summary>
    public class EnterCustomController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            DistrictInfoBll dbll = new DistrictInfoBll();
            var ProvinceList = await dbll.GetAsync(0);
            ViewBag.ProvinceList = ProvinceList;
            int pageIndex = 1;
            if (Request.Method =="POST")
            {
                pageIndex = Request.Form["page"].TryToInt(1);
            }
            
            int pageSize = 20;
            string EnterName = Request.Method == "POST"? Request.Form["EnterName"].TryToString():string.Empty;
            ViewBag.EnterName = EnterName;
            CustomerTypeEnum? CustomerType = null;
            ViewBag.CustomerType = 0;
            if (Request.Method == "POST"&& Request.Form["CustomerType"].TryToInt(0) > 0)
            {
                CustomerType = (CustomerTypeEnum)Request.Form["CustomerType"].TryToInt(0);
                ViewBag.CustomerType = Request.Form["CustomerType"].TryToInt(0);
            }
           
            RelationshipEnume? Relationship = null;
            ViewBag.Relationship = 0;
            if (Request.Method == "POST" && Request.Form["Relationship"].TryToInt(0) > 0)
            {
                Relationship = (RelationshipEnume)Request.Form["Relationship"].TryToInt(0);
                ViewBag.Relationship = Request.Form["Relationship"].TryToInt(0);
            }

           
            PhaseEnume? Phase = null;
            ViewBag.Phase = 0;
            if (Request.Method == "POST" && Request.Form["Phase"].TryToInt(0) > 0)
            {
                Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);
                ViewBag.Phase = Request.Form["Phase"].TryToInt(0);
            }
            


            ValueGradeEnume? ValueGrade = null;
            ViewBag.ValueGrade = 0;
            if (Request.Method == "POST" && Request.Form["ValueGrade"].TryToInt(0) > 0)
            {
                ValueGrade = (ValueGradeEnume)Request.Form["ValueGrade"].TryToInt(0);
                ViewBag.ValueGrade = Request.Form["ValueGrade"].TryToInt(0);
            }
           
            CustSource? Source = null;
            ViewBag.Source = 0;
            if (Request.Method == "POST" && Request.Form["Source"].TryToInt(0) > 0)
            {
                Source = (CustSource)Request.Form["Source"].TryToInt(0);
                ViewBag.Source = Request.Form["Source"].TryToInt(0);
            }
           
            bool? IsHeat = null;
            ViewBag.IsHeat = false;
            if (Request.Method == "POST" && !Request.Form["IsHeat"].TryToString().IsNull())
            {
                IsHeat = Request.Form["IsHeat"].TryToString() == "on";
                ViewBag.IsHeat = IsHeat;
            }
           
            DegreeOfHeatEnume? DegreeOfHeat = null;
            ViewBag.DegreeOfHeat = 0;
            if (Request.Method == "POST" && Request.Form["DegreeOfHeat"].TryToInt(0) > 0)
            {
                DegreeOfHeat = (DegreeOfHeatEnume)Request.Form["DegreeOfHeat"].TryToInt(0);
                ViewBag.DegreeOfHeat = Request.Form["DegreeOfHeat"].TryToInt(0);
            }
           
            string Province = null;
            ViewBag.Province = string.Empty;
            if (Request.Method == "POST" && !Request.Form["Province"].TryToString().IsNull())
            {
                Province = Request.Form["Province"].TryToString();
                ViewBag.Province = Request.Form["Province"].TryToString();
            }
           
            string City = null;
            ViewBag.City = string.Empty;
            if (Request.Method == "POST" && !Request.Form["City"].TryToString().IsNull())
            {
                City = Request.Form["City"].TryToString();
                ViewBag.City= Request.Form["City"].TryToString();
            }
            
            DateTime? UpdateTime = null;
            bool? timeType = null;
            ViewBag.UpdateTime = 0;
            if (Request.Method == "POST" && Request.Form["UpdateTime"].TryToInt(0) > 0)
            {
                ViewBag.UpdateTime = Request.Form["UpdateTime"].TryToInt(0);
                switch (Request.Form["UpdateTime"].TryToInt(0))
                {
                    case 1:
                        UpdateTime = DateTime.Now.AddDays(-7);
                        break;
                    case 2:
                        UpdateTime = DateTime.Now.AddDays(-15);
                        break;
                    case 3:
                        UpdateTime = DateTime.Now.AddDays(-30);
                        break;
                    case 4:
                        UpdateTime = DateTime.Now.AddDays(-60);
                        break;
                    case 5:
                        UpdateTime = DateTime.Now.AddDays(-90);
                        break;
                    case 6:
                        UpdateTime = DateTime.Now.AddDays(-3);
                        timeType = true;
                        break;
                    case 7:
                        UpdateTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
                        timeType = true;
                        break;
                }
            }
            EnterCustContactsBll ecbll = new EnterCustContactsBll();
            List<int> idlist = new List<int>();
            string Telephone = string.Empty;
            if (Request.Method == "POST" && !Request.Form["Telephone"].TryToString().IsNull())
            {
                ViewBag.Telephone = Request.Form["Telephone"].TryToString();
                Telephone = Request.Form["Telephone"].TryToString();
            }
            string QQ = string.Empty;
            if (Request.Method=="POST" && !Request.Form["QQ"].TryToString().IsNull())
            {
                ViewBag.QQ = Request.Form["QQ"].TryToString();
                QQ = Request.Form["QQ"].TryToString();
            }
            if (!QQ.IsNull() || !Telephone.IsNull())
            {
                var eclists = await ecbll.GetListAsync(Telephone, QQ);
                if (eclists!=null && eclists.Count>0)
                {
                    var ids = from ec in eclists
                              select ec.EnterCustID;
                    idlist.AddRange(ids);
                }
            }
            

            EnterCustomerBll bll = new EnterCustomerBll();
            EnterCustPhaseLogBll eclogbll = new EnterCustPhaseLogBll();
            List<EnterCustContacts> eclist = new List<EnterCustContacts>();
            
            List<EnterCustPhaseLog> ecplogList = new List<EnterCustPhaseLog>();

            var list = await bll.GetAsync(pageIndex, pageSize, User.ID, EnterName, CustomerType, Relationship, Phase, ValueGrade, Source, IsHeat, DegreeOfHeat, Province, City, UpdateTime, timeType, idlist);
            if (list != null && list.Item1!=null)
            {
                foreach(var info in list.Item1)
                {
                    var elist = await ecbll.GetListAsync(info.ID);
                    if (elist != null && elist.Count > 0)
                        eclist.AddRange(elist);
                    var eclog = await eclogbll.ListAsync(info.ID);
                    if (eclog != null && eclog.Count > 0)
                        ecplogList.Add(eclog.FirstOrDefault());
                }
            }
            ViewBag.ecplogList = ecplogList;
            ViewBag.eclist = eclist;
            ViewBag.count = list.Item2;
            ViewBag.pageIndex = pageIndex;
            ViewBag.UserID = User.ID;
            ViewBag.UserName = User.Name;
            ViewBag.root = "enter";
            return View(list.Item1);

            //return View();
        }

        public async Task<IActionResult> AllEnterList()
        {
            DistrictInfoBll dbll = new DistrictInfoBll();
            var ProvinceList = await dbll.GetAsync(0);
            ViewBag.ProvinceList = ProvinceList;
            int pageIndex = 1;
            if (Request.Method == "POST")
            {
                pageIndex = Request.Form["page"].TryToInt(1);
            }

            int pageSize = 20;
            string EnterName = Request.Method == "POST" ? Request.Form["EnterName"].TryToString() : string.Empty;
            ViewBag.EnterName = EnterName;
            CustomerTypeEnum? CustomerType = null;
            ViewBag.CustomerType = 0;
            if (Request.Method == "POST" && Request.Form["CustomerType"].TryToInt(0) > 0)
            {
                CustomerType = (CustomerTypeEnum)Request.Form["CustomerType"].TryToInt(0);
                ViewBag.CustomerType = Request.Form["CustomerType"].TryToInt(0);
            }

            RelationshipEnume? Relationship = null;
            ViewBag.Relationship = 0;
            if (Request.Method == "POST" && Request.Form["Relationship"].TryToInt(0) > 0)
            {
                Relationship = (RelationshipEnume)Request.Form["Relationship"].TryToInt(0);
                ViewBag.Relationship = Request.Form["Relationship"].TryToInt(0);
            }


            PhaseEnume? Phase = null;
            ViewBag.Phase = 0;
            if (Request.Method == "POST" && Request.Form["Phase"].TryToInt(0) > 0)
            {
                Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);
                ViewBag.Phase = Request.Form["Phase"].TryToInt(0);
            }



            ValueGradeEnume? ValueGrade = null;
            ViewBag.ValueGrade = 0;
            if (Request.Method == "POST" && Request.Form["ValueGrade"].TryToInt(0) > 0)
            {
                ValueGrade = (ValueGradeEnume)Request.Form["ValueGrade"].TryToInt(0);
                ViewBag.ValueGrade = Request.Form["ValueGrade"].TryToInt(0);
            }

            CustSource? Source = null;
            ViewBag.Source = 0;
            if (Request.Method == "POST" && Request.Form["Source"].TryToInt(0) > 0)
            {
                Source = (CustSource)Request.Form["Source"].TryToInt(0);
                ViewBag.Source = Request.Form["Source"].TryToInt(0);
            }

            bool? IsHeat = null;
            ViewBag.IsHeat = false;
            if (Request.Method == "POST" && !Request.Form["IsHeat"].TryToString().IsNull())
            {
                IsHeat = Request.Form["IsHeat"].TryToString() == "on";
                ViewBag.IsHeat = IsHeat;
            }

            DegreeOfHeatEnume? DegreeOfHeat = null;
            ViewBag.DegreeOfHeat = 0;
            if (Request.Method == "POST" && Request.Form["DegreeOfHeat"].TryToInt(0) > 0)
            {
                DegreeOfHeat = (DegreeOfHeatEnume)Request.Form["DegreeOfHeat"].TryToInt(0);
                ViewBag.DegreeOfHeat = Request.Form["DegreeOfHeat"].TryToInt(0);
            }

            string Province = null;
            ViewBag.Province = string.Empty;
            if (Request.Method == "POST" && !Request.Form["Province"].TryToString().IsNull())
            {
                Province = Request.Form["Province"].TryToString();
                ViewBag.Province = Request.Form["Province"].TryToString();
            }

            string City = null;
            ViewBag.City = string.Empty;
            if (Request.Method == "POST" && !Request.Form["City"].TryToString().IsNull())
            {
                City = Request.Form["City"].TryToString();
                ViewBag.City = Request.Form["City"].TryToString();
            }

            DateTime? UpdateTime = null;
            ViewBag.UpdateTime = 0;
            if (Request.Method == "POST" && Request.Form["UpdateTime"].TryToInt(0) > 0)
            {
                ViewBag.UpdateTime = Request.Form["UpdateTime"].TryToInt(0);
                switch (Request.Form["UpdateTime"].TryToInt(0))
                {
                    case 1:
                        UpdateTime = DateTime.Now.AddDays(-7);
                        break;
                    case 2:
                        UpdateTime = DateTime.Now.AddDays(-15);
                        break;
                    case 3:
                        UpdateTime = DateTime.Now.AddDays(-30);
                        break;
                    case 4:
                        UpdateTime = DateTime.Now.AddDays(-60);
                        break;
                    case 5:
                        UpdateTime = DateTime.Now.AddDays(-90);
                        break;
                }
            }
            int? userid = null;
            if (Request.Method == "POST" && Request.Form["UserID"].TryToInt()>0)
            {
                userid = Request.Form["UserID"].TryToInt();
            }
            ViewBag.userid = userid;
            EnterCustomerBll bll = new EnterCustomerBll();
            var list = await bll.GetAllAsync(pageIndex, pageSize, userid, EnterName, CustomerType, Relationship, Phase, ValueGrade, Source, IsHeat, DegreeOfHeat, Province, City, UpdateTime);
            ViewBag.count = list.Item2;
            ViewBag.pageIndex = pageIndex;

            UserBll userBll = new UserBll();
            List<UserInfo> userlist = await userBll.GetListAsync();

            EnterCustContactsBll enterCustContactsBll = new EnterCustContactsBll();
            List<EnterCustContacts> eclist = new List<EnterCustContacts>();
            if (list.Item1!=null)
            {
                foreach (var enterCustomer in list.Item1)
                {
                    var ecs = await enterCustContactsBll.GetListAsync(enterCustomer.ID);
                    if (ecs!=null && ecs.Count>0)
                    {
                        eclist.AddRange(ecs);
                    }
                }
            }

            ViewBag.eclist = eclist;
            ViewBag.userlist = userlist;
            ViewBag.root = "enter";
            return View(list.Item1);
        }
        /// <summary>
        /// 查看联系人
        /// </summary>
        /// <param name="EnterCustID"></param>
        /// <returns></returns>
        public async Task<IActionResult> EnterCustContactsList(int EnterCustID)
        {
            EnterCustContactsBll bll = new EnterCustContactsBll();
            var list = await bll.GetListAsync(EnterCustID);
            return View(list);
        }

        //[HttpPost]
        //public async Task<IActionResult> EnterCustomlist()
        //{
        //    int pageIndex = Request.Form["page"].TryToInt(1);
        //    int pageSize = 20;
        //    string EnterName = Request.Form["EnterName"].TryToString();
        //    CustomerTypeEnum? CustomerType = null;
        //    if (Request.Form["CustomerType"].TryToInt(0)>0)
        //    {
        //        CustomerType = (CustomerTypeEnum)Request.Form["CustomerType"].TryToInt(0);
        //    }
        //    RelationshipEnume? Relationship = null;
        //    if (Request.Form["Relationship"].TryToInt(0) > 0)
        //        Relationship = (RelationshipEnume)Request.Form["Relationship"].TryToInt(0);

        //    PhaseEnume? Phase = null;
        //    if (Request.Form["Phase"].TryToInt(0) > 0)
        //        Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);

        //    ValueGradeEnume? ValueGrade = null;
        //    if (Request.Form["ValueGrade"].TryToInt(0) > 0)
        //        ValueGrade = (ValueGradeEnume)Request.Form["ValueGrade"].TryToInt(0);

        //    CustSource? Source = null;
        //    if (Request.Form["Source"].TryToInt(0) > 0)
        //        Source = (CustSource)Request.Form["Source"].TryToInt(0);

        //    bool? IsHeat = null;
        //    if (!Request.Form["IsHeat"].TryToString().IsNull())
        //        IsHeat = Request.Form["IsHeat"].TryToString()== "on";

        //    DegreeOfHeatEnume? DegreeOfHeat = null;
        //    if (Request.Form["DegreeOfHeat"].TryToInt(0) > 0)
        //        DegreeOfHeat = (DegreeOfHeatEnume)Request.Form["DegreeOfHeat"].TryToInt(0);

        //    string Province = null;
        //    if (!Request.Form["Province"].TryToString().IsNull())
        //        Province = Request.Form["Province"].TryToString();

        //    string City = null;
        //    if (!Request.Form["City"].TryToString().IsNull())
        //        City = Request.Form["City"].TryToString();

        //    DateTime? UpdateTime = null;
        //    if (Request.Form["UpdateTime"].TryToInt(0) > 0)
        //    {
        //        switch (Request.Form["UpdateTime"].TryToInt(0))
        //        {
        //            case 1:
        //                UpdateTime = DateTime.Now.AddDays(-7);
        //                break;
        //            case 2:
        //                UpdateTime = DateTime.Now.AddDays(-15);
        //                break;
        //            case 3:
        //                UpdateTime = DateTime.Now.AddDays(-30);
        //                break;
        //            case 4:
        //                UpdateTime = DateTime.Now.AddDays(-60);
        //                break;
        //            case 5:
        //                UpdateTime = DateTime.Now.AddDays(-90);
        //                break;
        //        }
        //    }
        //    EnterCustomerBll bll = new EnterCustomerBll();
        //    var list =await bll.GetAsync(pageIndex,pageSize,User.ID,EnterName,CustomerType,Relationship,Phase,ValueGrade,Source,IsHeat,DegreeOfHeat,Province,City,UpdateTime);
        //    ViewBag.count = list.Item2;
        //   return View(list.Item1);
        //}
        /// <summary>
        /// 添加客户信息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddEnterCustom(int id)
        {
            EnterCustomer enterCustomer = new EnterCustomer();
            DistrictInfoBll dbll = new DistrictInfoBll();
            if (id>0)
            {
                EnterCustomerBll bll = new EnterCustomerBll();
                enterCustomer =await bll.GetAsync(id);
            }
           
            var ProvinceList = await dbll.GetAsync(0);
            ViewBag.ProvinceList = ProvinceList;
            ViewBag.root = "enter";
            return View(enterCustomer);
        }
        /// <summary>
        /// 企业客户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EnterCustomInfo(int id,int t=0)
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            EnterCustomer enterCustomer = await bll.GetAsync(id);
            EnterCustContactsBll enterCustContactsBll = new EnterCustContactsBll();
            List<EnterCustContacts> conlist =await enterCustContactsBll.GetListAsync(id);
            ViewBag.conlist = conlist;
            EnterCustPhaseLogBll logbll = new EnterCustPhaseLogBll();
            List<EnterCustPhaseLog> loglist =await logbll.ListAsync(id);
            ViewBag.loglist = loglist;

            SalesProjectBll projectBll = new SalesProjectBll();
            List<SalesProject> projectList =await projectBll.GetListAsync(id);
            ViewBag.projectList = projectList;

            Dictionary<int, double> recpayDic = new Dictionary<int, double>();
            if (projectList != null && projectList.Count > 0)
            {
                ReceivedPaymentsLogBll rpbll = new ReceivedPaymentsLogBll();
                foreach (var info in projectList)
                {
                    var rplog = await rpbll.GetListAsync(info.ID);
                    if (rplog != null && rplog.Count > 0)
                    {
                        var rp = from log in rplog
                                 select log.Amt;
                        recpayDic[info.ID] = rp.Sum();
                    }
                }
            }
            ViewBag.recpayDic = recpayDic;
            ViewBag.t = t;
            return View(enterCustomer);
        }
        /// <summary>
        /// 快速添加企业信息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddEnterCustomQuick()
        {
            DistrictInfoBll dbll = new DistrictInfoBll();
            var ProvinceList = await dbll.GetAsync(0);
            ViewBag.ProvinceList = ProvinceList;
            ViewBag.root = "enter";
            return View();
        }

        /// <summary>
        /// 校验企业名称是否已经存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="EnterName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExistsEnterName(int id,string EnterName)
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            bool flag =await bll.ExistsEnterNameAsync(id,EnterName);
            return Json(new { code = 1, result = flag });
        }
        [HttpPost]
        public async Task<IActionResult> SaveEnterCustom()
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            int id = Request.Form["ID"].TryToInt(0);
            EnterCustPhaseLogBll logbll = new EnterCustPhaseLogBll();
            if (id > 0)
            {
                var info = await bll.GetAsync(id);
                info.Address = Request.Form["Address"].TryToString();
                info.City = Request.Form["City"].TryToString();
                info.CustAbstract = Request.Form["CustAbstract"].TryToString();
                info.CustomerType = (CustomerTypeEnum)Request.Form["CustomerType"].TryToInt(0);
                info.DegreeOfHeat = (DegreeOfHeatEnume)Request.Form["DegreeOfHeat"].TryToInt(0);
                info.Email = Request.Form["Email"].TryToString();
                info.EnterName = Request.Form["EnterName"].TryToString();
                info.FaxNumber = Request.Form["FaxNumber"].TryToString();
                info.HeatMsg = Request.Form["HeatMsg"].TryToString();
                info.HeatTYPE = (HeatTypeEnum)Request.Form["HeatTYPE"].TryToInt(0);
                info.InvoiceMsg = Request.Form["InvoiceMsg"].TryToString();
                info.IsHeat = Request.Form["IsHeat"].TryToString().Equals("on");
                if (info.Phase != (PhaseEnume)Request.Form["Phase"].TryToInt(0))
                {
                    info.Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);
                    //EnterCustPhaseLog enterCustPhaseLog = new EnterCustPhaseLog();
                    //enterCustPhaseLog.CreateTime = DateTime.Now;
                    //enterCustPhaseLog.EnterCustomerID = id;
                    //enterCustPhaseLog.Phase = info.Phase;
                    //enterCustPhaseLog.UserID = User.ID;
                    //enterCustPhaseLog.UserName = User.Name;
                    //enterCustPhaseLog.Rem = "客户信息修改";
                    //await logbll.AddAsync(enterCustPhaseLog);
                }
                info.Province = Request.Form["Province"].TryToString();
                info.Relationship = (RelationshipEnume)Request.Form["Relationship"].TryToInt(0);
                info.Rem = Request.Form["Rem"].TryToString();
                info.Source = (CustSource)Request.Form["Source"].TryToInt(0);
                info.Telephone = Request.Form["Telephone"].TryToString();
                info.UpdateTime = DateTime.Now;
                info.ValueGrade = (ValueGradeEnume)Request.Form["ValueGrade"].TryToInt(0);
                info.WebSit = Request.Form["WebSit"].TryToString();
                info.ZipCode = Request.Form["ZipCode"].TryToString();
                info.Landline = Request.Form["Landline"].TryToString();
                bool flag = await bll.UpdateEnterCustomerAsync(info);
                if (flag)
                {
                    return Json(new { code = 1, msg = "OK",id= info.ID });
                }
                else
                    return Json(new { code = 0, msg = "保存失败" });
            }
            else
            {
                EnterCustomer info = new EnterCustomer();
                info.Address = Request.Form["Address"].TryToString();
                info.City = Request.Form["City"].TryToString();
                info.CustAbstract = Request.Form["CustAbstract"].TryToString();
                info.CustomerType = (CustomerTypeEnum)Request.Form["CustomerType"].TryToInt(0);
                info.DegreeOfHeat = (DegreeOfHeatEnume)Request.Form["DegreeOfHeat"].TryToInt(0);
                info.Email = Request.Form["Email"].TryToString();
                info.EnterName = Request.Form["EnterName"].TryToString();
                info.FaxNumber = Request.Form["FaxNumber"].TryToString();
                info.HeatMsg = Request.Form["HeatMsg"].TryToString();
                info.HeatTYPE = (HeatTypeEnum)Request.Form["HeatTYPE"].TryToInt(0);
                info.InvoiceMsg = Request.Form["InvoiceMsg"].TryToString();
                info.IsHeat = Request.Form["IsHeat"].TryToString().Equals("on");
                info.Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);
                info.Province = Request.Form["Province"].TryToString();
                info.Relationship = (RelationshipEnume)Request.Form["Relationship"].TryToInt(0);
                info.Rem = Request.Form["Rem"].TryToString();
                info.Source = (CustSource)Request.Form["Source"].TryToInt(0);
                info.Telephone = Request.Form["Telephone"].TryToString();
                info.UpdateTime = DateTime.Now;
                info.ValueGrade = (ValueGradeEnume)Request.Form["ValueGrade"].TryToInt(0);
                info.WebSit = Request.Form["WebSit"].TryToString();
                info.ZipCode = Request.Form["ZipCode"].TryToString();
                info.Landline = Request.Form["Landline"].TryToString();
                info.CreateTime = DateTime.Now;
                info.CreateUserID = User.ID;
                info.State = StateEnum.Invalid;
                info.UserID = User.ID;
                id = await bll.AddEnterCustomer(info);
                if (id>0)
                {
                    //EnterCustPhaseLog enterCustPhaseLog = new EnterCustPhaseLog();
                    //enterCustPhaseLog.CreateTime = DateTime.Now;
                    //enterCustPhaseLog.EnterCustomerID = id;
                    //enterCustPhaseLog.Phase = info.Phase;
                    //enterCustPhaseLog.UserID = User.ID;
                    //enterCustPhaseLog.UserName = User.Name;
                    //enterCustPhaseLog.Rem = "客户信息录入";
                    //await logbll.AddAsync(enterCustPhaseLog);
                    return Json(new { code = 1, msg = "OK", id=id });
                }
                else
                    return Json(new { code = 0, msg = "保存失败" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveEnterAndCust()
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            EnterCustContactsBll enterCustContactsBll = new EnterCustContactsBll();

            EnterCustomer info = new EnterCustomer();
            info.Address = Request.Form["Address"].TryToString();
            info.City = Request.Form["City"].TryToString();
            info.CustAbstract = Request.Form["CustAbstract"].TryToString();
            info.CustomerType = (CustomerTypeEnum)Request.Form["CustomerType"].TryToInt(0);
            info.DegreeOfHeat = (DegreeOfHeatEnume)Request.Form["DegreeOfHeat"].TryToInt(0);
            info.Email = Request.Form["Email"].TryToString();
            info.EnterName = Request.Form["EnterName"].TryToString();
            info.FaxNumber = Request.Form["FaxNumber"].TryToString();
            info.HeatMsg = Request.Form["HeatMsg"].TryToString();
            info.HeatTYPE = (HeatTypeEnum)Request.Form["HeatTYPE"].TryToInt(0);
            info.InvoiceMsg = Request.Form["InvoiceMsg"].TryToString();
            info.IsHeat = Request.Form["IsHeat"].TryToString().Equals("on");
            info.Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);
            info.Province = Request.Form["Province"].TryToString();
            info.Relationship = (RelationshipEnume)Request.Form["Relationship"].TryToInt(0);
            info.Rem = Request.Form["Rem"].TryToString();
            info.Source = (CustSource)Request.Form["Source"].TryToInt(0);
            //info.Telephone = Request.Form["Telephone"].TryToString();
            info.UpdateTime = DateTime.Now;
            info.ValueGrade = (ValueGradeEnume)Request.Form["ValueGrade"].TryToInt(0);
            info.WebSit = Request.Form["WebSit"].TryToString();
            info.ZipCode = Request.Form["ZipCode"].TryToString();
            //info.Landline = Request.Form["Landline"].TryToString();
            info.CreateTime = DateTime.Now;
            info.CreateUserID = User.ID;
            info.State = StateEnum.Invalid;
            info.UserID = User.ID;

            EnterCustContacts enterCustContacts = new EnterCustContacts();
            enterCustContacts.Address = Request.Form["Address"].TryToString();
            enterCustContacts.Business = Request.Form["Business"].TryToString();
            enterCustContacts.Department = Request.Form["Department"].TryToString();
            enterCustContacts.Duties = Request.Form["Duties"].TryToString();
            enterCustContacts.Email = Request.Form["Email"].TryToString();
            enterCustContacts.Name = Request.Form["Name"].TryToString();
            enterCustContacts.QQ = Request.Form["QQ"].TryToString();
            enterCustContacts.Rem = Request.Form["Rem"].TryToString();
            enterCustContacts.Sex = (SexEnum)Request.Form["Sex"].TryToInt();
            enterCustContacts.Telephone = Request.Form["Telephone"].TryToString();
            enterCustContacts.WeChart = Request.Form["WeChart"].TryToString();
            enterCustContacts.Landline = Request.Form["Landline"].TryToString();

            int id = await bll.AddEnterCustomer(info);
            if (id>0)
            {
                enterCustContacts.EnterCustID = id;
                await enterCustContactsBll.Add(enterCustContacts);
                return Json(new { code = 1, msg = "OK",id = id });
            }
            return Json(new {code =0, msg = "OK"});
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DelEnter(int id)
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            bool flag = await bll.DelAsync(id);
            if (flag)
                return Json(new { code = 1, msg = "OK" });
            return Json(new { code = 0, msg = "OK" });
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EnterCallback(int id)
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            var info =await bll.GetAsync(id);
            if (info!=null)
            {
                info.UserID = 0;
                bool flag = await bll.UpdateEnterCustomerAsync(info);
                if (flag)
                    return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "OK" });
        }

        /// <summary>
        /// 根据省份ID获取城市列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCityList(int id)
        {
            DistrictInfoBll dbll = new DistrictInfoBll();
            var citylist = await dbll.GetAsync(id);
            return Json(new { code = 1, citylist = citylist });
        }

        public async Task<IActionResult> AddEnterCustContacts(int id,int EnterCustID,int type=0)
        {
            EnterCustContactsBll bll = new EnterCustContactsBll();
            var info = await bll.GetAsync(id);
            ViewBag.EnterCustID = EnterCustID;
            ViewBag.root = "enter";
            ViewBag.type = type;
            return View(info);
        }
        /// <summary>
        /// 保存企业联系人
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveEnterCustContacts()
        {
            EnterCustContactsBll bll = new EnterCustContactsBll();
            int ID = Request.Form["ID"].TryToInt();
            if (ID > 0)
            {
                var info = await bll.GetAsync(ID);
                if (info != null)
                {
                    info.Address = Request.Form["Address"].TryToString();
                    info.Business = Request.Form["Business"].TryToString();
                    info.Department = Request.Form["Department"].TryToString();
                    info.Duties = Request.Form["Duties"].TryToString();
                    info.Email = Request.Form["Email"].TryToString();
                    info.EnterCustID = Request.Form["EnterCustID"].TryToInt();
                    info.Name = Request.Form["Name"].TryToString();
                    info.QQ = Request.Form["QQ"].TryToString();
                    info.Rem = Request.Form["Rem"].TryToString();
                    info.Sex = (SexEnum)Request.Form["Sex"].TryToInt();
                    info.Telephone = Request.Form["Telephone"].TryToString();
                    info.WeChart = Request.Form["WeChart"].TryToString();
                    info.Landline = Request.Form["Landline"].TryToString();
                    bool flag = await bll.UpdateAsync(info);
                    if (flag)
                        return Json(new { code = 1, msg = "OK" });
                }
            }
            else
            {
                EnterCustContacts info = new EnterCustContacts();
                info.Address = Request.Form["Address"].TryToString();
                info.Business = Request.Form["Business"].TryToString();
                info.Department = Request.Form["Department"].TryToString();
                info.Duties = Request.Form["Duties"].TryToString();
                info.Email = Request.Form["Email"].TryToString();
                info.EnterCustID = Request.Form["EnterCustID"].TryToInt();
                info.Name = Request.Form["Name"].TryToString();
                info.QQ = Request.Form["QQ"].TryToString();
                info.Rem = Request.Form["Rem"].TryToString();
                info.Sex = (SexEnum)Request.Form["Sex"].TryToInt();
                info.Telephone = Request.Form["Telephone"].TryToString();
                info.WeChart = Request.Form["WeChart"].TryToString();
                info.Landline = Request.Form["Landline"].TryToString();
                int id = await bll.Add(info);
                if (id > 0)
                    return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "保存失败" });
        }
        /// <summary>
        /// 公共客户池
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EnterPoolIndex()
        {
            DistrictInfoBll dbll = new DistrictInfoBll();
            var ProvinceList = await dbll.GetAsync(0);
            ViewBag.ProvinceList = ProvinceList;
            int pageIndex = 1;
            int pageSize = 20;
            string EnterName = string.Empty;
            CustomerTypeEnum? CustomerType = null;
            RelationshipEnume? Relationship = null;
            PhaseEnume? Phase = null;
            ValueGradeEnume? ValueGrade = null;
            CustSource? Source = null;
            bool? IsHeat = null;
            DegreeOfHeatEnume? DegreeOfHeat = null;
            DateTime? UpdateTime = null;
            string City = null;
            string Province = null;
            if (Request.Method=="POST")
            {
                pageIndex = Request.Form["page"].TryToInt(1); 
               
                EnterName = Request.Form["EnterName"].TryToString();

                if (Request.Form["CustomerType"].TryToInt(0) > 0)
                {
                    CustomerType = (CustomerTypeEnum)Request.Form["CustomerType"].TryToInt(0);
                    ViewBag.CustomerType = Request.Form["CustomerType"].TryToInt(0);
                }

                if (Request.Form["Relationship"].TryToInt(0) > 0)
                {
                    Relationship = (RelationshipEnume)Request.Form["Relationship"].TryToInt(0);
                    ViewBag.Relationship = Request.Form["Relationship"].TryToInt(0);
                }


                if (Request.Form["Phase"].TryToInt(0) > 0)
                {
                    Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);
                    ViewBag.Phase = Request.Form["Phase"].TryToInt(0);
                }


                if (Request.Form["ValueGrade"].TryToInt(0) > 0)
                {
                    ValueGrade = (ValueGradeEnume)Request.Form["ValueGrade"].TryToInt(0);
                    ViewBag.ValueGrade= Request.Form["ValueGrade"].TryToInt(0);
                }


                if (Request.Form["Source"].TryToInt(0) > 0)
                {
                    Source = (CustSource)Request.Form["Source"].TryToInt(0);
                    ViewBag.Source= Request.Form["Source"].TryToInt(0);
                }


                if (!Request.Form["IsHeat"].TryToString().IsNull())
                {
                    IsHeat = Request.Form["IsHeat"].TryToString() == "on";
                    ViewBag.IsHeat = IsHeat;
                }


                if (Request.Form["DegreeOfHeat"].TryToInt(0) > 0)
                {
                    DegreeOfHeat = (DegreeOfHeatEnume)Request.Form["DegreeOfHeat"].TryToInt(0);
                    ViewBag.DegreeOfHeat = Request.Form["DegreeOfHeat"].TryToInt(0); ;
                }

                
                if (!Request.Form["Province"].TryToString().IsNull())
                {
                    Province = Request.Form["Province"].TryToString();
                    ViewBag.Province = Request.Form["Province"].TryToString();
                }

               
                if (!Request.Form["City"].TryToString().IsNull())
                {
                    City = Request.Form["City"].TryToString();
                    ViewBag.City= Request.Form["City"].TryToString();
                }


                if (Request.Form["UpdateTime"].TryToInt(0) > 0)
                {
                    ViewBag.UpdateTime = Request.Form["UpdateTime"].TryToInt(0);
                    switch (Request.Form["UpdateTime"].TryToInt(0))
                    {
                        case 1:
                            UpdateTime = DateTime.Now.AddDays(-7);
                            break;
                        case 2:
                            UpdateTime = DateTime.Now.AddDays(-15);
                            break;
                        case 3:
                            UpdateTime = DateTime.Now.AddDays(-30);
                            break;
                        case 4:
                            UpdateTime = DateTime.Now.AddDays(-60);
                            break;
                        case 5:
                            UpdateTime = DateTime.Now.AddDays(-90);
                            break;
                    }
                }
            }
            ViewBag.pageIndex = pageIndex;
            ViewBag.EnterName = EnterName;
            EnterCustomerBll bll = new EnterCustomerBll();
            var list = await bll.GetAsync(pageIndex, pageSize, 0, EnterName, CustomerType, Relationship, Phase, ValueGrade, Source, IsHeat, DegreeOfHeat, Province, City, UpdateTime);
            ViewBag.count = list.Item2;
            ViewBag.root = "enter";
            return View(list.Item1);
        }
        [HttpPost]
        public async Task<IActionResult> EnterPoolList()
        {
            int pageIndex = Request.Form["page"].TryToInt(1);
            int pageSize = 20;
            string EnterName = Request.Form["EnterName"].TryToString();
            CustomerTypeEnum? CustomerType = null;
            if (Request.Form["CustomerType"].TryToInt(0) > 0)
            {
                CustomerType = (CustomerTypeEnum)Request.Form["CustomerType"].TryToInt(0);
            }
            RelationshipEnume? Relationship = null;
            if (Request.Form["Relationship"].TryToInt(0) > 0)
                Relationship = (RelationshipEnume)Request.Form["Relationship"].TryToInt(0);

            PhaseEnume? Phase = null;
            if (Request.Form["Phase"].TryToInt(0) > 0)
                Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);

            ValueGradeEnume? ValueGrade = null;
            if (Request.Form["ValueGrade"].TryToInt(0) > 0)
                ValueGrade = (ValueGradeEnume)Request.Form["ValueGrade"].TryToInt(0);

            CustSource? Source = null;
            if (Request.Form["Source"].TryToInt(0) > 0)
                Source = (CustSource)Request.Form["Source"].TryToInt(0);

            bool? IsHeat = null;
            if (!Request.Form["IsHeat"].TryToString().IsNull())
                IsHeat = Request.Form["IsHeat"].TryToString() == "on";

            DegreeOfHeatEnume? DegreeOfHeat = null;
            if (Request.Form["DegreeOfHeat"].TryToInt(0) > 0)
                DegreeOfHeat = (DegreeOfHeatEnume)Request.Form["DegreeOfHeat"].TryToInt(0);

            string Province = null;
            if (!Request.Form["Province"].TryToString().IsNull())
                Province = Request.Form["Province"].TryToString();

            string City = null;
            if (!Request.Form["City"].TryToString().IsNull())
                City = Request.Form["City"].TryToString();

            DateTime? UpdateTime = null;
            if (Request.Form["UpdateTime"].TryToInt(0) > 0)
            {
                switch (Request.Form["UpdateTime"].TryToInt(0))
                {
                    case 1:
                        UpdateTime = DateTime.Now.AddDays(-7);
                        break;
                    case 2:
                        UpdateTime = DateTime.Now.AddDays(-15);
                        break;
                    case 3:
                        UpdateTime = DateTime.Now.AddDays(-30);
                        break;
                    case 4:
                        UpdateTime = DateTime.Now.AddDays(-60);
                        break;
                    case 5:
                        UpdateTime = DateTime.Now.AddDays(-90);
                        break;
                }
            }
            EnterCustomerBll bll = new EnterCustomerBll();
            var list = await bll.GetAsync(pageIndex, pageSize, 0, EnterName, CustomerType, Relationship, Phase, ValueGrade, Source, IsHeat, DegreeOfHeat, Province, City, UpdateTime);
            ViewBag.count = list.Item2;
            return View(list.Item1);
        }

        /// <summary>
        /// 领用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UseEnterCust(int id)
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            var info = await bll.GetAsync(id);
            if(info != null)
            {
                info.UserID = User.ID;
                info.CreateTime = DateTime.Now;
                bool flag =await bll.UpdateEnterCustomerAsync(info);
                if (flag)
                    return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "OK" });
        }
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DistrutionEc(int userid,int eid)
        {
            EnterCustomerBll bll = new EnterCustomerBll();
            var info = await bll.GetAsync(eid);
            if (info!=null)
            {
                info.UserID = userid;
                bool flag = await bll.UpdateEnterCustomerAsync(info);
                if (flag)
                    return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "OK" });
        }
        /// <summary>
        /// 保存客户阶段日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveEnterCustPhaseLog()
        {
            EnterCustPhaseLogBll bll = new EnterCustPhaseLogBll();
            EnterCustPhaseLog log = new EnterCustPhaseLog();
            log.CreateTime = DateTime.Now;
            log.EnterCustomerID = Request.Form["EnterCustomerID"].TryToInt();
            log.Phase = (PhaseEnume)Request.Form["Phase"].TryToInt(0);
            log.Rem = Request.Form["Rem"].TryToString();
            log.UserID = User.ID;
            log.UserName = User.Name;
            int id = await bll.AddAsync(log);
            if (id>0)
            {
                EnterCustomerBll ebll = new EnterCustomerBll();
                var info = await ebll.GetAsync(log.EnterCustomerID);
                if (info!=null)
                {
                    info.Phase = log.Phase;
                    info.UpdateTime = DateTime.Now;
                    await ebll.UpdateEnterCustomerAsync(info);
                }
                return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "保存失败" });
        }

        public async Task<IActionResult> EnterCustPhaseLogList(int id)
        {
            string EnterName = string.Empty;
            List<EnterCustPhaseLog> LogList = null;
            EnterCustomerBll bll = new EnterCustomerBll();
            var info = await bll.GetAsync(id);
            if (info != null)
            {
                EnterName = info.EnterName;
                EnterCustPhaseLogBll logbll = new EnterCustPhaseLogBll();
                LogList = await logbll.ListAsync(id);
            }
            ViewBag.EnterName = EnterName;
            ViewBag.root = "enter";
            return View(LogList);
        }
        /// <summary>
        /// 销售项目首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SalesProjectIndex()
        {
            string Title = string.Empty ;
            if (Request.Method=="POST")
            {
                Title = Request.Form["Title"].TryToString();
            }
            ViewBag.Title = Title;

            ProjectStateEnum? ProjectState = null;
            ViewBag.ProjectState = 0;
            if (Request.Method=="POST" &&  Request.Form["ProjectState"].TryToInt(0) > 0)
            {
                ProjectState = (ProjectStateEnum)Request.Form["ProjectState"].TryToInt();
                ViewBag.ProjectState = Request.Form["ProjectState"].TryToInt();
            }
            ProjectTypeEnum? ProjectType = null;
            ViewBag.ProjectType = 0;
            if (Request.Method == "POST" && Request.Form["ProjectType"].TryToInt(0) > 0)
            {
                ProjectType = (ProjectTypeEnum)Request.Form["ProjectType"].TryToInt(0);
                ViewBag.ProjectType = Request.Form["ProjectType"].TryToInt(0);
            }
            DateTime? ProjectStartTime = null;
            ViewBag.ProjectStartTime = string.Empty;
            if (Request.Method == "POST" && !Request.Form["ProjectStartTime"].TryToString().IsNull())
            {
                ProjectStartTime = Request.Form["ProjectStartTime"].TryToDateTime();
                ViewBag.ProjectStartTime = Request.Form["ProjectStartTime"].TryToString();
            }
            DateTime? ProjectEndTime = null;
            ViewBag.ProjectEndTime = string.Empty;
            if (Request.Method == "POST" && !Request.Form["ProjectEndTime"].TryToString().IsNull())
            {
                ProjectEndTime = Request.Form["ProjectEndTime"].TryToDateTime();
                ViewBag.ProjectEndTime = Request.Form["ProjectEndTime"].TryToString();
            }
            int pageIndex = 1;
            if (Request.Method=="POST")
            {
                pageIndex = Request.Form["page"].TryToInt(1);
            }
            ViewBag.pageIndex = pageIndex;
            int pageSize = 20;
            SalesProjectBll bll = new SalesProjectBll();
            List<EnterCustomer> enterlist = new List<EnterCustomer>();
            var list = await bll.GetTupleAsync(pageIndex, pageSize, User.ID, Title, null, ProjectState, ProjectType, ProjectStartTime, ProjectEndTime);
            if (list.Item1!=null)
            {
                EnterCustomerBll enterCustomerBll = new EnterCustomerBll();
                foreach (var info in list.Item1)
                {
                    var enter =await enterCustomerBll.GetAsync(info.EnterCustomerID);
                    if (enter!=null)
                    {
                        enterlist.Add(enter);
                    }
                }
            }

            Dictionary<int, double> recpayDic = new Dictionary<int, double>();
            if (list != null && list.Item1.Count > 0)
            {
                ReceivedPaymentsLogBll rpbll = new ReceivedPaymentsLogBll();
                foreach (var info in list.Item1)
                {
                    var rplog = await rpbll.GetListAsync(info.ID);
                    if (rplog != null && rplog.Count > 0)
                    {
                        var rp = from log in rplog
                                 select log.Amt;
                        recpayDic[info.ID] = rp.Sum();
                    }
                }
            }
            ViewBag.recpayDic = recpayDic;

            ViewBag.enterlist = enterlist;
            ViewBag.Count = list.Item2;
            ViewBag.root = "sale";
            return View(list.Item1);
        }
        [HttpPost]
        public async Task<IActionResult> SalesProjectList()
        {
            string Title = Request.Form["Title"].TryToString();
            ProjectStateEnum? ProjectState = null;
            if (Request.Form["ProjectState"].TryToInt(0)>0)
            {
                ProjectState = (ProjectStateEnum)Request.Form["ProjectState"].TryToInt();
            }
            ProjectTypeEnum? ProjectType = null;
            if (Request.Form["ProjectType"].TryToInt(0)>0)
            {
                ProjectType = (ProjectTypeEnum)Request.Form["ProjectType"].TryToInt(0);
            }
            DateTime? ProjectStartTime = null;
            if (!Request.Form["ProjectStartTime"].TryToString().IsNull())
            {
                ProjectStartTime = Request.Form["ProjectStartTime"].TryToDateTime();
            }
            DateTime? ProjectEndTime = null;
            if (!Request.Form["ProjectEndTime"].TryToString().IsNull())
            {
                ProjectEndTime = Request.Form["ProjectEndTime"].TryToDateTime();
            }
            int pageIndex = Request.Form["page"].TryToInt(1);
            int pageSize = 20;
            SalesProjectBll bll = new SalesProjectBll();
            var list = await bll.GetTupleAsync(pageIndex, pageSize, User.ID,Title,null,ProjectState,ProjectType,ProjectStartTime,ProjectEndTime);
            ViewBag.Count = list.Item2;
            ViewBag.pageIndex = pageIndex;
            return View(list.Item1);
        }
        /// <summary>
        /// 添加销售项目
        /// </summary>
        /// <param name="EnterCustomerID"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddSalesProject(int EnterCustomerID)
        {
            UserBll userBll = new UserBll();
            var userlist = await userBll.GetListAsync();
            ViewBag.userlist = userlist;
            ViewBag.EnterCustomerID = EnterCustomerID;
            ViewBag.root = "sale";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveSalesProject()
        {
            SalesProject salesProject = new SalesProject();
            salesProject.CreateTime = DateTime.Now;
            salesProject.CreateUserID = User.ID;
            salesProject.EnterCustomerID = Request.Form["EnterCustomerID"].TryToInt();
            salesProject.HeadID = Request.Form["HeadID"].TryToInt();
            salesProject.ProjectAbstract = Request.Form["ProjectAbstract"].TryToString();
            salesProject.ProjectAmt = Request.Form["ProjectAmt"].TryToDouble();
            salesProject.ProjectState = (ProjectStateEnum)Request.Form["ProjectState"].TryToInt(0);
            salesProject.ProjectTime = Request.Form["ProjectTime"].TryToDateTime();
            salesProject.ProjectType = (ProjectTypeEnum)Request.Form["ProjectType"].TryToInt(0);
            salesProject.Title = Request.Form["Title"].TryToString();
            salesProject.ReceoverPayTime = Request.Form["ReceoverPayTime"].TryToDateTime();
            SalesProjectBll bll = new SalesProjectBll();
            int id = await bll.AddAsync(salesProject);
            if (id>0)
            {
                SalesProjectStateLog log = new SalesProjectStateLog();
                log.UserName = User.UserName;
                log.UserID = User.ID;
                log.CreateTime = DateTime.Now;
                log.ProjectState = salesProject.ProjectState;
                log.Rem = "创建项目";
                log.SalesProjectID = id;
                SalesProjectStateLogBll salesProjectStateLogBll = new SalesProjectStateLogBll();
                await salesProjectStateLogBll.AddAsync(log);
                return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "保存失败" });
        }

        public IActionResult AddSalesProjectStateLog(int SalesProjectID)
        {
            ViewBag.SalesProjectID = SalesProjectID;
            ViewBag.root = "sale";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveSalesProjectStateLog()
        {
            SalesProjectStateLog log = new SalesProjectStateLog();
            log.CreateTime = DateTime.Now;
            log.ProjectState = (ProjectStateEnum)Request.Form["ProjectState"].TryToInt(0);
            log.Rem = Request.Form["Rem"].TryToString();
            log.SalesProjectID = Request.Form["SalesProjectID"].TryToInt(0);
            log.UserID = User.ID;
            log.UserName = User.Name;

            SalesProjectStateLogBll bll = new SalesProjectStateLogBll();
            int id = await bll.AddAsync(log);
            if (id>0)
            {
                SalesProjectBll pbll = new SalesProjectBll();
                var project = await pbll.GetAsync(log.SalesProjectID);
                if (project!=null && project.ProjectState != log.ProjectState)
                {
                    project.ProjectState = log.ProjectState;
                    await pbll.UpdateAsync(project);
                }
                return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, mes = "保存失败" });
        }
        /// <summary>
        /// 销售项目状态变更时间线
        /// </summary>
        /// <param name="SalesProjectID"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSalesProjectStateLog(int SalesProjectID)
        {
            SalesProjectStateLogBll bll = new SalesProjectStateLogBll();
            var list = await bll.GetListAsync(SalesProjectID);
            SalesProjectBll pbll = new SalesProjectBll();
            var project =await pbll.GetAsync(SalesProjectID);
            string Title = string.Empty;
            if (project!=null)
            {
                Title = project.Title;
            }
            ViewBag.name = Title;
            ViewBag.root = "sale";
            return View(list);
        }

        /// <summary>
        /// 添加工作计划
        /// </summary>
        /// <param name="EnterCustID"></param>
        /// <returns></returns>
        public IActionResult AddWorkPlan(int EnterCustID)
        {
            ViewBag.EnterCustID = EnterCustID;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveWorkPlan()
        {
            WorkPlan workPlan = new WorkPlan();
            workPlan.CreateTime = DateTime.Now;
            workPlan.EnterCustID = Request.Form["EnterCustID"].TryToInt(0);
            workPlan.PlanContent = Request.Form["PlanContent"].TryToString();
            workPlan.PlanTime = Request.Form["PlanTime"].TryToDateTime();
            workPlan.UserID = User.ID;
            workPlan.WorkPlanState = WorkPlanStateEnum.NoFinish;

            WorkPlanBll bll = new WorkPlanBll();
            int id = await bll.AddAsync(workPlan);
            if(id>0)
                return Json(new { code = 1, msg = "OK" });
            return Json(new { code = 0, msg = "保存失败" });
        }

        /// <summary>
        /// 工作计划
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> WorkPlanIndex(int pageIndex)
        {
            int pageSize = 30;
            WorkPlanBll bll = new WorkPlanBll();
            var list = await bll.TupleAsync(User.ID, pageIndex, pageSize);
            ViewBag.count = list.Item2;
            List<EnterCustomer> elist = new List<EnterCustomer>();
            if (list != null && list.Item1.Count > 0)
            {
                EnterCustomerBll ebll = new EnterCustomerBll();
                foreach (var info in list.Item1)
                {
                    var e = await ebll.GetAsync(info.EnterCustID);
                    if (e != null)
                    {
                        elist.Add(e);
                    }
                }
            }
            ViewBag.pageIndex = pageIndex;
            ViewBag.elist = elist;
            return View(list.Item1);
        }

        [HttpGet]
        public async Task<IActionResult> FinishPlan(int id)
        {
            WorkPlanBll bll = new WorkPlanBll();
            var workplan = await bll.GetAsync(id);
            workplan.WorkPlanState = WorkPlanStateEnum.Finish;
            await bll.UpdateAsync(workplan);
            return Json(new { code = 1, msg = "OK" });
        }
        [HttpGet]
        public async Task<IActionResult> DelPlan(int id)
        {
            WorkPlanBll bll = new WorkPlanBll();
            var workplan = await bll.GetAsync(id);
            if (workplan!=null)
            {
                await bll.DelAsync(workplan);
            }
            return Json(new { code = 1, msg = "OK" });
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ConsoleIndex()
        {
            EnterCustomerBll customerBll = new EnterCustomerBll();
            SalesProjectBll salesProjectBll = new SalesProjectBll();

            ViewBag.entercustomTotalToday = null; //今天客户数量
            ViewBag.entercustomTotalWeek = null; // 本周客户数量
            ViewBag.entercustomTotalMonth = null;// 本月客户数量

            List<UserEnterReport> entercustomTotalToday = await customerBll.GetAsync(DateTime.Now.ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(),User.ID);
            List<UserEnterReport> entercustomTotalWeek = await customerBll.GetAsync(DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek+1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            List<UserEnterReport> entercustomTotalMonth = await customerBll.GetAsync(DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            if (entercustomTotalToday!=null && entercustomTotalToday.Count>0)
            {
                ViewBag.entercustomTotalToday = entercustomTotalToday.First();
            }
            if (entercustomTotalWeek!=null && entercustomTotalWeek.Count>0)
            {
                ViewBag.entercustomTotalWeek = entercustomTotalWeek.First();
            }
            if (entercustomTotalMonth!=null && entercustomTotalMonth.Count>0)
            {
                ViewBag.entercustomTotalMonth = entercustomTotalMonth[0];
            }

            ViewBag.projecttotalToday = 0; //今天成单数量
            ViewBag.projecttotalWeek = 0; //本周成单数量
            ViewBag.projecttotalMonth = 0; // 本月成单数量

            var projectToday = await salesProjectBll.GetAsync(DateTime.Now.ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            if (projectToday!=null && projectToday.Count>0)
            {
                ViewBag.projecttotalToday = projectToday.First().Total;
            }

            var projectWeek = await salesProjectBll.GetAsync(DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            if (projectWeek!=null && projectWeek.Count>0)
            {
                ViewBag.projecttotalWeek = projectWeek.First().Total;
            }

            var projectMonth = await salesProjectBll.GetAsync(DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            if (projectMonth!=null && projectMonth.Count>0)
            {
                ViewBag.projecttotalMonth = projectMonth.First().Total;
            }

            ViewBag.telphoneToday = 0;// 今天电话量
            ViewBag.telphoneWeek = 0;// 本周电话量
            ViewBag.telphoneMonth = 0;// 本月电话量

            EnterCustPhaseLogBll phlogbll = new EnterCustPhaseLogBll();
            var telphoneToday = await phlogbll.GetAsync(DateTime.Now.ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            if (telphoneToday!=null && telphoneToday.Count>0)
            {
                ViewBag.telphoneToday = telphoneToday.First().Total;
            }
            var telphoneWeek = await phlogbll.GetAsync(DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);

            if (telphoneWeek!=null && telphoneWeek.Count>0)
            {
                ViewBag.telphoneWeek = telphoneWeek.First().Total;
            }
            var telphoneMonth = await phlogbll.GetAsync(DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            if (telphoneMonth!=null && telphoneMonth.Count>0)
            {
                ViewBag.telphoneMonth = telphoneMonth.First().Total;
            }
            ViewBag.ReceoverPay = 0;//本月应收账款
            var projectlist =await salesProjectBll.GetListAsync(User.ID, DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime());
            if (projectlist!=null && projectlist.Count>0)
            {
                var amtlist = from project in projectlist
                              select project.ProjectAmt;
                if (amtlist!=null)
                {
                    ViewBag.ReceoverPay = amtlist.Sum();
                }
            }
            ViewBag.HReceoverPay = 0;// 本月已收账款
            ReceivedPaymentsLogBll receivedPaymentsLogBll = new ReceivedPaymentsLogBll();
            var receivedlist = await receivedPaymentsLogBll.GetListAsync(DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            if (receivedlist!=null)
            {
                var amtlist = from received in receivedlist
                              select received.Amt;
                if (amtlist!=null)
                {
                    ViewBag.HReceoverPay = amtlist.Sum();
                }
            }
            ViewBag.TargetAmt = User.TargetAmt;
            WorkPlanBll planbll = new WorkPlanBll();
            List<EnterCustomer> enterlist = new List<EnterCustomer>();
            var workplanlist = await planbll.ListAsync(User.ID);
            if (workplanlist!=null)
            {
                foreach (var plan in workplanlist)
                {
                    var enter =await customerBll.GetAsync(plan.EnterCustID);
                    if (enter != null)
                        enterlist.Add(enter);
                }
            }
            ViewBag.workplanlist = workplanlist;
            ViewBag.enterlist = enterlist;
            ViewBag.root = "console";

            int userid = User.ID;
            if (User.UserName == "admin")
            {
                userid = 0;
            }
            DateTime startTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd").TryToDateTime();
            DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").TryToDateTime();
            UserBll userBll = new UserBll();
            List<UserInfo> userlist = await userBll.GetListAsync();
            
            // 成单量统计
            List<UserEnterReport> entercustomTotal = await salesProjectBll.GetAsync(startTime, endTime, 0);
            // 客户量统计
            List<UserEnterReport> customerTotal = await customerBll.GetAsync(startTime, endTime, 0);

            //电话量统计
            List<UserEnterReport> phonetotal = await phlogbll.GetAsync(startTime, endTime, 0);

            List<UserReportViewModel> reportlist = new List<UserReportViewModel>();
            // 应收账款
            var ReceoverPayList = await salesProjectBll.GetListAsync(0, startTime, endTime);
            
            var ReceivedPaymentsLogList = await receivedPaymentsLogBll.GetListAsync(startTime, endTime, 0);

            if (userlist != null)
            {
                foreach (var user in userlist)
                {
                    UserReportViewModel userReportView = new UserReportViewModel();
                    if (customerTotal != null && customerTotal.Count > 0)
                    {
                        var customer = customerTotal.FirstOrDefault(x => x.UserID == user.ID);
                        if (customer != null)
                        {
                            userReportView.CustomorTotal = customer.Total;
                        }
                    }

                    if (entercustomTotal != null && entercustomTotal.Count > 0)
                    {
                        var enter = entercustomTotal.FirstOrDefault(x => x.UserID == user.ID);
                        if (enter != null)
                        {
                            userReportView.SalesProjectTotal = enter.Total;
                        }
                    }

                    if (phonetotal != null && phonetotal.Count > 0)
                    {
                        var phone = phonetotal.FirstOrDefault(x => x.UserID == user.ID);
                        if (phone != null)
                        {
                            userReportView.PhoneTotal = phone.Total;
                        }
                    }
                    userReportView.UserID = user.ID;
                    userReportView.UserName = user.Name;
                    userReportView.TargetAmt = user.TargetAmt;
                    if (ReceoverPayList != null && ReceoverPayList.Count > 0)
                    {
                        var rlist = from rpay in ReceoverPayList
                                    where rpay.HeadID == user.ID
                                    select rpay.ProjectAmt;
                        if (rlist != null)
                            userReportView.ReceoverPay = rlist.Sum();
                    }

                    if (ReceivedPaymentsLogList != null && ReceivedPaymentsLogList.Count > 0)
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
            ViewBag.reportlist = reportlist;
            bool isadmin = false;
            if (User.UserName == "admin")
                isadmin = true;
            ViewBag.isadmin = isadmin;
            return View();
        }
        /// <summary>
        /// 昨天的客户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<IActionResult> CustomorTotalList(int userid)
        {
            DateTime startTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd").TryToDateTime();
            DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").TryToDateTime();
            EnterCustomerBll enterCustomerBll = new EnterCustomerBll();
            var list = await enterCustomerBll.ListAsync(startTime,endTime, userid);
            return View(list);
        }

        public async Task<IActionResult> CustomorTotalLReport(string startTime, string endTime, int userid)
        {
            EnterCustomerBll enterCustomerBll = new EnterCustomerBll();
            var list = await enterCustomerBll.ListAsync(startTime.TryToDateTime(), endTime.TryToDateTime(), userid);
            return View(list);
        }

        /// <summary>
        /// 昨天电话信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<IActionResult> PhoneTotalList(int userid)
        {
            DateTime startTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd").TryToDateTime();
            DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").TryToDateTime();
            EnterCustPhaseLogBll phlogbll = new EnterCustPhaseLogBll();
            var list = await phlogbll.ListAsync(startTime,endTime,userid);
            List<EnterCustomer> enlist = new List<EnterCustomer>();
            if (list!=null)
            {
                EnterCustomerBll bll = new EnterCustomerBll();
                foreach (var log in list)
                {
                    EnterCustomer ec =await bll.GetAsync(log.EnterCustomerID);
                    if (ec!=null)
                    {
                        enlist.Add(ec);
                    }
                }
            }
            ViewBag.enlist = enlist;
            return View(list);
        }

        public async Task<IActionResult> PhoneTotalReport(int userid,string startTime, string endTime)
        {
           
            EnterCustPhaseLogBll phlogbll = new EnterCustPhaseLogBll();
            var list = await phlogbll.ListAsync(startTime.TryToDateTime(), endTime.TryToDateTime(), userid);
            List<EnterCustomer> enlist = new List<EnterCustomer>();
            if (list != null)
            {
                EnterCustomerBll bll = new EnterCustomerBll();
                foreach (var log in list)
                {
                    EnterCustomer ec = await bll.GetAsync(log.EnterCustomerID);
                    if (ec != null)
                    {
                        enlist.Add(ec);
                    }
                }
            }
            ViewBag.enlist = enlist;
            return View(list);
        }
        /// <summary>
        /// 添加回款记录
        /// </summary>
        /// <param name="SalesProjectID"></param>
        /// <returns></returns>
        public IActionResult AddReceovedPayLog(int SalesProjectID)
        {
            ViewBag.SalesProjectID = SalesProjectID;
            ViewBag.root = "sale";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveReceovedPayLog()
        {
            ReceivedPaymentsLog log = new ReceivedPaymentsLog();
            log.Amt = Request.Form["Amt"].TryToDouble();
            log.CreateTime = DateTime.Now;
            log.Rem = Request.Form["Rem"].TryToString();
            log.SalesProjectID = Request.Form["SalesProjectID"].TryToInt();
            log.UserID = User.ID;
            ReceivedPaymentsLogBll bll = new ReceivedPaymentsLogBll();
            int id = await bll.AddAsync(log);
            SalesProjectBll salesProjectBll = new SalesProjectBll();
            if (id > 0)
            {
                var project = await salesProjectBll.GetAsync(log.SalesProjectID);
                project.ReceoverPay = project.ReceoverPay + log.Amt;
                await salesProjectBll.UpdateAsync(project);
                return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "保存失败" });
        }

        /// <summary>
        /// 本月应收账款的项目
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ProjectAmtList()
        {
            SalesProjectBll salesProjectBll = new SalesProjectBll();
            List<EnterCustomer> enterlist = new List<EnterCustomer>();
            var list = await salesProjectBll.GetListAsync(User.ID, DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime());
            if (list!=null && list.Count>0)
            {
                EnterCustomerBll bll = new EnterCustomerBll();
                foreach (var project in list)
                {
                    var info =await bll.GetAsync(project.EnterCustomerID);
                    enterlist.Add(info);
                }
            }
            ViewBag.enterlist = enterlist;
            return View(list);
        }
        /// <summary>
        /// 本月已经已收账款明细
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ReceovedPayLogList()
        {
            ReceivedPaymentsLogBll bll = new ReceivedPaymentsLogBll();
            var list =await bll.GetListAsync(DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd").TryToDateTime(), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd").TryToDateTime(), User.ID);
            List<SalesProject> projectlist = new List<SalesProject>();
            if (list!=null && list.Count>0)
            {
                SalesProjectBll salesProjectBll = new SalesProjectBll();
                foreach (var info in list)
                {
                    SalesProject salesProject =await salesProjectBll.GetAsync(info.SalesProjectID);
                    if (salesProject!=null)
                    {
                        projectlist.Add(salesProject);
                    }
                }
            }
            ViewBag.projectlist = projectlist;
            return View(list);
        }

        /// <summary>
        /// 设置月到账目标金额
        /// </summary>
        /// <param name="amt"></param>
        /// <returns></returns>
        public async Task<IActionResult> SetAmtTarget(double amt)
        {
            User.TargetAmt = amt;
            UserBll bll = new UserBll();
            await bll.Update(User);
            return Json(new { code = 1, msg = "OK" });
        }

        public async Task<IActionResult> AddEnterCustPhaseLog(int id,int types=0)
        {
            EnterCustomerBll customerBll = new EnterCustomerBll();
            var enter = await customerBll.GetAsync(id);
            ViewBag.types = types;
            return View(enter);
        }

        
    }
}