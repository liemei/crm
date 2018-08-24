using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bnuxq.Bll;
using bnuxq.Common;
using bnuxq.Model;
using bnuxq.Model.Enume;
using Microsoft.AspNetCore.Mvc;

namespace bnuxq.crm.Controllers
{
    public class QuesController : BaseController
    {
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            int pageSize = 35;
            QuestionsBll bll = new QuestionsBll();
            var result = await bll.GetList(pageIndex, pageSize);
            ViewBag.total = result.Item2;
            List<Questions> list = result.Item1;
            ViewBag.pageIndex = pageIndex;
            return View(list);
        }

        public async Task<IActionResult> OptionList(int questionId)
        {
            OptionBll bll = new OptionBll();
            var list = await bll.GetList(questionId);
            return View(list);
        }

        public IActionResult AddQuestion()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SaveQuestion()
        {
            string content = Request.Form["Content"].TryToString();
            int QType = Request.Form["QType"].TryToInt(1);
            Questions questions = new Questions();
            questions.QuestionsType = (QuestionsTypeEnum)QType;
            questions.Content = content;
            QuestionsBll bll = new QuestionsBll();
            int id = await bll.AddAsync(questions);
            if (id > 0)
            {
                return Json(new { code = 1, msg = "OK", id = id });
            }

            return Json(new { code = 0, msg = "保存失败" });
        }

