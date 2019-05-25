#pragma checksum "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2c895a7e1391c47240fc071e5a569337ed294396"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EnterCustom_SalesProjectIndex), @"mvc.1.0.view", @"/Views/EnterCustom/SalesProjectIndex.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/EnterCustom/SalesProjectIndex.cshtml", typeof(AspNetCore.Views_EnterCustom_SalesProjectIndex))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
using lsc.Model;

#line default
#line hidden
#line 2 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
using lsc.Model.Enume;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2c895a7e1391c47240fc071e5a569337ed294396", @"/Views/EnterCustom/SalesProjectIndex.cshtml")]
    public class Views_EnterCustom_SalesProjectIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SalesProject>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
  
    ViewData["Title"] = "销售项目管理";
    Layout = "~/Pages/_Layout.cshtml";
    List<EnterCustomer> enterlist = ViewBag.enterlist;
    Dictionary<int, double> recpayDic = ViewBag.recpayDic;

#line default
#line hidden
            BeginContext(267, 699, true);
            WriteLiteral(@"<blockquote class=""layui-elem-quote"">
    销售项目管理
    <a class=""layui-btn layui-btn-normal"" href=""/EnterCustom/AddEnterCustom?id=0"">添加客户</a>
</blockquote>
<fieldset class=""layui-elem-field layui-field-title"" style=""margin-top: 20px;"">
    <legend>销售项目综合查询</legend>
</fieldset>
<div class=""layui-fluid"">
    <div class=""layui-row"">
        <form class=""layui-form"" method=""post"" id=""queryform"" action=""/EnterCustom/SalesProjectIndex"">
            <div class=""layui-col-md4 query-from-item"">
                <label class=""layui-form-label"">项目标题</label>
                <div class=""layui-input-inline"">
                    <input type=""text"" name=""Title"" class=""layui-form-text layui-input""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 966, "\"", 988, 1);
