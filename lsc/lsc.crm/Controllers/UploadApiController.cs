using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bnuxq.Common;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using bnuxq.Bll;
using bnuxq.Model;

namespace bnuxq.crm.Controllers
{
    public class UploadApiController : Controller
    {
        private IHostingEnvironment hostingEnv;
        public UploadApiController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }
        [HttpPost]
        public async Task<IActionResult> uploadImage()
        {

            JsonResult<PicMsg> result = new JsonResult<PicMsg>();
            result.code = 1;
            result.msg = "";
            string url = string.Empty;
            // 定义允许上传的文件扩展名 
            const string fileTypes = "gif,jpg,jpeg,png,bmp";
            // 最大文件大小(200KB)
            const int maxSize = 505000;
            
            // 获取附带POST参数值
            for (var fileId = 0; fileId < Request.Form.Files.Count; fileId++)
            {
                var curFile = Request.Form.Files[fileId];
                if (curFile.Length < 1) { continue; }
                else if (curFile.Length > maxSize)
                {
                    result.msg = "上传文件中有图片大小超出500KB!";
                    return Json(result);
                }
                var fileExt = Path.GetExtension(curFile.FileName);
                if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                {
                    result.msg = "上传文件中包含不支持文件格式!";
                    return Json(result);
                }
                else
                {
                    // 存储文件名
                    string FileName = DateTime.Now.ToString("yyyyMMddhhmmssff") + CreateRandomCode(8) + fileExt;

                    // 存储路径（绝对路径）
                    string virtualPath = Path.Combine(hostingEnv.WebRootPath, "img");
                    if (!Directory.Exists(virtualPath))
                    {
                        Directory.CreateDirectory(virtualPath);
                    }
                    string filepath = Path.Combine(virtualPath, FileName);
                    try
                    {
                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            await curFile.CopyToAsync(fs);
                            fs.Flush();
                        }
                        result.code = 0;
                        result.msg = "OK";
                        result.data = new PicMsg();
                        result.data.src = "/img/"+ FileName;
                        result.data.title = FileName;
                    }
                    catch (Exception exception)
                    {
                        result.msg = "上传失败:" + exception.Message;
                    }
                }
            }
            return Json(result);
        }
        /// <summary>
        /// 上传客户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> uploadEnterCustom()
        {
            JsonResult<PicMsg> result = new JsonResult<PicMsg>();
            result.code = 1;
            result.msg = "";
            int UserID = Request.Form["UserID"].TryToInt();
            string UserName = string.Empty;
            UserBll userBll = new UserBll();
            UserInfo user = await userBll.GetByID(UserID);
            if (user!=null)
            {
                UserName = user.Name;
            }
           
            // 获取附带POST参数值
            for (var fileId = 0; fileId < Request.Form.Files.Count; fileId++)
            {
                var curFile = Request.Form.Files[fileId];
                if (curFile.Length < 1) { continue; }
                var fileExt = Path.GetExtension(curFile.FileName);
                if (String.IsNullOrEmpty(fileExt) || fileExt!= ".xlsx")
                {
                    result.msg = "上传文件中包含不支持文件格式!";
                    return Json(result);
                }
                else
                {
                    // 存储文件名
                    string FileName = DateTime.Now.ToString("yyyyMMddhhmmssff") + CreateRandomCode(8) + fileExt;

                    // 存储路径（绝对路径）
                    string virtualPath = Path.Combine(hostingEnv.WebRootPath, "file");
                    if (!Directory.Exists(virtualPath))
                    {
                        Directory.CreateDirectory(virtualPath);
                    }
                    string filepath = Path.Combine(virtualPath, FileName);
                    try
                    {
                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            await curFile.CopyToAsync(fs);
                            fs.Flush();
                        }
                        FileInfo file = new FileInfo(filepath);
                        using (ExcelPackage package = new ExcelPackage(file))
                        {
                            StringBuilder sb = new StringBuilder();
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                            int rowCount = worksheet.Dimension.Rows;
                            int ColCount = worksheet.Dimension.Columns;
                            if (rowCount<2 || ColCount<17)
                            {
                                result.msg = "Excel模板不正确";
                                return Json(result);
                            }
                            StringBuilder stringBuilder = new StringBuilder();
                            Dictionary<string, int> enteridDIc = new Dictionary<string, int>();
                            #region 客户信息
                            EnterCustomerBll bll = new EnterCustomerBll();
                            //EnterCustPhaseLogBll logbll = new EnterCustPhaseLogBll();
                            for (int row =2; row < rowCount; row++)
                            {
                                EnterCustomer enterCustomer = new EnterCustomer();
                                for (int col = 1;col<ColCount;col++)
                                {
                                    string value = worksheet.Cells[row, col].Value.TryToString().Trim();
                                    switch (col)
                                    {
                                        case 1:
                                            enterCustomer.EnterName = value;
                                            break;
                                        case 2:
                                            enterCustomer.Province = value;
                                            break;
                                        case 3:
                                            enterCustomer.City = value;
                                            break;
                                        case 4:
                                            enterCustomer.Telephone = value;
                                            break;
                                        case 5:
                                            enterCustomer.Landline = value;
                                            break;
                                        case 6:
                                            enterCustomer.FaxNumber = value;
                                            break;
                                        case 7:
                                            enterCustomer.ZipCode = value;
                                            break;
                                        case 8:
                                            enterCustomer.Email = value;
                                            break;
                                        case 9:
                                            enterCustomer.WebSit = value;
                                            break;
                                        case 10:
                                            enterCustomer.Address = value;
                                            break;
                                        case 11:
                                            enterCustomer.CustAbstract = value;
                                            break;
                                        case 12:
                                            if (value.IsNull())
                                                break;
                                            switch (value)
                                            {
                                                case "代理经销商":
                                                    enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.Dealer;
                                                    break;
                                                case "普通客户":
                                                    enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.Ordinary;
                                                    break;
                                                case "集团大客户":
                                                    enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.BigCustomer;
                                                    break;
                                                case "业务合作商":
                                                    enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.Cooperation;
                                                    break;
                                                case "怀疑同行":
                                                    enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.Same;
                                                    break;
                                                case "其他客户":
                                                    enterCustomer.CustomerType = Model.Enume.CustomerTypeEnum.Other;
                                                    break;
                                            }
                                            break;
                                        case 13:
                                            if (value.IsNull())
                                                break;
                                            switch (value)
                                            {
                                                case "密切":
                                                    enterCustomer.Relationship = Model.Enume.RelationshipEnume.Intimate;
                                                    break;
                                                case "较好":
                                                    enterCustomer.Relationship = Model.Enume.RelationshipEnume.Better;
                                                    break;
                                                case "一般":
                                                    enterCustomer.Relationship = Model.Enume.RelationshipEnume.Commonly;
                                                    break;
                                                case "较差":
                                                    enterCustomer.Relationship = Model.Enume.RelationshipEnume.Poor;
                                                    break;

                                            }
                                            break;
                                        case 14:
                                            if (value.IsNull())
                                                break;
                                            switch (value)
                                            {
                                                case "高":
                                                    enterCustomer.ValueGrade = Model.Enume.ValueGradeEnume.Senior;
                                                    break;
                                                case "中":
                                                    enterCustomer.ValueGrade = Model.Enume.ValueGradeEnume.Intermediate;
                                                    break;
                                                case "低":
                                                    enterCustomer.ValueGrade = Model.Enume.ValueGradeEnume.Lower;
                                                    break;
                                            }
                                            break;
                                        case 15:
                                            if (value.IsNull())
                                                break;
                                            switch (value)
                                            {
                                                case "客户来电":
                                                    enterCustomer.Source = Model.Enume.CustSource.CustTelephone;
                                                    break;
                                                case "主动挖掘":
                                                    enterCustomer.Source = Model.Enume.CustSource.Excavate;
                                                    break;
                                                case "网站咨询":
                                                    enterCustomer.Source = Model.Enume.CustSource.WebConsulting;
                                                    break;
                                                case "客户介绍":
                                                    enterCustomer.Source = Model.Enume.CustSource.Introduction;
                                                    break;
                                                case "其他来源":
                                                    enterCustomer.Source = Model.Enume.CustSource.Other;
                                                    break;
                                            }
                                            break;
                                        case 16:
                                            if (value.IsNull())
                                                break;
                                            switch (value)
                                            {
                                                case "售前跟踪":
                                                    enterCustomer.Phase = Model.Enume.PhaseEnume.Pre_sale;
                                                    break;
                                                case "需求确定":
                                                    enterCustomer.Phase = Model.Enume.PhaseEnume.Demand_Confirmation;
                                                    break;
                                                case "售中跟单":
                                                    enterCustomer.Phase = Model.Enume.PhaseEnume.In_Sales;
                                                    break;
                                                case "签约洽谈":
                                                    enterCustomer.Phase = Model.Enume.PhaseEnume.Sign_Contract;
                                                    break;
                                                case "成交售后":
                                                    enterCustomer.Phase = Model.Enume.PhaseEnume.After_Sale;
                                                    break;
                                                case "跟单失败":
                                                    enterCustomer.Phase = Model.Enume.PhaseEnume.Invalid;
                                                    break;
                                                case "暂且搁置":
                                                    enterCustomer.Phase = Model.Enume.PhaseEnume.Shelve;
                                                    break;
                                                case "其他阶段":
                                                    enterCustomer.Phase = Model.Enume.PhaseEnume.Other;
                                                    break;
                                            }
                                            break;
                                        case 17:
                                            if (value.IsNull())
                                                break;
                                            switch (value)
                                            {
                                                case "是":
                                                    enterCustomer.IsHeat = true;
                                                    break;
                                                case "否":
                                                    enterCustomer.IsHeat = false;
                                                    break;
                                            }
                                            break;
                                        case 18:
                                            if (value.IsNull())
                                                break;
                                            switch (value)
                                            {
                                                case "低热":
                                                    enterCustomer.DegreeOfHeat = Model.Enume.DegreeOfHeatEnume.Lower;
                                                    break;
                                                case "中热":
                                                    enterCustomer.DegreeOfHeat = Model.Enume.DegreeOfHeatEnume.Intermediate;
                                                    break;
                                                case "高热":
                                                    enterCustomer.DegreeOfHeat = Model.Enume.DegreeOfHeatEnume.Senior;
                                                    break;
                                            }
                                            break;
                                        case 19:
                                            if (value.IsNull())
                                                break;
                                            switch (value)
                                            {
                                                case "高意向客户":
                                                    enterCustomer.HeatTYPE = Model.Enume.HeatTypeEnum.Intentional;
                                                    break;
                                                case "重点跟踪客户":
                                                    enterCustomer.HeatTYPE = Model.Enume.HeatTypeEnum.Key_Account;
                                                    break;
                                                case "有望签单客户":
                                                    enterCustomer.HeatTYPE = Model.Enume.HeatTypeEnum.Hopeful;
                                                    break;
                                            }
                                            break;
                                        case 20:
                                            enterCustomer.CreateTime = value.TryToDateTime();
                                            break;
                                    }
                                }
                                enterCustomer.CreateUserID = UserID;
                                enterCustomer.UpdateTime = DateTime.Now;

                                enterCustomer.UserID = UserID;
                                
                                bool flag = await bll.ExistsEnterNameAsync(0, enterCustomer.EnterName);
                                if (flag)
                                {
                                    stringBuilder.AppendLine(string.Format("{0}已存在", enterCustomer.EnterName));
                                    continue;
                                }
                                int id = await bll.AddEnterCustomer(enterCustomer);
                                if (id > 0)
                                {
                                    enteridDIc[enterCustomer.EnterName] = id;
                                }
                                else
                                {
                                    stringBuilder.AppendLine(string.Format("企业【{0}】|添加失败", enterCustomer.EnterName));
                                }
                            }
                            #endregion

                            #region 联系人信息
                            EnterCustContactsBll enterCustContactsBll = new EnterCustContactsBll();
                            ExcelWorksheet worksheet1 = package.Workbook.Worksheets[2];
                            int rowCount1 = worksheet1.Dimension.Rows;
                            int colCount1 = worksheet1.Dimension.Columns;
                            if (rowCount1<2 || colCount1<11)
                            {
                                result.msg = "客户联系人信息格式不正确";
                                return Json(result);
                            }

                            for (int row=2;row<rowCount1;row++)
                            {
                                EnterCustContacts enterCustContacts = new EnterCustContacts();
                                for (int col=1;col<colCount1;col++)
                                {                                    string value = worksheet1.Cells[row, col].Value.TryToString().Trim();
                                    switch (col)
                                    {
                                        case 1:
                                            if (enteridDIc.ContainsKey(value))
                                            {
                                                enterCustContacts.EnterCustID = enteridDIc[value];
                                            }
                                            break;
                                        case 2:
                                            enterCustContacts.Name = value;
                                            break;
                                        case 3:  
                                            if (value == "男")
                                                enterCustContacts.Sex = Model.Enume.SexEnum.Man;
                                            else if (value == "女")
                                                enterCustContacts.Sex = Model.Enume.SexEnum.Woman;
                                            break;
                                        case 4:
                                            enterCustContacts.Business = value;
                                            break;
                                        case 5:
                                            enterCustContacts.Department = value;
                                            break;
                                        case 6:
                                            enterCustContacts.Duties = value;
                                            break;
                                        case 7:
                                            enterCustContacts.Landline = value;
                                            break;
                                        case 8:
                                            enterCustContacts.Telephone = value;
                                            break;
                                        case 9:
                                            enterCustContacts.Email = value;
                                            break;
                                        case 10:
                                            enterCustContacts.QQ = value;
                                            break;
                                        case 11:
                                            enterCustContacts.WeChart = value;
                                            break;
                                        case 12:
                                            enterCustContacts.Address = value;
                                            break;
                                        case 13:
                                            enterCustContacts.Rem = value;
                                            break;
                                    }
                                }
                                if (enterCustContacts.EnterCustID > 0)
                                {
                                    int id = await enterCustContactsBll.Add(enterCustContacts);
                                    if (id <= 0)
                                        stringBuilder.AppendLine(string.Format("企业联系人【{0}】信息保存失败", enterCustContacts.Name));
                                }
                            }
                            #endregion

                            if (stringBuilder.Length>0)
                            {
                                string errorfile = DateTime.Now.ToString("yyyyMMddhhmmssff") + UserID + ".txt";
                                string errorfilePath = Path.Combine(virtualPath, errorfile);
                                try
                                {
                                    FileStream filestream = new FileStream(errorfilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                                    StreamWriter writer = new StreamWriter(filestream, System.Text.Encoding.UTF8);
                                    writer.BaseStream.Seek(0, SeekOrigin.End);
                                    await writer.WriteAsync(stringBuilder.ToString());
                                    writer.Flush();
                                    writer.Close();
                                    filestream.Close();
                                    result.data = new PicMsg();
                                    result.data.src = "/file/" + errorfile;
                                    result.data.title = errorfile;
                                }
                                catch
                                {
                                }
                            }
                        }
                        result.code = 0;
                        result.msg = "OK";
                    }
                    catch (Exception exception)
                    {
                        result.msg = "上传失败:" + exception.Message;
                    }
                }
            }

            return Json(result);
        }
        /// <summary>
        /// 上传目标邮箱
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> uploadTagEmail()
        {
            JsonResult<PicMsg> result = new JsonResult<PicMsg>();
            result.code = 1;
            result.msg = "";
        
            // 获取附带POST参数值
            for(var fileId = 0; fileId < Request.Form.Files.Count; fileId++)
            {
                var curFile = Request.Form.Files[fileId];
                if (curFile.Length < 1) { continue; }
                var fileExt = Path.GetExtension(curFile.FileName);
                if (String.IsNullOrEmpty(fileExt) || fileExt != ".xlsx")
                {
                    result.msg = "上传文件中包含不支持文件格式!";
                    return Json(result);
                }
                else
                {
                    // 存储文件名
                    string FileName = DateTime.Now.ToString("yyyyMMddhhmmssff") + CreateRandomCode(8) + fileExt;

                    // 存储路径（绝对路径）
                    string virtualPath = Path.Combine(hostingEnv.WebRootPath, "file");
                    if (!Directory.Exists(virtualPath))
                    {
                        Directory.CreateDirectory(virtualPath);
                    }
                    string filepath = Path.Combine(virtualPath, FileName);
                    try
                    {
                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            await curFile.CopyToAsync(fs);
                            fs.Flush();
                        }
                        FileInfo file = new FileInfo(filepath);
                        using (ExcelPackage package = new ExcelPackage(file))
                        {
                            StringBuilder sb = new StringBuilder();
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                            int rowCount = worksheet.Dimension.Rows;
                            int ColCount = worksheet.Dimension.Columns;
                            if (rowCount < 2 || ColCount < 2)
                            {
                                result.msg = "Excel模板不正确";
                                return Json(result);
                            }
                            StringBuilder stringBuilder = new StringBuilder();
                            Dictionary<string, int> enteridDIc = new Dictionary<string, int>();
                            TargetEmailBll bll = new TargetEmailBll();
                            for (int row = 2; row <= rowCount; row++)
                            {
                                TargetEmail targetEmail = new TargetEmail();
                                for (int col = 1; col <= ColCount; col++)
                                {
                                    string value = worksheet.Cells[row, col].Value.TryToString().Trim();
                                    switch (col)
                                    {
                                        case 1:
                                            targetEmail.Name = value;
                                            break;
                                        case 2:
                                            targetEmail.Email = value;
                                            break;
                                    }
                                }
                                bool flag = await bll.Exists(targetEmail.Name);
                                if (flag)
                                {
                                    stringBuilder.AppendLine(string.Format("{0}已存在", targetEmail.Name));
                                    continue;
                                }
                                int id = await bll.AddAsync(targetEmail);
                                if (id <= 0)
                                {
                                    stringBuilder.AppendLine(string.Format("邮箱【{0}】|添加失败", targetEmail.Name));
                                }
                            }

                            if (stringBuilder.Length > 0)
                            {
                                string errorfile = DateTime.Now.ToString("yyyyMMddhhmmssff")+ "error.txt";
                                string errorfilePath = Path.Combine(virtualPath, errorfile);
                                try
                                {
                                    FileStream filestream = new FileStream(errorfilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                                    StreamWriter writer = new StreamWriter(filestream, System.Text.Encoding.UTF8);
                                    writer.BaseStream.Seek(0, SeekOrigin.End);
                                    await writer.WriteAsync(stringBuilder.ToString());
                                    writer.Flush();
                                    writer.Close();
                                    filestream.Close();
                                    result.data = new PicMsg();
                                    result.data.src = "/file/" + errorfile;
                                    result.data.title = errorfile;
                                }
                                catch(Exception ex)
                                {
                                    ClassLoger.Error("uploadTagEmail", ex);
                                }
                            }
                        }
                        result.code = 0;
                        result.msg = "OK";
                    }
                    catch (Exception exception)
                    {
                        result.msg = "上传失败:" + exception.Message;
                    }
                }
            }

            return Json(result);
        }

        class PicMsg
        {
            public string src { get; set; }
            public string title { get; set; }
        }
        /// <summary>
        /// 生成指定长度的随机码。
        /// </summary>
        private string CreateRandomCode(int length)
        {
            string[] codes = new string[36] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            StringBuilder randomCode = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                randomCode.Append(codes[rand.Next(codes.Length)]);
            }
            return randomCode.ToString();
        }
    }
}