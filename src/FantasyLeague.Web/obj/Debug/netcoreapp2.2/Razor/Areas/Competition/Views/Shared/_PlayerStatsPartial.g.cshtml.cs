#pragma checksum "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "81e409cb5d33693373c58a47ab56e11b107a2161"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Competition_Views_Shared__PlayerStatsPartial), @"mvc.1.0.view", @"/Areas/Competition/Views/Shared/_PlayerStatsPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Competition/Views/Shared/_PlayerStatsPartial.cshtml", typeof(AspNetCore.Areas_Competition_Views_Shared__PlayerStatsPartial))]
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
#line 1 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.Web.Areas.Identity;

#line default
#line hidden
#line 3 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.Models;

#line default
#line hidden
#line 4 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.Models.Enums;

#line default
#line hidden
#line 5 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.Web.Views.Shared;

#line default
#line hidden
#line 6 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.Common.Constants;

#line default
#line hidden
#line 7 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.Common.Pagination;

#line default
#line hidden
#line 8 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.ViewModels.Team;

#line default
#line hidden
#line 9 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.ViewModels.Player;

#line default
#line hidden
#line 10 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.ViewModels.Matchday;

#line default
#line hidden
#line 11 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using FantasyLeague.ViewModels.Score;

#line default
#line hidden
#line 12 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\_ViewImports.cshtml"
using System.Globalization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"81e409cb5d33693373c58a47ab56e11b107a2161", @"/Areas/Competition/Views/Shared/_PlayerStatsPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"993710b4cd2d4acba9eae3a87431601d1ac927b4", @"/Areas/Competition/Views/_ViewImports.cshtml")]
    public class Areas_Competition_Views_Shared__PlayerStatsPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<PlayerStatsViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"

    bool isAdmin = User.IsInRole(RoleConstants.AdminRoleName);
    bool activePlayers = Model.FirstOrDefault(x => x.Active) != null;
    var teamId = activePlayers ? Model.First().TeamId : Guid.NewGuid();

#line default
#line hidden
            BeginContext(257, 8, true);
            WriteLiteral("\r\n<table");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 265, "\"", 283, 2);
            WriteAttributeValue("", 270, "table_", 270, 6, true);