        [HttpGet]
        public async Task<IActionResult> DelQuestion(int id)
        {
            QuestionsBll bll = new QuestionsBll();
            var info = await bll.GetByIdAsync(id);
            if (info!=null)
            {
                bool flag = await bll.DelAsync(info);
                OptionBll obll = new OptionBll();
                var list = await obll.GetList(id);
                if (list!=null)
                {
                    foreach (Option option in list)
                    {
                        await obll.DelAsync(option);
                    }
                }

                if (flag)
                {
                    return Json(new {code = 1, msg = "OK"});
                }
            }
            return Json(new {code = 0, msg = "删除失败"});
        }

        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public IActionResult AddOption(int questionId)
        {
            ViewBag.questionId = questionId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveOption()
        {
            Option option = new Option()
            {
                QuestionsId = Request.Form["QuestionsId"].TryToInt(0),
                Content = Request.Form["Content"].TryToString(),
                ItemIndex = Request.Form["ItemIndex"].TryToString(),
                IsOk = Request.Form["IsOk"].TryToString() == "on"
            };
            OptionBll bll = new OptionBll();
            var id = await bll.Addasync(option);
            return id > 0 ? Json(new { code = 1, msg = "OK" }) : Json(new { code = 0, msg = "保存失败" });
        }

        [HttpGet]
        public async Task<IActionResult> DelOption(int id)
        {
            OptionBll bll = new OptionBll();
            var info = await bll.GetByIdAsync(id);
            if (info!=null)
            {
                bool flag = await bll.DelAsync(info);
                if (flag)
                {
                    return Json(new {code = 1, msg = "OK"});
                }
            }
            return Json(new {code = 0, msg = "删除失败"});
        }

        /// <summary>
        /// 用户答题记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserAnswerLogList(int pageIndex=1)
        {
            UserAnswerLogBll bll = new UserAnswerLogBll();
            int pageSize = 35;
            var result = await bll.GetList(User.ID, pageIndex, pageSize);
            bool flag = false;
            QuestionsBll qbll = new QuestionsBll();
            var list1 = await qbll.GetList(QuestionsTypeEnum.ChoiceOne, 90);
            var list2 = await qbll.GetList(QuestionsTypeEnum.ChoiceMore, 10);
            var list3 = await qbll.GetList(QuestionsTypeEnum.FillInTheBlanks, 5);
            if (list1!=null && list1.Count==90 && list2!=null && list2.Count==10 && list3!=null && list3.Count==5)
            {
                flag = true;
            }

            ViewBag.flag = flag;
            ViewBag.total = result.Item2;
            ViewBag.pageIndex = pageIndex;
            return View(result.Item1);
        }

        /// <summary>
        /// 用户答题详情
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserAnswerLogInfo(int logId)
        {
            UserQuestionsBll bll = new UserQuestionsBll();
            var userQuestionses = await bll.GetList(logId);
            UserAnswerBll userAnswerBll = new UserAnswerBll();
            var userAnswers = await userAnswerBll.GetList(logId);
            List<Questions> questionses = new List<Questions>();
            List<Option> options = new List<Option>();
            OptionBll optionBll = new OptionBll();
            QuestionsBll questionsBll = new QuestionsBll();
            if (userQuestionses!=null)
            {
                foreach (UserQuestions userQuestionse in userQuestionses)
                {
                    var info = await questionsBll.GetByIdAsync(userQuestionse.QuestionsId);
                    if (info!=null)
                    {
                        questionses.Add(info);
                    }

                    var optionlist = await optionBll.GetList(userQuestionse.QuestionsId);
                    if(optionlist!=null)
                        options.AddRange(optionlist);
                }
            }

            userQuestionses = userQuestionses.OrderBy(x => x.QIndex).ToList();

            ViewBag.questionses = questionses;
            ViewBag.options = options;
            ViewBag.userAnswers = userAnswers;

            ViewBag.isadmin = User.RoleID == 1;
            return View(userQuestionses);
        }

        /// <summary>
        /// 开始答题
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> StartAnswer()
        {
            QuestionsBll bll = new QuestionsBll();
            var list1 = await bll.GetList(QuestionsTypeEnum.ChoiceOne, 90);
            var list2 = await bll.GetList(QuestionsTypeEnum.ChoiceMore, 10);
            var list3 = await bll.GetList(QuestionsTypeEnum.FillInTheBlanks, 5);
            List<Questions> list = new List<Questions>();
            if (list1!=null)
            {
                list.AddRange(list1);
            }

            if (list2!=null)
            {
                list.AddRange(list2);
            }

            list = list.OrderBy(x => x.Id).ToList();
            UserAnswerLog log = new UserAnswerLog();
            UserAnswerLogBll logBll = new UserAnswerLogBll();
            log.CreateTime = DateTime.Now;
            log.UserId = User.ID;
            int logid = await logBll.AddAsync(log);
            int index = 1;
            UserQuestionsBll uqbll = new UserQuestionsBll();
            OptionBll obll = new OptionBll();
            List<Option> optionList = new List<Option>();
            List<UserQuestions> uoList = new List<UserQuestions>();

            foreach (Questions questionse in list)
            {
                UserQuestions userQuestions = new UserQuestions();
                userQuestions.LogId = logid;
                userQuestions.QIndex = index;
                userQuestions.QuestionsId = questionse.Id;
                userQuestions.Id =  await uqbll.AddAsync(userQuestions);
                index++;
                var olist = await obll.GetList(questionse.Id);
                if (olist!=null)
                {
                    optionList.AddRange(olist);
                }
                uoList.Add(userQuestions);
            }

            if (list3!=null)
            {
                foreach (Questions questionse in list3)
                {
                    UserQuestions userQuestions = new UserQuestions();
                    userQuestions.LogId = logid;
                    userQuestions.QIndex = index;
                    userQuestions.QuestionsId = questionse.Id;
                    userQuestions.Id = await uqbll.AddAsync(userQuestions);
                    index++;
                    uoList.Add(userQuestions);
                }
            }

            ViewBag.optionList = optionList;
            list.AddRange(list3);
            ViewBag.list = list;
            return View(uoList);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserAnswer()
        {
            UserAnswer userAnswer = new UserAnswer()
            {
                LogId = Request.Form["LogId"].TryToInt(0),
                QuestionId = Request.Form["QuestionId"].TryToInt(0),
            };
            UserAnswerBll bll = new UserAnswerBll();
            string qtype = Request.Form["qtype"].TryToString();
            string OptionId = Request.Form["OptionIds"].TryToString();
            string Content = Request.Form["Content"].TryToString();
            int isend = Request.Form["isend"].TryToInt(0);

            OptionBll optionBll = new OptionBll();
            var options = await optionBll.GetList(userAnswer.QuestionId);
            int uid = 0;
            switch (qtype)
            {
                case "ChoiceMore":
                    double oklength = 1;
                    var oklist = options.Where(x => x.IsOk);
                    if (oklist != null && oklist.Count() > 0)
                        oklength = oklist.Count();
                    double scoreRat = 1 / oklength;
                    var ids = OptionId.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (ids!=null && ids.Length>0)
                    {
                        foreach (string id in ids)
                        {
                            userAnswer.OptionId = id.TryToInt();
                            userAnswer.Score = 0;
                            if (options!=null)
                            {
                                var option = options.FirstOrDefault(x => x.Id == userAnswer.OptionId);
                                if (option != null)
                                {
                                    userAnswer.IsOk = option.IsOk;
                                    if (option.IsOk)
                                    {
                                        userAnswer.Score = scoreRat;
                                    }
                                }
                            }

                            userAnswer.Id = 0;
                            uid = await bll.AddAsync(userAnswer);
                        }
                    }
                    break;
                case "ChoiceOne":
                    userAnswer.OptionId = OptionId.TryToInt();
                    userAnswer.Score = 0;
                    var opti = options?.FirstOrDefault(x => x.Id == userAnswer.OptionId);
                    if (opti != null)
                    {
                        userAnswer.IsOk = opti.IsOk;
                        if (opti.IsOk)
                        {
                            userAnswer.Score = 1;
                        }
                    }
                    uid = await bll.AddAsync(userAnswer);
                    break;
                case "FillInTheBlanks":
                    userAnswer.Content = Content;
                    uid = await bll.AddAsync(userAnswer);
                    break;
            }

            if (isend==1)
            {
                var ualist = await bll.GetList(userAnswer.LogId);
                var scores = ualist.Select(x => x.Score);
                UserAnswerLogBll ulbll = new UserAnswerLogBll();
                var log = await ulbll.GetById(userAnswer.LogId);
                if (log != null)
                {
                    log.TotalScore = scores.Sum();
                    log.Duration = (DateTime.Now - log.CreateTime).TotalMinutes;
                    await ulbll.UpdateAsync(log);
                }
            }
            return uid > 0 ? Json(new {code = 1, msg = "Ok"}) : Json(new {code = 0, msg = "保存失败"});
        }
        /// <summary>
        /// 设置分数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetAnswerScore()
        {
            int id = Request.Form["id"].TryToInt(0);
            double score = Request.Form["score"].TryToDouble();
            UserAnswerBll bll = new UserAnswerBll();
            var userAnswer = await bll.GetByIdAsync(id);
            if (userAnswer!=null)
            {
                userAnswer.Score = score;
                bool flag = await bll.UpdateAsync(userAnswer);
                UserAnswerLogBll userAnswerLogBll = new UserAnswerLogBll();
                var log = await userAnswerLogBll.GetById(userAnswer.LogId);
                if (log!=null)
                {
                    log.TotalScore = log.TotalScore + score;
                    await userAnswerLogBll.UpdateAsync(log);
                }

                if (flag)
                {
                    return Json(new {code = 1, msg = "OK"});
                }
            }

            return Json(new {code = 0, msg = "更新失败"});
        }

        /// <summary>
        /// 所有人答题记录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UserAnswerLogs(int userId = 0,int pageIndex = 1)
        {
            UserAnswerLogBll bll = new UserAnswerLogBll();
            int pageSize = 35;
            var result = await bll.GetList(userId, pageIndex, pageSize);

            UserBll ubll = new UserBll();
            ViewBag.userList = await ubll.GetListAsync();
            ViewBag.total = result.Item2;
            ViewBag.userId = userId;
            ViewBag.pageIndex = pageIndex;
            return View(result.Item1);
        }
    }
}