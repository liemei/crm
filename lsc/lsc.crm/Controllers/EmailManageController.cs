using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bnuxq.Bll;
using bnuxq.crm.ViewModel;
using bnuxq.Common;
using bnuxq.Model;
using Microsoft.AspNetCore.Mvc;

namespace bnuxq.crm.Controllers
{
    public class EmailManageController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            EmailResourcesBll bll = new EmailResourcesBll();
            var list = await bll.GetList();
            ViewBag.root = "email";
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> DelAsync(int id)
        {
            EmailResourcesBll bll = new EmailResourcesBll();
            var info = await bll.GetById(id);
            await bll.DelAsync(info);
            return Json(new {code = 1, msg = "OK"});
        }

        public IActionResult Add()
        {
            ViewBag.root = "email";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveAsync()
        {
            EmailResourcesBll bll = new EmailResourcesBll();
            EmailResources emailResources = new EmailResources();
            emailResources.Email = Request.Form["Email"].TryToString();
            emailResources.Password = Request.Form["Password"].TryToString();
            emailResources.Port = Request.Form["Port"].TryToString();
            emailResources.SenderServerIp = Request.Form["SenderServerIp"].TryToString();
            emailResources.UserName = Request.Form["UserName"].TryToString();
            await bll.AddAsync(emailResources);
            return Json(new {code = 1, msg = "OK"});
        }
        /// <summary>
        /// 目标邮箱
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IActionResult> TargetEmailList(int pageIndex=1)
        {
            int pageSize = 20;
            TargetEmailBll targetEmailBll = new TargetEmailBll();
            var tup = await targetEmailBll.GetList(pageIndex, pageSize);
            ViewBag.count = tup.Item2;
            ViewBag.pageSize = pageSize;
            ViewBag.pageIndex = pageIndex;
            ViewBag.root = "email";
            return View(tup.Item1);
        }
        [HttpGet]
        public async Task<IActionResult> DelEmail(int id)
        {
            TargetEmailBll targetEmailBll = new TargetEmailBll();
            var info =await targetEmailBll.Get(id);
            await targetEmailBll.DelAsync(info);
            return Json(new {code = 1, msg = "OK"});
        }
        /// <summary>
        /// 邮件模板
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> EmailTemplateList()
        {
            EmailTemplateBll emailTemplateBll = new EmailTemplateBll();
            var list = await emailTemplateBll.GetList();
            ViewBag.root = "email";
            return View(list);
        }
        /// <summary>
        /// 删除邮件模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DelEmailTemp(int id)
        {
            EmailTemplateBll emailTemplateBll = new EmailTemplateBll();
            var info = await emailTemplateBll.GetById(id);
            if (info!=null)
            {
                await emailTemplateBll.DelAsync(info);
            }
            return Json(new {code = 1, msg = "OK"});
        }
        /// <summary>
        /// 添加邮件模板
        /// </summary>
        /// <returns></returns>
        public IActionResult AddEmailTemp()
        {
            ViewBag.root = "email";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveEmailTemp()
        {
            EmailTemplateBll emailTemplateBll = new EmailTemplateBll();
            EmailTemplate emailTemplate = new EmailTemplate();
            emailTemplate.CreateTime = DateTime.Now;
            emailTemplate.Title = Request.Form["Title"].TryToString();
            emailTemplate.EmailContent = Request.Form["EmailContent"].TryToString();
            var id = await emailTemplateBll.AddAsync(emailTemplate);
            return Json(new {code = 1, msg = "OK"});
        }
        /// <summary>
        /// 添加邮件发送任务
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateSendEmailTask(int emailtempId=0)
        {
            EmailTemplateBll emailTemplateBll = new EmailTemplateBll();
            var list = await emailTemplateBll.GetList();
            ViewBag.list = list;
            ViewBag.emailtempId = emailtempId;
            ViewBag.root = "email";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveSendEmailTask()
        {
            SendEmailTask sendEmailTask = new SendEmailTask();
            SendEmailLogBll sendEmailLogBll = new SendEmailLogBll();
            sendEmailTask.EmailTempId = Request.Form["EmailTempId"].TryToInt(0);
            sendEmailTask.TaskName = Request.Form["TaskName"].TryToString();
            sendEmailTask.CreateTime = DateTime.Now;
            string Email = Request.Form["Email"].TryToString();
            bool flag = Request.Form["sendAll"].TryToString() == "on";
            SendEmailTaskBll bll = new SendEmailTaskBll();
            int id = await bll.AddAsync(sendEmailTask);
            if (id>0)
            {
                Task.Run(async () =>
                {
                    if (!Email.IsNull())
                    {
                        SendEmailLog log = new SendEmailLog
                        {
                            SendEmailTaskId = id,
                            Email = Email,
                            IsRead = false,
                            IsSend = false,
                            IsSendOk = false,
                            Name = Email,
                            EmailTempId = sendEmailTask.EmailTempId,
                        };
                        await sendEmailLogBll.AddAsync(log);
                    }
                    if (flag)
                    {
                        TargetEmailBll targetEmailBll = new TargetEmailBll();
                        int pageIndex = 0;
                        int pageSize = 50;
                        A:
                        pageIndex++;
                        var tup = await targetEmailBll.GetList(pageIndex, pageSize);
                        if (tup.Item1 != null && tup.Item1.Count > 0)
                        {
                            foreach (TargetEmail targetEmail in tup.Item1)
                            {
                                SendEmailLog log1 = new SendEmailLog
                                {
                                    SendEmailTaskId = id,
                                    Email = targetEmail.Email,
                                    IsRead = false,
                                    IsSend = false,
                                    IsSendOk = false,
                                    Name = targetEmail.Name,
                                    EmailTempId = sendEmailTask.EmailTempId,
                                };
                                await sendEmailLogBll.AddAsync(log1);
                            }
                            goto A;
                        }
                    }
                    SendEmailHelper.StartSendEmail(id);
                });
            }
            return Json(new {code = 1, msg = "OK"});
        }

        /// <summary>
        /// 邮件发送日志
        /// </summary>
        /// <param name="sendEmailTaskId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IActionResult SendEmailLogList(int sendEmailTaskId,int pageIndex=1)
        {
            SendEmailLogBll sendEmailLogBll = new SendEmailLogBll();
            int pageSize = 35;
            var tup = sendEmailLogBll.GetByTaskId(sendEmailTaskId, pageIndex, pageSize);
            SendEmailTaskBll sendEmailTaskBll = new SendEmailTaskBll();
            var taskinfo = sendEmailTaskBll.GetById(sendEmailTaskId).Result;
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.count = tup.Item2;
            ViewBag.sendEmailTaskId = sendEmailTaskId;
            ViewBag.taskinfo = taskinfo;
            ViewBag.root = "email";
            return View(tup.Item1);
        }
        /// <summary>
        /// 邮件发送任务列表
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SendEmailTaskList()
        {
            SendEmailTaskBll sendEmailTaskBll = new SendEmailTaskBll();
            var list = await sendEmailTaskBll.GetList();
            ViewBag.root = "email";
            return View(list);
        }
        /// <summary>
        /// 手动发送邮件
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SendEmail(int logId)
        {
            SendEmailLogBll sendEmailLogBll = new SendEmailLogBll();
            var log = await sendEmailLogBll.GetById(logId);
            SendEmailHelper.SendEmail(log);
            return Json(new {code = 1, msg = "OK"});
        }

      

    }
}