#line 9 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
WriteAttributeValue("", 276, teamId, 276, 7, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(284, 322, true);
            WriteLiteral(@" class=""table table-hover text-center"">
    <thead>
        <tr>
            <th>#</th>
            <th>Image</th>
            <th>Name</th>
            <th>Nationality</th>
            <th>Position</th>
            <th>Price</th>
            <th>Goals</th>
            <th>Assist</th>
            <th>TP</th>
");
            EndContext();
#line 21 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
             if (isAdmin)
            {

#line default
#line hidden
            BeginContext(648, 34, true);
            WriteLiteral("                <th>Actions</th>\r\n");
            EndContext();
#line 24 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
            }

#line default
#line hidden
            BeginContext(697, 42, true);
            WriteLiteral("        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 28 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
          
            for (int i = 0; i < Model.Count; i++)
            {

#line default
#line hidden
            BeginContext(817, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(865, 5, false);
#line 32 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                    Write(i + 1);

#line default
#line hidden
            EndContext();
            BeginContext(871, 54, true);
            WriteLiteral("</td>\r\n                    <td><img class=\"mini-image\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 925, "\"", 952, 1);
#line 33 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
WriteAttributeValue("", 931, Model[i].PlayerImage, 931, 21, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(953, 60, true);
            WriteLiteral(" /></td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1014, 13, false);
#line 35 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                   Write(Model[i].Name);

#line default
#line hidden
            EndContext();
            BeginContext(1027, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1107, 20, false);
#line 38 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                   Write(Model[i].Nationality);

#line default
#line hidden
            EndContext();
            BeginContext(1127, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1207, 17, false);
#line 41 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                   Write(Model[i].Position);

#line default
#line hidden
            EndContext();
            BeginContext(1224, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1304, 14, false);
#line 44 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                   Write(Model[i].Price);

#line default
#line hidden
            EndContext();
            BeginContext(1318, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1398, 14, false);
#line 47 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                   Write(Model[i].Goals);

#line default
#line hidden
            EndContext();
            BeginContext(1412, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1492, 16, false);
#line 50 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                   Write(Model[i].Assists);

#line default
#line hidden
            EndContext();
            BeginContext(1508, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1588, 20, false);
#line 53 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                   Write(Model[i].TotalPoints);

#line default
#line hidden
            EndContext();
            BeginContext(1608, 29, true);
            WriteLiteral("\r\n                    </td>\r\n");
            EndContext();
#line 55 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                     if (isAdmin)
                    {

#line default
#line hidden
            BeginContext(1695, 117, true);
            WriteLiteral("                        <td>\r\n                            <div class=\"button-holder d-flex justify-content-around\">\r\n");
            EndContext();
#line 59 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                 if (activePlayers)
                                {

#line default
#line hidden
            BeginContext(1900, 36, true);
            WriteLiteral("                                    ");
            EndContext();
            BeginContext(1936, 263, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "81e409cb5d33693373c58a47ab56e11b107a216114769", async() => {
                BeginContext(2081, 114, true);
                WriteLiteral("\r\n                                        <span class=\"oi oi-pencil\"></span>\r\n                                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 61 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                           WriteLiteral(PagesConstants.Players);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 61 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                                                                WriteLiteral(ActionConstants.Edit);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 61 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                                                                                                     WriteLiteral(Model[i].Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2199, 38, true);
            WriteLiteral("\r\n                                    ");
            EndContext();
            BeginContext(2237, 315, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "81e409cb5d33693373c58a47ab56e11b107a216118887", async() => {
                BeginContext(2435, 113, true);
                WriteLiteral("\r\n                                        <span class=\"oi oi-minus\"></span>\r\n                                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 64 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                           WriteLiteral(PagesConstants.Players);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 64 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                                                                WriteLiteral(ActionConstants.Archive);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-playerId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 64 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                                                                                                              WriteLiteral(Model[i].Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["playerId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-playerId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["playerId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 64 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                                                                                                                                              WriteLiteral(Model[i].TeamId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["teamId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-teamId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["teamId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2552, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 67 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                }
                                else
                                {

#line default
#line hidden
            BeginContext(2662, 36, true);
            WriteLiteral("                                    ");
            EndContext();
            BeginContext(2698, 314, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "81e409cb5d33693373c58a47ab56e11b107a216124325", async() => {
                BeginContext(2896, 112, true);
                WriteLiteral("\r\n                                        <span class=\"oi oi-plus\"></span>\r\n                                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 70 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                           WriteLiteral(PagesConstants.Players);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 70 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                                                                WriteLiteral(ActionConstants.Restore);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-playerId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 70 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                                                                                                              WriteLiteral(Model[i].Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["playerId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-playerId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["playerId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 70 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                                                                                                                                              WriteLiteral(Model[i].TeamId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["teamId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-teamId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["teamId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3012, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 73 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                                }

#line default
#line hidden
            BeginContext(3049, 67, true);
            WriteLiteral("                            </div>\r\n                        </td>\r\n");
            EndContext();
#line 76 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
                    }

#line default
#line hidden
            BeginContext(3139, 23, true);
            WriteLiteral("                </tr>\r\n");
            EndContext();
#line 78 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
            }
        

#line default
#line hidden
            BeginContext(3188, 473, true);
            WriteLiteral(@"    </tbody>
</table>


<script type=""text/javascript"" charset=""utf8"" src=""https://code.jquery.com/jquery-3.3.1.js""></script>
<script type=""text/javascript"" charset=""utf8"" src=""https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js""></script>
<script type=""text/javascript"" charset=""utf8"" src=""https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js""></script>

<script type=""text/javascript"">
    $(document).ready(function () {
        $('#table_");
            EndContext();
            BeginContext(3662, 6, false);
#line 90 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Shared\_PlayerStatsPartial.cshtml"
             Write(teamId);

#line default
#line hidden
            EndContext();
            BeginContext(3668, 79, true);
            WriteLiteral("\').DataTable({\r\n            responsive: true\r\n        });\r\n    });\r\n</script>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<PlayerStatsViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
