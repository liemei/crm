using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bnuxq.Bll;
using bnuxq.Common;
using bnuxq.Model;

namespace bnuxq.crm.ViewModel
{
    public class SendEmailHelper
    {
        /// <summary>
        /// 开始发送
        /// </summary>
        public static void StartSendEmail(int sendEmailTaskId)
        {
            if (sendEmailTaskId<=0)
            {
                ClassLoger.Fail("SendEmailHelper.StartSendEmail", "邮件发送任务Id小于0不能发送任务");
                return;
            }
            Task.Run(() =>
            {
                ClassLoger.Info("SendEmailHelper.StartSendEmail", "开始发送邮件");
                SendEmailLogBll sendEmailLogBll = new SendEmailLogBll();
                EmailTemplateBll emailTemplateBll = new EmailTemplateBll();

                try
                {
                    EmailResourcesBll emailResourcesBll = new EmailResourcesBll();
                    List<EmailResources> emailResourceses = emailResourcesBll.GetLists();
                    if (emailResourceses == null || emailResourceses.Count == 0)
                    {
                        ClassLoger.Fail("SendEmailHelper.StartSendEmail", "邮件资源为空不能发邮件");
                    }
                    int pageIndex = 0;
                    int pageSize = 60;
                    A:
                    pageIndex++;
                    var tup = sendEmailLogBll.GetNoSendByTaskId(sendEmailTaskId, pageIndex, pageSize);
                    if (tup.Item1 != null && tup.Item1.Count > 0)
                    {
                        foreach (var sendEmailLog in tup.Item1)
                        {
                            Random r = new Random();
                            int i = r.Next(0, emailResourceses.Count - 1);
                            var emailResourcese = emailResourceses[i];
                            var template = emailTemplateBll.GetByIds(sendEmailLog.EmailTempId);
                            string url =
                                $"http://open.bnuxq.com:8080/Account/OpenEmailCallBack?logid=" + sendEmailLog.Id;
                            string imag =
                                $"<img src=\"{url}\" style=\"width: 1px; height: 1px;\" />";

                            string emailCount = template.EmailContent + "<br/>" + imag;//邮箱内容

                            EmailHelper emailHelper = new EmailHelper(emailResourcese.SenderServerIp,
                                sendEmailLog.Email, emailResourcese.Email, template.Title,
                                emailCount, emailResourcese.UserName, emailResourcese.Password,
                                emailResourcese.Port, false, false);
                            try
                            {
                                emailHelper.Send();
                                sendEmailLog.IsSendOk = true;
                            }
                            catch (Exception e)
                            {
                                sendEmailLog.IsSendOk = false;
                            }
                            sendEmailLog.IsSend = true;
                            sendEmailLogBll.Update(sendEmailLog);
                            Thread.Sleep(1000 * 60 * 3);
                        }
                        goto A;
                    }
                }
                catch (Exception e)
                {
                    ClassLoger.Error("SendEmailHelper.SendEmail", e);
                }
            });
        }

        public static void SendEmail(SendEmailLog log)
        {
            try
            {
                EmailResourcesBll emailResourcesBll = new EmailResourcesBll();
                List<EmailResources> emailResourceses = emailResourcesBll.GetLists();
                EmailTemplateBll emailTemplateBll = new EmailTemplateBll();
                SendEmailLogBll sendEmailLogBll = new SendEmailLogBll();
                if (emailResourceses == null || emailResourceses.Count == 0)
                {
                    return;
                }
                Random r = new Random();
                int i = r.Next(0, emailResourceses.Count - 1);
                var emailResourcese = emailResourceses[i];
                var template = emailTemplateBll.GetByIds(log.EmailTempId);
                string url =
                    $"http://open.bnuxq.com:8080/Account/OpenEmailCallBack?logid=" + log.Id;
                string imag =
                    $"<img src=\"{url}\" style=\"width: 1px; height: 1px;\" />";

                string emailCount = template.EmailContent + "<br/>" + imag;//邮箱内容

                EmailHelper emailHelper = new EmailHelper(emailResourcese.SenderServerIp,
                    log.Email, emailResourcese.Email, template.Title,
                    emailCount, emailResourcese.UserName, emailResourcese.Password,
                    emailResourcese.Port, false, false);
                try
                {
                    emailHelper.Send();
                    log.IsSendOk = true;
                }
                catch (Exception e)
                {
                    log.IsSendOk = false;
                }
                log.IsSend = true;
                sendEmailLogBll.Update(log);
            }
            catch (Exception e)
            {
                
            }
        }
    }
}

