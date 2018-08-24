using System;
using System.Collections.Generic;
using System.Text;

namespace bnuxq.Model.Enume
{
    public static class EnumeHelper
    {
        public static string TryToStr(this CustomerTypeEnum ct)
        {
            string str = string.Empty;
            switch (ct)
            {
                case CustomerTypeEnum.BigCustomer:
                    str = "集团大客户";
                    break;
                case CustomerTypeEnum.Cooperation:
                    str = "业务合作商";
                    break;
                case CustomerTypeEnum.Dealer:
                    str = "代理经销商";
                    break;
                case CustomerTypeEnum.Ordinary:
                    str = "普通客户";
                    break;
                case CustomerTypeEnum.Other:
                    str = "其他客户";
                    break;
                case CustomerTypeEnum.Same:
                    str = "怀疑同行";
                    break;
                case CustomerTypeEnum.ArmedPolice:
                    str = "武警部队";
                    break;
                case CustomerTypeEnum.Colleges:
                    str = "高校";
                    break;
                case CustomerTypeEnum.Commission:
                    str = "教委";
                    break;
                case CustomerTypeEnum.Hospital:
                    str = "医院";
                    break;
                case CustomerTypeEnum.JDS:
                    str = "戒毒所";
                    break;
                case CustomerTypeEnum.Judicial:
                    str = "公检法";
                    break;
                case CustomerTypeEnum.MiddleSchool:
                    str = "中学";
                    break;
                case CustomerTypeEnum.PrimarySchool:
                    str = "小学";
                    break;
                case CustomerTypeEnum.Prison:
                    str = "监狱";
                    break;
                case CustomerTypeEnum.Special:
                    str = "特教";
                    break;
                case CustomerTypeEnum.VocationalSchools:
                    str = "中职";
                    break;
            }
            return str;
        }

        public static string TryToStr(this CustSource custSource)
        {
            string str = string.Empty;
            switch (custSource)
            {
                case CustSource.CustTelephone:
                    str = "客户来电";
                    break;
                case CustSource.Excavate:
                    str = "主动挖掘";
                    break;
                case CustSource.Introduction:
                    str = "客户介绍";
                    break;
                case CustSource.Other:
                    str = "其他来源";
                    break;
                case CustSource.WebConsulting:
                    str = "网站咨询";
                    break;
            }
            return str;
        }

        public static string TryToStr(this DegreeOfHeatEnume degreeOfHeat)
        {
            string str = string.Empty;
            switch (degreeOfHeat)
            {
                case DegreeOfHeatEnume.Intermediate:
                    str = "中";
                    break;
                case DegreeOfHeatEnume.Lower:
                    str = "低";
                    break;
                case DegreeOfHeatEnume.Senior:
                    str = "高";
                    break;
            }
            return str;
        }

        public static string TryToStr(this HeatTypeEnum heatType)
        {
            string str = string.Empty;
            switch (heatType)
            {
                case HeatTypeEnum.Hopeful:
                    str = "有望签单客户";
                    break;
                case HeatTypeEnum.Intentional:
                    str = "有意向客户";
                    break;
                case HeatTypeEnum.Key_Account:
                    str = "重点客户";
                    break;
            }
            return str;
        }

        public static string TryToStr(this PhaseEnume phase)
        {
            string str = string.Empty;
            switch (phase)
            {
                case PhaseEnume.After_Sale:
                    str = "售后";
                    break;
                case PhaseEnume.Demand_Confirmation:
                    str = "需求确认";
                    break;
                case PhaseEnume.Invalid:
                    str = "订单失效";
                    break;
                case PhaseEnume.In_Sales:
                    str = "售中跟单";
                    break;
                case PhaseEnume.Other:
                    str = "其他";
                    break;
                case PhaseEnume.Pre_sale:
                    str = "售前跟踪";
                    break;
                case PhaseEnume.Shelve:
                    str = "搁置";
                    break;
                case PhaseEnume.Sign_Contract:
                    str = "签约洽谈";
                    break;
            }
            return str;
        }

        public static string TryToStr(this RelationshipEnume relationship)
        {
            string str = string.Empty;
            switch (relationship)
            {
                case RelationshipEnume.Better:
                    str = "较好";
                    break;
                case RelationshipEnume.Commonly:
                    str = "一般";
                    break;
                case RelationshipEnume.Intimate:
                    str = "密切";
                    break;
                case RelationshipEnume.Poor:
                    str = "较差";
                    break;
            }
            return str;
        }

        public static string TryToStr(this SexEnum sex)
        {
            string str = "未知";
            switch (sex)
            {
                case SexEnum.Man:
                    str = "男";
                    break;
                case SexEnum.Woman:
                    str = "女";
                    break;
            }
            return str;
        }

        public static string TryToStr(this ValueGradeEnume valueGrade)
        {
            string str = string.Empty;
            switch (valueGrade)
            {
                case ValueGradeEnume.Intermediate:
                    str = "中";
                    break;
                case ValueGradeEnume.Lower:
                    str = "低";
                    break;
                case ValueGradeEnume.Senior:
                    str = "高";
                    break;
            }
            return str;
        }

        public static string TryToStr(this ProjectStateEnum projectState)
        {
            string str = string.Empty;
            switch (projectState)
            {
                case ProjectStateEnum.Abandon:
                    str = "放弃";
                    break;
                case ProjectStateEnum.Failed:
                    str = "失败";
                    break;
                case ProjectStateEnum.Ongoing:
                    str = "进行中";
                    break;
                case ProjectStateEnum.Shelve:
                    str = "搁置";
                    break;
                case ProjectStateEnum.Success:
                    str = "成功";
                    break;
            }
            return str;
        }

        public static string TryToStr(this ProjectTypeEnum projectType)
        {
            string str = string.Empty;
            switch (projectType)
            {
                case ProjectTypeEnum.Big:
                    str = "大项目";
                    break;
                case ProjectTypeEnum.Secondary:
                    str = "中项目";
                    break;
                case ProjectTypeEnum.Small:
                    str = "小项目";
                    break;
            }
            return str;
        }

        public static string TryToStr(this WorkPlanStateEnum workPlanState)
        {
            string state = string.Empty;
            switch (workPlanState)
            {
                case WorkPlanStateEnum.Finish:
                    state = "已完成";
                    break;
                case WorkPlanStateEnum.NoFinish:
                    state = "未完成";
                    break;
            }
            return state;
        }


    }
}
