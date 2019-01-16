using FantasyLeague.Common.Constants;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FantasyLeague.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ITeamsService _teamsService;
        private readonly IUsersService _usersService;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            ITeamsService teamsService,
            IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _teamsService = teamsService;
            _usersService = usersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public ICollection<TeamViewModel> BundesligaTeams { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [Display(Name = "Club Name")]
            public string ClubName { get; set; }

            [Display(Name = "Favourite Team")]
            public Guid? FavouriteTeamId { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction(
                    controllerName: PagesConstants.Home,
                    actionName: ActionConstants.Index);
            }

            ReturnUrl = returnUrl;
            BundesligaTeams = this._teamsService
                .All<TeamViewModel>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Username,
                    Email = Input.Email
                };

                if (!string.IsNullOrEmpty(Input.FavouriteTeamId.ToString()))
                {
                    user.FavouriteTeamId = Input.FavouriteTeamId;
                }

                var clubNameResult = _usersService.ClubNameVacant(Input.ClubName);
                var result = new IdentityResult();
                var addRoleResult = new IdentityResult();

                if (clubNameResult.Succeeded)
                {
                    user.ClubName = Input.ClubName;
                    result = await _userManager.CreateAsync(user, Input.Password);
                    addRoleResult = await this._userManager.AddToRoleAsync(user, RoleConstants.UserRoleName);
                }

                if (result.Succeeded && clubNameResult.Succeeded && addRoleResult.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                foreach (var error in addRoleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                if (!clubNameResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, clubNameResult.Error);
                }
            }

            BundesligaTeams = this._teamsService
                .All<TeamViewModel>();
            return Page();
        }
    }
}
