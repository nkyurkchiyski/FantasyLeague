using FantasyLeague.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace FantasyLeague.Web.Views.Shared
{
    public class ManageSidebar
    {

        public static string[] AccountPages = new[]
        {
            PagesConstants.AccountIndex,
            PagesConstants.ChangePassword,
            PagesConstants.ExternalLogins,
            PagesConstants.PersonalData,
            PagesConstants.TwoFactorAuthentication
        };

        public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, PagesConstants.Home);
        public static string MatchdaysNavClass(ViewContext viewContext) => PageNavClass(viewContext, PagesConstants.Matchdays);
        public static string RosterNavClass(ViewContext viewContext) => PageNavClass(viewContext, PagesConstants.Roster);
        public static string TeamsNavClass(ViewContext viewContext) => PageNavClass(viewContext, PagesConstants.Teams);
        public static string LeaguesNavClass(ViewContext viewContext) => PageNavClass(viewContext, PagesConstants.Leagues);
        public static string AccountNavClass(ViewContext viewContext) => ParentPageNavClass(viewContext, AccountPages);
        public static string RegisterNavClass(ViewContext viewContext) => PageNavClass(viewContext, PagesConstants.Register);
        public static string LoginNavClass(ViewContext viewContext) => PageNavClass(viewContext, PagesConstants.Login);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        private static string ParentPageNavClass(ViewContext viewContext, string[] pages)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                 ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return pages.Contains(activePage) ? "active" : null;
        }

    }
}
