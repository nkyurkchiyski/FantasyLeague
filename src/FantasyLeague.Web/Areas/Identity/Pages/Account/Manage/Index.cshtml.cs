using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Team;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FantasyLeague.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITeamsService _teamsService;
        private readonly IUsersService _usersService;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITeamsService teamsService,
            IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _teamsService = teamsService;
            _usersService = usersService;
        }

        public string Username { get; set; }
        
        [Display(Name = "Club Name")]
        public string ClubName { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public ICollection<TeamViewModel> BundesligaTeams { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Favourite Team")]
            public string FavouriteTeamName { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var clubName = await _usersService.GetClubNameAsync(user.Id);
            var favouriteTeam = await _usersService.GetFavouriteTeamAsync(user.Id);

            Username = userName;
            ClubName = clubName;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
            };

            if (favouriteTeam!=null)
            {
                Input.FavouriteTeamName = favouriteTeam.Name;
            }

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            BundesligaTeams = this._teamsService
                .All<TeamViewModel>();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                BundesligaTeams = this._teamsService
                .All<TeamViewModel>();
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            var favouriteTeam = await _usersService.GetFavouriteTeamAsync(user.Id);
            
            if ((favouriteTeam == null && Input.FavouriteTeamName != null)||
                Input.FavouriteTeamName != favouriteTeam.Name)
            {
                var setFavouriteTeamResult = await _usersService.SetFavouriteTeamAsync(user.Id, Input.FavouriteTeamName);
                if (!setFavouriteTeamResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting favourite team for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            return RedirectToPage();
        }
    }
}
