#pragma checksum "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "06a849000633a14db8fc17cc6a7420bd9dbc27c3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Competition_Views_Teams_Details), @"mvc.1.0.view", @"/Areas/Competition/Views/Teams/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Competition/Views/Teams/Details.cshtml", typeof(AspNetCore.Areas_Competition_Views_Teams_Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"06a849000633a14db8fc17cc6a7420bd9dbc27c3", @"/Areas/Competition/Views/Teams/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"993710b4cd2d4acba9eae3a87431601d1ac927b4", @"/Areas/Competition/Views/_ViewImports.cshtml")]
    public class Areas_Competition_Views_Teams_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TeamPlayersViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Areas/Competition/Views/Shared/_PlayerStatsPartial.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
    ViewData["Title"] = Model.Name;
    ViewData["ActivePage"] = PagesConstants.Teams;

    var activePlayers = Model.Players.Where(x => x.Active).ToList();
    var archivedPlayers = Model.Players.Where(x => !x.Active).ToList();
    bool isAdmin = User.IsInRole(RoleConstants.AdminRoleName);

#line default
#line hidden
            BeginContext(338, 6, true);
            WriteLiteral("\r\n<h1>");
            EndContext();
            BeginContext(345, 10, false);
#line 11 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(355, 7, true);
            WriteLiteral("</h1>\r\n");
            EndContext();
            BeginContext(366, 4, true);
            WriteLiteral("    ");
            EndContext();
            BeginContext(370, 161, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "06a849000633a14db8fc17cc6a7420bd9dbc27c37966", async() => {
                BeginContext(478, 49, true);
                WriteLiteral("<span class=\"oi oi-share-boxed flip-icon\"></span>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 13 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
           WriteLiteral(PagesConstants.Teams);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 13 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
                                              WriteLiteral(ActionConstants.All);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(531, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(536, 171, true);
            WriteLiteral("<hr>\r\n\r\n<div class=\"bundesliga-box-shadow p-3 rounded table-responsive\">\r\n\r\n    <div class=\"d-flex justify-content-between\">\r\n        <h2 class=\"m-0\">Active players</h2>\r\n");
            EndContext();
#line 21 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
         if (isAdmin)
        {

#line default
#line hidden
            BeginContext(741, 12, true);
            WriteLiteral("            ");
            EndContext();
            BeginContext(753, 148, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "06a849000633a14db8fc17cc6a7420bd9dbc27c311303", async() => {
                BeginContext(887, 10, true);
                WriteLiteral("Add player");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 23 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
                   WriteLiteral(PagesConstants.Players);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 23 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
                                                        WriteLiteral(ActionConstants.Create);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-teamId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 23 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
                                                                                                   WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["teamId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-teamId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["teamId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(901, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 24 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
        }

#line default
#line hidden
            BeginContext(914, 28, true);
            WriteLiteral("    </div>\r\n    <hr />\r\n    ");
            EndContext();
            BeginContext(942, 101, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "06a849000633a14db8fc17cc6a7420bd9dbc27c315428", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#line 27 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = activePlayers;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1043, 161, true);
            WriteLiteral("\r\n</div>\r\n<hr class=\"bg-danger\" />\r\n<div class=\"bundesliga-box-shadow p-3 rounded table-responsive\">\r\n    <h2 class=\"m-0\">Archived players</h2>\r\n    <hr />\r\n    ");
            EndContext();
            BeginContext(1204, 103, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "06a849000633a14db8fc17cc6a7420bd9dbc27c317349", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#line 33 "C:\Users\Nikolay Kyurkchiyski\Documents\Visual Studio 2017\Projects\FantasyLeague\FantasyLeague.Web\Areas\Competition\Views\Teams\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = archivedPlayers;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1307, 10, true);
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TeamPlayersViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