#line 23 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
WriteAttributeValue("", 974, ViewBag.Title, 974, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(989, 389, true);
            WriteLiteral(@" />
                </div>
            </div>
            <div class=""layui-col-md4 query-from-item"">
                <label class=""layui-form-label"">项目状态</label>
                <div class=""layui-input-inline"">
                    <select class=""layui-form-select"" name=""ProjectState"">
                        <option value=""""></option>
                        <option value=""0""  ");
            EndContext();
            BeginContext(1380, 39, false);
#line 31 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                       Write(ViewBag.ProjectState==0 ? "selected":"");

#line default
#line hidden
            EndContext();
            BeginContext(1420, 58, true);
            WriteLiteral(">进行中</option>\r\n                        <option value=\"1\"  ");
            EndContext();
            BeginContext(1480, 39, false);
#line 32 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                       Write(ViewBag.ProjectState==1 ? "selected":"");

#line default
#line hidden
            EndContext();
            BeginContext(1520, 57, true);
            WriteLiteral(">成功</option>\r\n                        <option value=\"2\"  ");
            EndContext();
            BeginContext(1579, 39, false);
#line 33 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                       Write(ViewBag.ProjectState==2 ? "selected":"");

#line default
#line hidden
            EndContext();
            BeginContext(1619, 57, true);
            WriteLiteral(">失败</option>\r\n                        <option value=\"3\"  ");
            EndContext();
            BeginContext(1678, 39, false);
#line 34 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                       Write(ViewBag.ProjectState==3 ? "selected":"");

#line default
#line hidden
            EndContext();
            BeginContext(1718, 57, true);
            WriteLiteral(">搁置</option>\r\n                        <option value=\"4\"  ");
            EndContext();
            BeginContext(1777, 39, false);
#line 35 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                       Write(ViewBag.ProjectState==4 ? "selected":"");

#line default
#line hidden
            EndContext();
            BeginContext(1817, 427, true);
            WriteLiteral(@">放弃</option>
                    </select>
                </div>
            </div>
            <div class=""layui-col-md4 query-from-item"">
                <label class=""layui-form-label"">项目类型</label>
                <div class=""layui-input-inline"">
                    <select class=""layui-form-select"" name=""ProjectType"">
                        <option value=""""></option>
                        <option value=""1"" ");
            EndContext();
            BeginContext(2246, 40, false);
#line 44 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                      Write(ViewBag.ProjectType == 1 ? "selected":"");

#line default
#line hidden
            EndContext();
            BeginContext(2287, 58, true);
            WriteLiteral(">大型项目</option>\r\n                        <option value=\"2\" ");
            EndContext();
            BeginContext(2347, 40, false);
#line 45 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                      Write(ViewBag.ProjectType == 2 ? "selected":"");

#line default
#line hidden
            EndContext();
            BeginContext(2388, 58, true);
            WriteLiteral(">中型项目</option>\r\n                        <option value=\"3\" ");
            EndContext();
            BeginContext(2448, 40, false);
#line 46 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                      Write(ViewBag.ProjectType == 3 ? "selected":"");

#line default
#line hidden
            EndContext();
            BeginContext(2489, 384, true);
            WriteLiteral(@">小型项目</option>
                    </select>
                </div>
            </div>
            <div class=""layui-col-md4 query-from-item"">
                <label class=""layui-form-label"">立案开始时间</label>
                <div class=""layui-input-inline"">
                    <input type=""text"" class=""layui-input"" id=""starttime"" placeholder=""yyyy-MM-dd"" name=""ProjectStartTime""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 2873, "\"", 2908, 1);
#line 53 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
WriteAttributeValue("", 2881, ViewBag.ProjectStartTime, 2881, 27, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2909, 338, true);
            WriteLiteral(@" />
                </div>
            </div>
            <div class=""layui-col-md4 query-from-item"">
                <label class=""layui-form-label"">立案截止时间</label>
                <div class=""layui-input-inline"">
                    <input type=""text"" class=""layui-input"" id=""endtime"" placeholder=""yyyy-MM-dd"" name=""ProjectEndTime""");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 3247, "\"", 3280, 1);
#line 59 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
WriteAttributeValue("", 3255, ViewBag.ProjectEndTime, 3255, 25, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3281, 207, true);
            WriteLiteral(" />\r\n                </div>\r\n            </div>\r\n            <div class=\"layui-col-md4 query-from-item\">\r\n                <div class=\"layui-input-block\">\r\n                    <input type=\"hidden\" name=\"page\"");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 3488, "\"", 3516, 1);
#line 64 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
WriteAttributeValue("", 3496, ViewBag.pageIndex, 3496, 20, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3517, 648, true);
            WriteLiteral(@" />
                    <button class=""layui-btn"" lay-submit lay-filter=""*"">查询</button>
                </div>
            </div>
        </form>
    </div>
    <div class=""layui-row"">
        <table class=""layui-table"">
            <thead>
                <tr>
                    <th>项目标题</th>
                    <th>项目概要</th>
                    <th>客户</th>
                    <th>项目状态</th>
                    <th>项目类型</th>
                    <th>立项时间</th>
                    <th>项目金额</th>
                    <th>已回款</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 86 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                 if (Model != null && Model.Count > 0)
                {
                    foreach (var info in Model)
                    {
                        string entername = string.Empty;
                        if (enterlist!=null)
                        {
                            var enter = enterlist.FirstOrDefault(x => x.ID == info.EnterCustomerID);
                            if (enter!=null)
                            {
                                entername = enter.EnterName;
                            }
                        }
                        double amt = 0;
                        if (recpayDic!=null && recpayDic.ContainsKey(info.ID))
                        {
                            amt = recpayDic[info.ID];
                         }

#line default
#line hidden
            BeginContext(4973, 62, true);
            WriteLiteral("                        <tr>\r\n                            <td>");
            EndContext();
            BeginContext(5036, 10, false);
#line 105 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                           Write(info.Title);

#line default
#line hidden
            EndContext();
            BeginContext(5046, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(5086, 20, false);
#line 106 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                           Write(info.ProjectAbstract);

#line default
#line hidden
            EndContext();
            BeginContext(5106, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(5146, 9, false);
#line 107 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                           Write(entername);

#line default
#line hidden
            EndContext();
            BeginContext(5155, 41, true);
            WriteLiteral("</td>\r\n                            <td><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 5196, "\"", 5265, 2);
            WriteAttributeValue("", 5203, "/EnterCustom/GetSalesProjectStateLog?SalesProjectID=", 5203, 52, true);
#line 108 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
WriteAttributeValue("", 5255, info.ID, 5255, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(5266, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(5268, 28, false);
#line 108 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                                                                                                    Write(info.ProjectState.TryToStr());

#line default
#line hidden
            EndContext();
            BeginContext(5296, 43, true);
            WriteLiteral("</a></td>\r\n                            <td>");
            EndContext();
            BeginContext(5340, 27, false);
#line 109 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                           Write(info.ProjectType.TryToStr());

#line default
#line hidden
            EndContext();
            BeginContext(5367, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(5407, 39, false);
#line 110 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                           Write(info.ProjectTime.ToString("yyyy-MM-dd"));

#line default
#line hidden
            EndContext();
            BeginContext(5446, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(5486, 15, false);
#line 111 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                           Write(info.ProjectAmt);

#line default
#line hidden
            EndContext();
            BeginContext(5501, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(5541, 3, false);
#line 112 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                           Write(amt);

#line default
#line hidden
            EndContext();
            BeginContext(5544, 162, true);
            WriteLiteral("</td>\r\n                            <td>\r\n                                <div class=\"layui-btn-group\">\r\n                                    <a href=\"javascript:;\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 5706, "\"", 5739, 3);
            WriteAttributeValue("", 5716, "updatestate(\'", 5716, 13, true);
#line 115 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
WriteAttributeValue("", 5729, info.ID, 5729, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 5737, "\')", 5737, 2, true);
            EndWriteAttribute();
            BeginContext(5740, 139, true);
            WriteLiteral(" class=\"layui-btn layui-btn-small\">修改项目状态</a>\r\n                                    <a href=\"javascript:;\" class=\"layui-btn layui-btn-small\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 5879, "\"", 5916, 3);
            WriteAttributeValue("", 5889, "addReceoverdPay(\'", 5889, 17, true);
#line 116 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
WriteAttributeValue("", 5906, info.ID, 5906, 8, false);

#line default
#line hidden
            WriteAttributeValue("", 5914, "\')", 5914, 2, true);
            EndWriteAttribute();
            BeginContext(5917, 149, true);
            WriteLiteral(">添加回款记录</a>\r\n                                </div>\r\n                            \r\n                            </td>\r\n                        </tr>\r\n");
            EndContext();
#line 121 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                    }
                }

#line default
#line hidden
            BeginContext(6108, 91, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n        <div id=\"page\"></div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(6222, 870, true);
                WriteLiteral(@"
    <script type=""text/javascript"">
        var layer, form
        layui.use(['form', 'element', 'layer', 'laydate','laypage'], function () {
            layer = layui.layer
            form = layui.form
            laydate = layui.laydate
            var laypage = layui.laypage

            laydate.render({
                elem: '#starttime'
            });

            laydate.render({
                elem: '#endtime'
            });

            //form.on('submit(*)', function (data) {
            //    $.post('/EnterCustom/SalesProjectList', data.field, function (res) {
            //        $(""#content_table"").html(res)
            //    })
            //    return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
            //});

            //分页
            laypage.render({
                elem: 'page' //分页容器的id
                , count: ");
                EndContext();
                BeginContext(7094, 13, false);
#line 156 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                     Write(ViewBag.Count);

#line default
#line hidden
                EndContext();
                BeginContext(7108, 141, true);
                WriteLiteral(" //总页数\r\n                ,limit:20\r\n                , skin: \'#1E9FFF\' //自定义选中色值\r\n                //,skip: true //开启跳页\r\n                ,curr: ");
                EndContext();
                BeginContext(7251, 17, false);
#line 160 "D:\gitCode\crm\lsc\lsc.crm\Views\EnterCustom\SalesProjectIndex.cshtml"
                   Write(ViewBag.pageIndex);

#line default
#line hidden
                EndContext();
                BeginContext(7269, 1072, true);
                WriteLiteral(@" //获取起始页
                ,jump: function (obj, first) {
                    console.log(obj)
                    if (!first) {
                        $(""input[name='page']"").val(obj.curr)
                        $(""#queryform"").submit();
                    }
                }
                , hash: 'fenye' //自定义hash值
            });
        });

        updatestate = function (id) {
            layer.open({
                type: 2,
                title: '更新销售项目状态',
                shadeClose: true,
                shade: 0.8,
                area: ['1200px', '70%'],
                content: '/EnterCustom/AddSalesProjectStateLog?SalesProjectID=' + id
            });
        }
        addReceoverdPay = function (id) {
            layer.open({
                type: 2,
                title: '添加回款记录',
                shadeClose: true,
                shade: 0.8,
                area: ['400px', '40%'],
                content: '/EnterCustom/AddReceovedPayLog?SalesProjectID=' + id
");
                WriteLiteral("            });\r\n         }\r\n    </script>\r\n    ");
                EndContext();
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SalesProject>> Html { get; private set; }
    }
}
#pragma warning restore 1591